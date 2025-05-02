using System;
using System.IO.Ports;
using System.Threading;
using VacX_OutSense.Core.Communication.Interfaces;

namespace VacX_OutSense.Core.Communication
{
    /// <summary>
    /// 시리얼 통신을 관리하는 클래스입니다.
    /// 싱글톤 패턴을 사용하여 애플리케이션 전체에서 하나의 인스턴스만 사용합니다.
    /// </summary>
    public class SerialManager : ICommunicationManager
    {
        #region 싱글톤 구현

        private static readonly Lazy<SerialManager> _instance = new Lazy<SerialManager>(() => new SerialManager());

        /// <summary>
        /// SerialManager의 싱글톤 인스턴스를 가져옵니다.
        /// </summary>
        public static SerialManager Instance => _instance.Value;

        #endregion

        #region 필드 및 속성

        private SerialPort _serialPort;
        private bool _isConnected;
        private readonly object _lockObject = new object();
        private readonly object _readLockObject = new object(); // 읽기 전용 락 객체 추가
        private readonly object _writeLockObject = new object(); // 쓰기 전용 락 객체 추가

        // 응답 수신 관련 필드
        private readonly int _maxReadRetries = 5;
        private readonly int _readRetryDelayMs = 20;

        /// <summary>
        /// 통신 상태 변경 이벤트
        /// </summary>
        public event EventHandler<CommunicationStatusEventArgs> StatusChanged;

        /// <summary>
        /// 데이터 수신 이벤트
        /// </summary>
        public event EventHandler<byte[]> DataReceived;

        /// <summary>
        /// 연결 상태
        /// </summary>
        public bool IsConnected
        {
            get
            {
                lock (_lockObject)
                {
                    return _isConnected && _serialPort != null && _serialPort.IsOpen;
                }
            }
            private set
            {
                lock (_lockObject)
                {
                    _isConnected = value;
                }
            }
        }

        /// <summary>
        /// 연결 ID (포트 이름)
        /// </summary>
        public string ConnectionId { get; private set; }

        /// <summary>
        /// 통신 설정
        /// </summary>
        public CommunicationSettings Settings { get; private set; }

        #endregion

        #region 생성자 및 소멸자

        /// <summary>
        /// SerialManager 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        private SerialManager()
        {
            _isConnected = false;
            Settings = new CommunicationSettings();
        }

        /// <summary>
        /// 객체가 가비지 컬렉션될 때 리소스를 해제합니다.
        /// </summary>
        ~SerialManager()
        {
            Disconnect();
        }

        #endregion

        #region ICommunicationManager 구현

