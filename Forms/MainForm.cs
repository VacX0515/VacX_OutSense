using System.IO.Ports;
using System.Text;
using VacX_OutSense.Core.Communication;
using VacX_OutSense.Core.Devices.Base;
using VacX_OutSense.Core.Devices.Gauges;
using VacX_OutSense.Core.Devices.IO_Module;
using VacX_OutSense.Core.Devices.IO_Module.Enum;
using VacX_OutSense.Core.Devices.IO_Module.Models;
using VacX_OutSense.Core.Devices.Relay_Module;
using VacX_OutSense.Core.Devices.Relay_Module.Enum;
using VacX_OutSense.Core.Devices.Relay_Module;
using VacX_OutSense.Core.Devices.Relay_Module.Models;

namespace VacX_OutSense
{
    public partial class MainForm : Form
    {
        #region 필드 및 속성

        // 통신 관리자
        private SerialManager _serialManager;

        // 장치 목록
        private List<IDevice> _deviceList = new List<IDevice>();

        // 연결 정보
        private string _selectedPort;
        private int _selectedBaudRate = 9600;
        private Parity _selectedParity = Parity.None;
        private int _selectedDataBits = 8;
        private StopBits _selectedStopBits = StopBits.One;

        // 통신 장치 인스턴스들
        private IO_Module _ioModule;
        private RelayModule _relayModule;
        private bool _isUpdatingRelayData = false;

        // 장치 인스턴트
        private ATMswitch _atmSwitch;
        private PiraniGauge _piraniGauge;
        private IonGauge _ionGauge;


        // 타이머 (주기적 데이터 업데이트용)
        private System.Windows.Forms.Timer _updateTimer;
        private const int DEFAULT_UPDATE_INTERVAL = 200; // 0.2초

        // 로깅
        private StringBuilder _logBuffer = new StringBuilder();
        private const int MAX_LOG_ENTRIES = 100;

        #endregion

        #region 생성자 및 초기화

        public MainForm()
        {
            InitializeComponent();
            InitializeIOList();
            InitializeSerialManager();
            InitializeDevices();
            SetupEventHandlers();
        }

        private void InitializeIOList()
        {
            this.gridViewMaster.Columns.Add("ChannelId", "채널");
            this.gridViewMaster.Columns.Add("CurrentValue", "전류(mA)");
            this.gridViewMaster.Columns.Add("Percentage", "백분율(%)");


            // 4개 채널에 대한 행 추가
            for (int i = 0; i < 4; i++)
            {
                this.gridViewMaster.Rows.Add($"채널 {i + 1}", "0.000", "0.0");
            }

            layout.Controls.Add(this.gridViewMaster, 0, 1);

            this.gridViewExpansion.Columns.Add("ChannelId", "채널");
            this.gridViewExpansion.Columns.Add("VoltageValue", "전압(V)");
            this.gridViewExpansion.Columns.Add("Percentage", "백분율(%)");

            // 8개 채널에 대한 행 추가
            for (int i = 0; i < 8; i++)
            {
                this.gridViewExpansion.Rows.Add($"채널 {i + 1}", "0.000", "0.0");
            }

            layout.Controls.Add(this.gridViewExpansion, 0, 2);
        }

        private void InitializeSerialManager()
        {
            _serialManager = SerialManager.Instance;
            _serialManager.StatusChanged += SerialManager_StatusChanged;
            _serialManager.DataReceived += SerialManager_DataReceived;
        }

        private void InitializeDevices()
        {
            // IO 모듈 초기화
            _ioModule = new IO_Module(_serialManager, "M31-XAXA0404G-L", 1);
            _ioModule.StatusChanged += Device_StatusChanged;
            _ioModule.ErrorOccurred += Device_ErrorOccurred;
            connectionIndicator_iomodule.DataSource = _ioModule;
            connectionIndicator_iomodule.DataMember = "IsConnected";

            //릴레이 모듈 초기화
            InitializeRelayModule();

            // 여기에 다른 장치들 추가 가능
            _atmSwitch = new ATMswitch();
            _piraniGauge = new PiraniGauge();
            _ionGauge = new IonGauge();

            // 통신 장치 목록에 추가
            _deviceList.Add(_ioModule);
            //_deviceList.Add(_relayModule);


            // 연결
            ConnectToDevices(_deviceList);
        }

        //TODO - 인터페이스 세팅 정보 디바이스 인터페이스에 할당하기
        private void ConnectToDevices(List<IDevice> deviceList)
        {
            var settings = new CommunicationSettings()
            {
                BaudRate = _selectedBaudRate,
                DataBits = _selectedDataBits,
                Parity = _selectedParity,
                StopBits = _selectedStopBits
            };

            _ioModule.Connect("COM4", settings);
            _relayModule.Connect("COM2", settings);
            // 타이머가 초기화되었는지 확인 후 시작
            if (_updateTimer != null)
            {
                _updateTimer.Start();
            }
            else
            {
                AppendLog("타이머가 초기화되지 않았습니다.", true);
            }

        }

        private void InitializeRelayModule()
        {
            _relayModule = new RelayModule(_serialManager);
            connectionIndicator_relaymodule.DataSource = _relayModule;
            connectionIndicator_relaymodule.DataMember = "IsConnected";
        }

        private void SetupEventHandlers()
        {
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 시리얼 포트 목록 로드
            RefreshPortList();

            // 타이머 초기화 - 여기에 타이머 초기화 코드 추가
            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = DEFAULT_UPDATE_INTERVAL;
            _updateTimer.Tick += UpdateTimer_Tick;  // 이벤트 핸들러 연결

            // UI 초기화
            UpdateConnectionStatus(false);

            // 로그 창 초기화
            AppendLog("애플리케이션이 시작되었습니다.");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 연결 해제 및 리소스 정리
            DisconnectAllDevices();

            _updateTimer.Stop();
            _updateTimer.Dispose();

            _serialManager.StatusChanged -= SerialManager_StatusChanged;
            _serialManager.DataReceived -= SerialManager_DataReceived;
        }

        #endregion

        #region 이벤트 핸들러