        /// <summary>
        /// 시리얼 포트에 연결합니다.
        /// </summary>
        /// <param name="connectionId">포트 이름 (예: COM1)</param>
        /// <param name="settings">통신 설정</param>
        /// <returns>연결 성공 여부</returns>
        public bool Connect(string connectionId, CommunicationSettings settings)
        {
            lock (_lockObject)
            {
                try
                {
                    // 이미 연결되어 있으면 먼저 연결 해제
                    if (IsConnected)
                    {
                        Disconnect();
                    }

                    // 새 SerialPort 인스턴스 생성
                    _serialPort = new SerialPort
                    {
                        PortName = connectionId,
                        BaudRate = settings.BaudRate,
                        DataBits = settings.DataBits,
                        Parity = settings.Parity,
                        StopBits = settings.StopBits,
                        Handshake = settings.Handshake,
                        ReadTimeout = settings.ReadTimeout,
                        WriteTimeout = settings.WriteTimeout,
                        ReceivedBytesThreshold = 1,  // 1바이트가 들어오면 이벤트 발생
                        ReadBufferSize = 4096,       // 읽기 버퍼 크기 증가
                        WriteBufferSize = 4096       // 쓰기 버퍼 크기 증가
                    };

                    // 데이터 수신 이벤트 핸들러 설정
                    _serialPort.DataReceived += SerialPort_DataReceived;

                    // 포트 열기
                    _serialPort.Open();

                    // 연결 정보 저장
                    ConnectionId = connectionId;
                    Settings = settings;
                    IsConnected = true;

                    // 잔여 데이터 비우기
                    DiscardInBuffer();
                    DiscardOutBuffer();

                    // 상태 이벤트 발생
                    OnStatusChanged(new CommunicationStatusEventArgs(true, $"포트 {connectionId}에 연결됨"));

                    return true;
                }
                catch (Exception ex)
                {
                    OnStatusChanged(new CommunicationStatusEventArgs(false, $"연결 실패: {ex.Message}", ex));
                    if (_serialPort != null)
                    {
                        try
                        {
                            _serialPort.Dispose();
                        }
                        catch { /* 무시 */ }
                        _serialPort = null;
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// 시리얼 포트에 연결합니다. (기본 설정 사용)
        /// </summary>
        /// <param name="connectionId">포트 이름 (예: COM1)</param>
        /// <returns>연결 성공 여부</returns>
        public bool Connect(string connectionId)
        {
            return Connect(connectionId, new CommunicationSettings());
        }

        /// <summary>
        /// 시리얼 포트 연결을 해제합니다.
        /// </summary>
        public void Disconnect()
        {
            lock (_lockObject)
            {
                if (_serialPort != null)
                {
                    try
                    {
                        // 이벤트 핸들러 제거
                        _serialPort.DataReceived -= SerialPort_DataReceived;

                        if (_serialPort.IsOpen)
                        {
                            _serialPort.Close();
                        }

                        _serialPort.Dispose();
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged(new CommunicationStatusEventArgs(false, $"연결 해제 오류: {ex.Message}", ex));
                    }
                    finally
                    {
                        _serialPort = null;
                        IsConnected = false;
                        OnStatusChanged(new CommunicationStatusEventArgs(false, "연결 해제됨"));
                    }
                }
            }
        }

        /// <summary>
        /// 데이터를 전송합니다.
        /// </summary>
        /// <param name="buffer">전송할 데이터 버퍼</param>
        /// <param name="offset">시작 오프셋</param>
        /// <param name="count">바이트 수</param>
        /// <returns>전송 성공 여부</returns>
        public bool Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null || count <= 0 || offset < 0 || offset + count > buffer.Length)
            {
                return false;
            }

            lock (_writeLockObject) // 쓰기 전용 락 사용
            {
                if (!IsConnected || _serialPort == null)
                {
                    return false;
                }

                try
                {
                    // 쓰기 전에 출력 버퍼 비우기
                    _serialPort.DiscardOutBuffer();

                    // 데이터 전송
                    _serialPort.Write(buffer, offset, count);

                    // 모든 데이터가 전송될 때까지 대기
                    _serialPort.BaseStream.Flush();

                    return true;
                }
                catch (Exception ex)
                {
                    OnStatusChanged(new CommunicationStatusEventArgs(IsConnected, $"데이터 전송 오류: {ex.Message}", ex));
                    return false;
                }
            }
        }

        /// <summary>
        /// 데이터를 전송합니다.
        /// </summary>
        /// <param name="buffer">전송할 데이터 버퍼</param>
        /// <returns>전송 성공 여부</returns>
        public bool Write(byte[] buffer)
        {
            if (buffer == null)
            {
                return false;
            }

            return Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 모든 가용한 데이터를 읽습니다.
        /// </summary>
        /// <returns>읽은 데이터 또는 데이터가 없으면 null</returns>
        public byte[] ReadAll()
        {
            lock (_readLockObject) // 읽기 전용 락 사용
            {
                if (!IsConnected || _serialPort == null)
                {
                    return null;
                }

                try
                {
                    // 데이터가 도착할 때까지 여러 번 시도
                    for (int retry = 0; retry < _maxReadRetries; retry++)
                    {
                        int bytesToRead = _serialPort.BytesToRead;
                        if (bytesToRead > 0)
                        {
                            byte[] buffer = new byte[bytesToRead];
                            int bytesRead = _serialPort.Read(buffer, 0, bytesToRead);

                            if (bytesRead > 0)
                            {
                                if (bytesRead < bytesToRead)
                                {
                                    byte[] result = new byte[bytesRead];
                                    Array.Copy(buffer, result, bytesRead);
                                    return result;
                                }
                                return buffer;
                            }
                        }

                        // 바로 다시 시도하기 전에 짧게 대기
                        Thread.Sleep(_readRetryDelayMs);
                    }

                    // 최대 시도 횟수 이후에도 데이터가 없으면 null 반환
                    return null;
                }
                catch (TimeoutException)
                {
                    // 타임아웃은 오류로 간주하지 않음
                    return null;
                }
                catch (Exception ex)
                {
                    OnStatusChanged(new CommunicationStatusEventArgs(IsConnected, $"데이터 읽기 오류: {ex.Message}", ex));
                    return null;
                }
            }
        }

        /// <summary>
        /// 지정된 길이만큼 데이터를 읽습니다.
        /// </summary>
        /// <param name="count">읽을 바이트 수</param>
        /// <returns>읽은 데이터 또는 데이터가 없으면 null</returns>
        public byte[] Read(int count)
        {
            if (count <= 0)
            {
                return null;
            }

            lock (_readLockObject) // 읽기 전용 락 사용
            {
                if (!IsConnected || _serialPort == null)
                {
                    return null;
                }

                try
                {
                    byte[] buffer = new byte[count];
                    int totalBytesRead = 0;
                    int bytesRead = 0;
                    DateTime startTime = DateTime.Now;
                    int timeout = _serialPort.ReadTimeout > 0 ? _serialPort.ReadTimeout : 1000;

                    // 요청한 바이트 수를 모두 읽거나 타임아웃까지 반복
                    while (totalBytesRead < count &&
                           (DateTime.Now - startTime).TotalMilliseconds < timeout)
                    {
                        // 가용한 데이터가 있는지 확인
                        if (_serialPort.BytesToRead == 0)
                        {
                            // 데이터가 올 때까지 잠시 대기 후 다시 시도
                            Thread.Sleep(_readRetryDelayMs);
                            continue;
                        }

                        int bytesToRead = Math.Min(count - totalBytesRead, _serialPort.BytesToRead);
                        bytesRead = _serialPort.Read(buffer, totalBytesRead, bytesToRead);
                        totalBytesRead += bytesRead;

                        if (bytesRead == 0)
                        {
                            // 읽기 실패 시 잠시 대기 후 재시도
                            Thread.Sleep(_readRetryDelayMs);
                        }
                    }

                    if (totalBytesRead <= 0)
                    {
                        return null;
                    }

                    if (totalBytesRead < count)
                    {
                        // 요청한 만큼 데이터를 받지 못했지만, 일부 데이터가 있는 경우
                        byte[] result = new byte[totalBytesRead];
                        Array.Copy(buffer, result, totalBytesRead);
                        return result;
                    }

                    return buffer;
                }
                catch (TimeoutException)
                {
                    // 타임아웃은 오류로 간주하지 않음
                    return null;
                }
                catch (Exception ex)
                {
                    OnStatusChanged(new CommunicationStatusEventArgs(IsConnected, $"데이터 읽기 오류: {ex.Message}", ex));
                    return null;
                }
            }
        }

        /// <summary>
        /// 시간 초과 값을 설정합니다.
        /// </summary>
        /// <param name="timeout">시간 초과 (밀리초)</param>
        public void SetTimeout(int timeout)
        {
            lock (_lockObject)
            {
                if (IsConnected && _serialPort != null)
                {
                    try
                    {
                        _serialPort.ReadTimeout = timeout;
                        _serialPort.WriteTimeout = timeout;
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged(new CommunicationStatusEventArgs(IsConnected, $"시간 초과 설정 오류: {ex.Message}", ex));
                    }
                }
            }
        }

        /// <summary>
        /// 입력 버퍼를 비웁니다.
        /// </summary>
        public void DiscardInBuffer()
        {
            lock (_readLockObject)
            {
                if (IsConnected && _serialPort != null)
                {
                    try
                    {
                        _serialPort.DiscardInBuffer();
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged(new CommunicationStatusEventArgs(IsConnected, $"입력 버퍼 비우기 오류: {ex.Message}", ex));
                    }
                }
            }
        }

        /// <summary>
        /// 출력 버퍼를 비웁니다.
        /// </summary>
        public void DiscardOutBuffer()
        {
            lock (_writeLockObject)
            {
                if (IsConnected && _serialPort != null)
                {
                    try
                    {
                        _serialPort.DiscardOutBuffer();
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged(new CommunicationStatusEventArgs(IsConnected, $"출력 버퍼 비우기 오류: {ex.Message}", ex));
                    }
                }
            }
        }

        #endregion

        #region 이벤트 핸들러

        /// <summary>
        /// 시리얼 포트 데이터 수신 이벤트 핸들러
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!IsConnected || _serialPort == null)
            {
                return;
            }

            try
            {
                // 모든 데이터가 도착할 때까지 대기
                // 더 긴 시간 대기하여 패킷 전체가 도착할 확률을 높임
                Thread.Sleep(50);

                // 데이터를 읽어 이벤트로 전달
                byte[] data = ReadAll();
                if (data != null && data.Length > 0)
                {
                    OnDataReceived(data);
                }
            }
            catch (Exception ex)
            {
                OnStatusChanged(new CommunicationStatusEventArgs(IsConnected, $"데이터 수신 오류: {ex.Message}", ex));
            }
        }

        /// <summary>
        /// 통신 상태 변경 이벤트를 발생시킵니다.
        /// </summary>
        /// <param name="e">이벤트 인자</param>
        private void OnStatusChanged(CommunicationStatusEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 데이터 수신 이벤트를 발생시킵니다.
        /// </summary>
        /// <param name="data">수신된 데이터</param>
        private void OnDataReceived(byte[] data)
        {
            DataReceived?.Invoke(this, data);
        }

        #endregion
    }
}