        private void SerialManager_StatusChanged(object sender, CommunicationStatusEventArgs e)
        {
            // UI 스레드에서 실행
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => SerialManager_StatusChanged(sender, e)));
                return;
            }

            // 연결 상태 UI 업데이트
            UpdateConnectionStatus(e.IsConnected);
            AppendLog($"통신 상태 변경: {e.StatusMessage}");
        }

        private void SerialManager_DataReceived(object sender, byte[] data)
        {
            // 필요한 경우 여기서 수신 데이터 처리
        }

        private void Device_StatusChanged(object sender, DeviceStatusEventArgs e)
        {
            // UI 스레드에서 실행
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => Device_StatusChanged(sender, e)));
                return;
            }

            IDevice device = sender as IDevice;
            if (device != null)
            {
                AppendLog($"[{device.DeviceName}] 상태 변경: {e.StatusMessage}");
                UpdateDeviceInfo();
            }
        }

        private void Device_ErrorOccurred(object sender, string errorMessage)
        {
            // UI 스레드에서 실행
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => Device_ErrorOccurred(sender, errorMessage)));
                return;
            }

            IDevice device = sender as IDevice;
            string deviceName = device != null ? device.DeviceName : "Unknown";

            AppendLog($"[{deviceName}] 오류: {errorMessage}", true);
            MessageBox.Show(errorMessage, $"{deviceName} 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (_serialManager.IsConnected)
            {
                // 연결 해제
                DisconnectAllDevices();
                AppendLog("연결이 해제되었습니다.");
            }
            else
            {
                // 연결
                ConnectToSelectedPort();
            }
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshPortList();
        }

        private void ButtonSetRange_Click(object sender, EventArgs e)
        {
            if (_ioModule != null && _ioModule.IsConnected)
            {
                try
                {
                    // 마스터 모듈 범위 설정
                    CurrentRange masterRange = GetSelectedMasterRange();
                    bool masterResult = _ioModule.SetMasterCurrentRange(masterRange);

                    // 확장 모듈 범위 설정
                    VoltageRange expansionRange = GetSelectedExpansionRange();
                    bool expansionResult = _ioModule.SetExpansionVoltageRange(expansionRange);

                    if (masterResult && expansionResult)
                    {
                        AppendLog("범위 설정이 성공적으로 적용되었습니다.");
                    }
                    else
                    {
                        AppendLog("범위 설정 중 오류가 발생했습니다.", true);
                    }
                }
                catch (Exception ex)
                {
                    AppendLog($"범위 설정 오류: {ex.Message}", true);
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // 타이머 일시 중지 (처리 중에 다른 업데이트가 시작되지 않도록)
            _updateTimer.Stop();

            try
            {
                // IO 모듈 데이터 업데이트
                if (_ioModule != null && _ioModule.IsConnected)
                {
                    UpdateIOModuleData();
                }

                // 릴레이 모듈 데이터 업데이트
                if (_relayModule != null && _relayModule.IsConnected)
                {
                    UpdateRelayModuleData();
                }
            }
            catch (Exception ex)
            {
                AppendLog($"업데이트 타이머 오류: {ex.Message}", true);
            }
            finally
            {
                // 업데이트 주기가 너무 짧으면 안정성을 위해 최소값 강제 적용
                if (_updateTimer.Interval < 100)
                {
                    numericUpdateInterval.Value = 100;
                    _updateTimer.Interval = 100;
                    AppendLog("안정성을 위해 업데이트 간격을 최소 100ms로 설정합니다.");
                }

                // 타이머 재시작
                _updateTimer.Start();
            }
        }

        private void MenuCommSettings_Click(object sender, EventArgs e)
        {
            // 통신 설정 다이얼로그 표시
            // (여기서는 간단한 구현을 위해 생략)
            MessageBox.Show("통신 설정은 현재 메인 화면에서 직접 변경 가능합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuHelpAbout_Click(object sender, EventArgs e)
        {
            // 정보 다이얼로그 표시
            MessageBox.Show("VacX OutSense System Controller\n버전 1.0.0\n\n© 2024 VacX Inc.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NumericUpdateInterval_ValueChanged(object sender, EventArgs e)
        {
            // 업데이트 타이머 간격 변경
            int newInterval = (int)numericUpdateInterval.Value;

            // 안정성을 위해 최소값 설정
            if (newInterval < 100)
            {
                newInterval = 100;
                numericUpdateInterval.Value = 100;
                AppendLog("안정성을 위해 업데이트 간격은 최소 100ms 이상이어야 합니다.");
            }

            _updateTimer.Interval = newInterval;
            AppendLog($"업데이트 간격이 {newInterval}ms로 변경되었습니다.");
        }

        #endregion

        #region IO 모듈 관련 기능
        // 비동기 통신 상태 추적을 위한 변수
        private bool _isUpdatingIOData = false;

        private async void UpdateIOModuleData()
        {
            // 이전 업데이트가 완료되지 않았으면 중복 실행 방지
            if (_isUpdatingIOData)
            {
                return;
            }

            _isUpdatingIOData = true;

            try
            {
                var analogInputs = await _ioModule.ReadAnalogInputsAsync();

                // UI 스레드에서 실행
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (analogInputs != null)
                        {
                            UpdateIOModuleUI(analogInputs);
                        }
                        else
                        {
                            // 읽기 실패 시 UI에 표시할 처리 (예: 상태 표시 업데이트)
                            toolStripStatusConnection.Text = "응답 읽기 실패";
                        }

                        _isUpdatingIOData = false;
                    }));
                }
                else
                {
                    if (analogInputs != null)
                    {
                        UpdateIOModuleUI(analogInputs);
                    }
                    else
                    {
                        // 읽기 실패 시 UI에 표시할 처리
                        toolStripStatusConnection.Text = "응답 읽기 실패";
                    }

                    _isUpdatingIOData = false;
                }
            }
            catch (Exception ex)
            {
                AppendLog($"IO 모듈 데이터 업데이트 오류: {ex.Message}", true);
                _isUpdatingIOData = false;
            }
        }

        private void UpdateIOModuleUI(AnalogInputValues values)
        {
            // 마스터 모듈 데이터 업데이트
            for (int i = 0; i < Math.Min(4, values.MasterCurrentValues.Length); i++)
            {
                double current = values.MasterCurrentValues[i];
                double percentage = CalculateCurrentPercentage(current, values.MasterCurrentRange);

                gridViewMaster.Rows[i].Cells["CurrentValue"].Value = current.ToString("F3");
                gridViewMaster.Rows[i].Cells["Percentage"].Value = percentage.ToString("F1");
            }

            // 확장 모듈 데이터 업데이트
            for (int i = 0; i < Math.Min(8, values.ExpansionVoltageValues.Length); i++)
            {
                double voltage = values.ExpansionVoltageValues[i];
                double percentage = CalculateVoltagePercentage(voltage, values.ExpansionVoltageRange);

                gridViewExpansion.Rows[i].Cells["VoltageValue"].Value = voltage.ToString("F3");
                gridViewExpansion.Rows[i].Cells["Percentage"].Value = percentage.ToString("F1");
            }

            bindableTextBox1.TextValue = _atmSwitch.ConvertVoltageToPressure(values.ExpansionVoltageValues[0]).ToString();
            bindableTextBox2.TextValue = _piraniGauge.ConvertVoltageToPressure(values.ExpansionVoltageValues[1]).ToString();
            bindableTextBox3.TextValue = _ionGauge.ConvertVoltageToPressure(values.ExpansionVoltageValues[2]).ToString();
            bindableTextBox4.TextValue = _ionGauge.CheckGaugeStatus(values.ExpansionVoltageValues[2], values.ExpansionVoltageValues[3]).ToString();

        }

        private double CalculateCurrentPercentage(double current, CurrentRange range)
        {
            switch (range)
            {
                case CurrentRange.Range_0_20mA:
                    return (current / 20.0) * 100.0;

                case CurrentRange.Range_4_20mA:
                    if (current < 4.0) return 0.0;
                    return ((current - 4.0) / 16.0) * 100.0;

                case CurrentRange.Range_Neg20_Pos20mA:
                    return ((current + 20.0) / 40.0) * 100.0;

                default:
                    return 0.0;
            }
        }

        private double CalculateVoltagePercentage(double voltage, VoltageRange range)
        {
            switch (range)
            {
                case VoltageRange.Range_0_10V:
                    return (voltage / 10.0) * 100.0;

                case VoltageRange.Range_Neg5_Pos5V:
                    return ((voltage + 5.0) / 10.0) * 100.0;

                case VoltageRange.Range_Neg10_Pos10V:
                    return ((voltage + 10.0) / 20.0) * 100.0;

                default:
                    return 0.0;
            }
        }

        private CurrentRange GetSelectedMasterRange()
        {
            switch (comboBoxMasterRange.SelectedIndex)
            {
                case 0:
                    return CurrentRange.Range_0_20mA;
                case 1:
                    return CurrentRange.Range_4_20mA;
                case 2:
                    return CurrentRange.Range_Neg20_Pos20mA;
                default:
                    return CurrentRange.Range_0_20mA;
            }
        }

        private VoltageRange GetSelectedExpansionRange()
        {
            switch (comboBoxExpansionRange.SelectedIndex)
            {
                case 0:
                    return VoltageRange.Range_0_10V;
                case 1:
                    return VoltageRange.Range_Neg5_Pos5V;
                case 2:
                    return VoltageRange.Range_Neg10_Pos10V;
                default:
                    return VoltageRange.Range_0_10V;
            }
        }

        #endregion

        #region 릴레이 모듈 기능
        private async void UpdateRelayModuleData()
        {
            try
            {
                //모든 채널 상태 값 비동기 읽기
                var relayStates = await _relayModule.ReadAllRelayStatesAsync();

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (relayStates != null)
                        {
                            UpdateRelayModuleUI(relayStates);
                        }
                    }));
                }
                else if (relayStates != null)
                {
                    UpdateRelayModuleUI(relayStates);
                }
            }
            catch (Exception ex)
            {
                AppendLog($"릴레이 모듈 데이터 업데이트 오류: {ex.Message}", true);
            }
        }
        private void UpdateRelayModuleUI(RelayModuleValues values)
        {
            //0 게이트 1 벤트 2 배기 3 이온게이지
            btn_GV.Text = values.RelayStates[0] ? "ON" :"OFF";
            btn_VV.Text = values.RelayStates[1] ? "ON" :"OFF";
            btn_EV.Text = values.RelayStates[2] ? "ON" :"OFF";
            btn_iongauge.Text = values.RelayStates[3] ? "ON" :"OFF";
        }

        #endregion


        #region 로깅 기능

        private void AppendLog(string message, bool isError = false)
        {
            // UI 스레드에서 실행
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => AppendLog(message, isError)));
                return;
            }

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string logEntry = $"[{timestamp}] {(isError ? "[오류] " : "")}{message}";

            // 로그 버퍼에 추가
            _logBuffer.AppendLine(logEntry);

            // 최대 로그 항목 수 유지
            string[] lines = _logBuffer.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > MAX_LOG_ENTRIES)
            {
                _logBuffer.Clear();
                for (int i = lines.Length - MAX_LOG_ENTRIES; i < lines.Length; i++)
                {
                    _logBuffer.AppendLine(lines[i]);
                }
            }

            // 로그 텍스트박스 업데이트
            if (textBoxLogs != null)
            {
                textBoxLogs.Text = _logBuffer.ToString();
                textBoxLogs.SelectionStart = textBoxLogs.Text.Length;
                textBoxLogs.ScrollToCaret();
            }
        }

        #endregion

        #region 연결 및 통신 기능

        private void RefreshPortList()
        {
            comboBoxPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length > 0)
            {
                comboBoxPorts.Items.AddRange(ports);
                comboBoxPorts.SelectedIndex = 0;
                buttonConnect.Enabled = true;
            }
            else
            {
                comboBoxPorts.Items.Add("포트 없음");
                comboBoxPorts.SelectedIndex = 0;
                buttonConnect.Enabled = false;
                AppendLog("사용 가능한 COM 포트가 없습니다.", true);
            }
        }

        private void UpdateConnectionStatus(bool isConnected)
        {
            if (isConnected)
            {
                labelConnectionStatus.Text = $"연결 상태: 연결됨 ({_selectedPort}, {_selectedBaudRate}bps)";
                labelConnectionStatus.ForeColor = Color.Green;
                buttonConnect.Text = "연결 해제";
                toolStripStatusConnection.Text = $"연결됨: {_selectedPort}";

                // 장치별 UI 활성화
                comboBoxMasterRange.Enabled = true;
                comboBoxExpansionRange.Enabled = true;
                buttonSetRange.Enabled = true;

                // 타이머 시작
                _updateTimer.Start();
            }
            else
            {
                labelConnectionStatus.Text = "연결 상태: 연결 안됨";
                labelConnectionStatus.ForeColor = Color.Red;
                buttonConnect.Text = "연결";
                toolStripStatusConnection.Text = "연결 안됨";

                // 장치별 UI 비활성화
                comboBoxMasterRange.Enabled = false;
                comboBoxExpansionRange.Enabled = false;
                buttonSetRange.Enabled = false;

                // 타이머 중지
                _updateTimer.Stop();
            }
        }

        private void UpdateDeviceInfo()
        {
            StringBuilder info = new StringBuilder("장치 정보: ");

            foreach (IDevice device in _deviceList)
            {
                if (device.IsConnected)
                {
                    info.Append($"{device.DeviceName} ({device.Model}), ");
                }
            }

            // 마지막 쉼표 제거
            if (info.Length > 11)
            {
                info.Length -= 2;
            }

            labelDeviceInfo.Text = info.ToString();
        }

        private void ConnectToSelectedPort()
        {
            if (comboBoxPorts.SelectedItem == null || comboBoxPorts.SelectedItem.ToString() == "포트 없음")
            {
                MessageBox.Show("연결할 포트를 선택해주세요.", "연결 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedPort = comboBoxPorts.SelectedItem.ToString();
            _selectedBaudRate = int.Parse(comboBoxBaudRate.SelectedItem.ToString());

            var settings = new CommunicationSettings()
            {
                BaudRate = _selectedBaudRate,
                DataBits = _selectedDataBits,
                Parity = _selectedParity,
                StopBits = _selectedStopBits
            };

            try
            {
                AppendLog($"포트 {_selectedPort}에 연결 시도 중...");

                // 장치 연결
                foreach (IDevice device in _deviceList)
                {
                    bool result = device.Connect(_selectedPort, settings);
                    if (!result)
                    {
                        AppendLog($"{device.DeviceName} 연결 실패", true);
                    }
                }

                // 연결 상태 업데이트
                bool anyConnected = _deviceList.Any(d => d.IsConnected);
                if (anyConnected)
                {
                    AppendLog("최소 하나의 장치 연결 성공");
                    UpdateConnectionStatus(true);
                    UpdateDeviceInfo();
                }
                else
                {
                    AppendLog("모든 장치 연결 실패", true);
                }
            }
            catch (Exception ex)
            {
                AppendLog($"연결 오류: {ex.Message}", true);
                MessageBox.Show($"연결 오류: {ex.Message}", "연결 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisconnectAllDevices()
        {
            _updateTimer.Stop();

            foreach (IDevice device in _deviceList)
            {
                if (device.IsConnected)
                {
                    device.Disconnect();
                }
            }

            UpdateConnectionStatus(false);
        }

        #endregion

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindableTextBox1_Load(object sender, EventArgs e)
        {
            bindableTextBox1.IsReadOnly = true;
        }

        private void bindableTextBox2_Load(object sender, EventArgs e)
        {
            bindableTextBox2.LabelText = "Pirani(Torr)";
            bindableTextBox2.IsReadOnly = true;
        }

        private void bindableTextBox3_Load(object sender, EventArgs e)
        {
            bindableTextBox3.LabelText = "Ion(Torr)";
            bindableTextBox3.IsReadOnly = true;
        }

        private void bindableTextBox4_Load_1(object sender, EventArgs e)
        {
            bindableTextBox4.LabelText = "상태";
            bindableTextBox4.IsReadOnly = true;
        }

        private void btn_iongauge_Click(object sender, EventArgs e)
        {
            if (_relayModule.ReadRelayState(4) == true)
            {
                _relayModule.TurnOffRelay(4);
            }
            else
            {
                _relayModule.TurnOnRelay(4);
            }
        }

        private void connectionIndicator2_Load(object sender, EventArgs e)
        {

        }

        private void connectionIndicator3_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_relayModule.CurrentValues.RelayStates[0] == true)
            {
                _relayModule.TurnOffRelay(1);
            }
            else
            {
                _relayModule.TurnOnRelay(1);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}