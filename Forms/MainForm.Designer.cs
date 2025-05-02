using System.Windows.Forms;

namespace VacX_OutSense
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 해제해야 하면 true, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanelMain = new TableLayoutPanel();
            tabControlMain = new TabControl();
            tabPageIOModule = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelConnection = new Panel();
            labelPort = new Label();
            comboBoxPorts = new ComboBox();
            labelBaudRate = new Label();
            comboBoxBaudRate = new ComboBox();
            buttonConnect = new Button();
            buttonRefresh = new Button();
            labelConnectionStatus = new Label();
            labelDeviceInfo = new Label();
            labelUpdateInterval = new Label();
            numericUpdateInterval = new NumericUpDown();
            layout = new TableLayoutPanel();
            controlPanel = new Panel();
            labelMasterRange = new Label();
            comboBoxMasterRange = new ComboBox();
            labelExpansionRange = new Label();
            comboBoxExpansionRange = new ComboBox();
            buttonSetRange = new Button();
            tabPageLog = new TabPage();
            textBoxLogs = new TextBox();
            tabPage1 = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            connectionIndicator_heater = new Forms.UserControls.ConnectionIndicator();
            connectionIndicator_chiller = new Forms.UserControls.ConnectionIndicator();
            connectionIndicator_relaymodule = new Forms.UserControls.ConnectionIndicator();
            connectionIndicator_iomodule = new Forms.UserControls.ConnectionIndicator();
            connectionIndicator_drypump = new Forms.UserControls.ConnectionIndicator();
            connectionIndicator_turbopump = new Forms.UserControls.ConnectionIndicator();
            panel1 = new Panel();
            tableLayoutPanel5 = new TableLayoutPanel();
            btn_GV = new Button();
            btn_VV = new Button();
            btn_EV = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btn_iongauge = new Button();
            bindableTextBox4 = new Forms.UserControls.BindableTextBox();
            bindableTextBox3 = new Forms.UserControls.BindableTextBox();
            bindableTextBox2 = new Forms.UserControls.BindableTextBox();
            bindableTextBox1 = new Forms.UserControls.BindableTextBox();
            menuStrip = new MenuStrip();
            menuFile = new ToolStripMenuItem();
            menuFileExit = new ToolStripMenuItem();
            menuComm = new ToolStripMenuItem();
            menuCommSettings = new ToolStripMenuItem();
            menuHelp = new ToolStripMenuItem();
            menuHelpAbout = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            toolStripStatusConnection = new ToolStripStatusLabel();
            _updateTimer = new System.Windows.Forms.Timer(components);
            gridViewMaster = new DataGridView();
            gridViewExpansion = new DataGridView();
            tableLayoutPanelMain.SuspendLayout();
            tabControlMain.SuspendLayout();
            tabPageIOModule.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panelConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpdateInterval).BeginInit();
            layout.SuspendLayout();
            controlPanel.SuspendLayout();
            tabPageLog.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewMaster).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewExpansion).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tabControlMain, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 24);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelMain.Size = new Size(1186, 683);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPage1);
            tabControlMain.Controls.Add(tabPageIOModule);
            tabControlMain.Controls.Add(tabPageLog);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(3, 3);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1180, 677);
            tabControlMain.TabIndex = 1;
            // 
            // tabPageIOModule
            // 
            tabPageIOModule.Controls.Add(tableLayoutPanel1);
            tabPageIOModule.Location = new Point(4, 24);
            tabPageIOModule.Name = "tabPageIOModule";
            tabPageIOModule.Size = new Size(1172, 649);
            tabPageIOModule.TabIndex = 0;
            tabPageIOModule.Text = "IO 모듈";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelConnection, 0, 0);
            tableLayoutPanel1.Controls.Add(layout, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Size = new Size(1172, 649);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelConnection
            // 
            panelConnection.Controls.Add(labelPort);
            panelConnection.Controls.Add(comboBoxPorts);
            panelConnection.Controls.Add(labelBaudRate);
            panelConnection.Controls.Add(comboBoxBaudRate);
            panelConnection.Controls.Add(buttonConnect);
            panelConnection.Controls.Add(buttonRefresh);
            panelConnection.Controls.Add(labelConnectionStatus);
            panelConnection.Controls.Add(labelDeviceInfo);
            panelConnection.Controls.Add(labelUpdateInterval);
            panelConnection.Controls.Add(numericUpdateInterval);
            panelConnection.Dock = DockStyle.Fill;
            panelConnection.Location = new Point(3, 3);
            panelConnection.Name = "panelConnection";
            panelConnection.Size = new Size(1166, 123);
            panelConnection.TabIndex = 2;
            // 
            // labelPort
            // 
            labelPort.AutoSize = true;
            labelPort.Location = new Point(10, 15);
            labelPort.Name = "labelPort";
            labelPort.Size = new Size(34, 15);
            labelPort.TabIndex = 0;
            labelPort.Text = "포트:";
            // 
            // comboBoxPorts
            // 
            comboBoxPorts.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPorts.Location = new Point(60, 12);
            comboBoxPorts.Name = "comboBoxPorts";
            comboBoxPorts.Size = new Size(120, 23);
            comboBoxPorts.TabIndex = 1;
            // 
            // labelBaudRate
            // 
            labelBaudRate.AutoSize = true;
            labelBaudRate.Location = new Point(200, 15);
            labelBaudRate.Name = "labelBaudRate";
            labelBaudRate.Size = new Size(62, 15);
            labelBaudRate.TabIndex = 2;
            labelBaudRate.Text = "전송 속도:";
            // 
            // comboBoxBaudRate
            // 
            comboBoxBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBaudRate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            comboBoxBaudRate.Location = new Point(270, 12);
            comboBoxBaudRate.Name = "comboBoxBaudRate";
            comboBoxBaudRate.Size = new Size(100, 23);
            comboBoxBaudRate.TabIndex = 3;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(390, 10);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(100, 23);
            buttonConnect.TabIndex = 4;
            buttonConnect.Text = "연결";
            buttonConnect.Click += ButtonConnect_Click;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(500, 10);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(100, 23);
            buttonRefresh.TabIndex = 5;
            buttonRefresh.Text = "새로고침";
            buttonRefresh.Click += ButtonRefresh_Click;
            // 
            // labelConnectionStatus
            // 
            labelConnectionStatus.AutoSize = true;
            labelConnectionStatus.ForeColor = Color.Red;
            labelConnectionStatus.Location = new Point(10, 50);
            labelConnectionStatus.Name = "labelConnectionStatus";
            labelConnectionStatus.Size = new Size(118, 15);
            labelConnectionStatus.TabIndex = 6;
            labelConnectionStatus.Text = "연결 상태: 연결 안됨";
            // 
            // labelDeviceInfo
            // 
            labelDeviceInfo.AutoSize = true;
            labelDeviceInfo.Location = new Point(10, 75);
            labelDeviceInfo.Name = "labelDeviceInfo";
            labelDeviceInfo.Size = new Size(66, 15);
            labelDeviceInfo.TabIndex = 7;
            labelDeviceInfo.Text = "장치 정보: ";
            // 
            // labelUpdateInterval
            // 
            labelUpdateInterval.AutoSize = true;
            labelUpdateInterval.Location = new Point(620, 15);
            labelUpdateInterval.Name = "labelUpdateInterval";
            labelUpdateInterval.Size = new Size(110, 15);
            labelUpdateInterval.TabIndex = 8;
            labelUpdateInterval.Text = "업데이트 간격(ms):";
            // 
            // numericUpdateInterval
            // 
            numericUpdateInterval.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpdateInterval.Location = new Point(750, 12);
            numericUpdateInterval.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpdateInterval.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpdateInterval.Name = "numericUpdateInterval";
            numericUpdateInterval.Size = new Size(80, 23);
            numericUpdateInterval.TabIndex = 9;
            numericUpdateInterval.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // layout
            // 
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(controlPanel, 0, 0);
            layout.Location = new Point(3, 132);
            layout.Name = "layout";
            layout.RowCount = 3;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layout.Size = new Size(1166, 514);
            layout.TabIndex = 1;
            // 
            // controlPanel
            // 
            controlPanel.Controls.Add(labelMasterRange);
            controlPanel.Controls.Add(comboBoxMasterRange);
            controlPanel.Controls.Add(labelExpansionRange);
            controlPanel.Controls.Add(comboBoxExpansionRange);
            controlPanel.Controls.Add(buttonSetRange);
            controlPanel.Dock = DockStyle.Fill;
            controlPanel.Location = new Point(3, 3);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(1160, 44);
            controlPanel.TabIndex = 0;
            // 
            // labelMasterRange
            // 
            labelMasterRange.AutoSize = true;
            labelMasterRange.Location = new Point(10, 15);
            labelMasterRange.Name = "labelMasterRange";
            labelMasterRange.Size = new Size(130, 15);
            labelMasterRange.TabIndex = 0;
            labelMasterRange.Text = "마스터 모듈 전류 범위:";
            // 
            // comboBoxMasterRange
            // 
            comboBoxMasterRange.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMasterRange.Enabled = false;
            comboBoxMasterRange.Items.AddRange(new object[] { "0-20mA", "4-20mA", "±20mA" });
            comboBoxMasterRange.Location = new Point(150, 12);
            comboBoxMasterRange.Name = "comboBoxMasterRange";
            comboBoxMasterRange.Size = new Size(150, 23);
            comboBoxMasterRange.TabIndex = 1;
            // 
            // labelExpansionRange
            // 
            labelExpansionRange.AutoSize = true;
            labelExpansionRange.Location = new Point(320, 15);
            labelExpansionRange.Name = "labelExpansionRange";
            labelExpansionRange.Size = new Size(118, 15);
            labelExpansionRange.TabIndex = 2;
            labelExpansionRange.Text = "확장 모듈 전압 범위:";
            // 
            // comboBoxExpansionRange
            // 
            comboBoxExpansionRange.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxExpansionRange.Enabled = false;
            comboBoxExpansionRange.Items.AddRange(new object[] { "0-10V", "±5V", "±10V" });
            comboBoxExpansionRange.Location = new Point(450, 12);
            comboBoxExpansionRange.Name = "comboBoxExpansionRange";
            comboBoxExpansionRange.Size = new Size(150, 23);
            comboBoxExpansionRange.TabIndex = 3;
            // 
            // buttonSetRange
            // 
            buttonSetRange.Enabled = false;
            buttonSetRange.Location = new Point(620, 10);
            buttonSetRange.Name = "buttonSetRange";
            buttonSetRange.Size = new Size(100, 23);
            buttonSetRange.TabIndex = 4;
            buttonSetRange.Text = "범위 설정";
            buttonSetRange.Click += ButtonSetRange_Click;
            // 
            // tabPageLog
            // 
            tabPageLog.Controls.Add(textBoxLogs);
            tabPageLog.Location = new Point(4, 24);
            tabPageLog.Name = "tabPageLog";
            tabPageLog.Size = new Size(1172, 649);
            tabPageLog.TabIndex = 1;
            tabPageLog.Text = "로그";
            // 
            // textBoxLogs
            // 
            textBoxLogs.Dock = DockStyle.Fill;
            textBoxLogs.Font = new Font("Consolas", 9.75F);
            textBoxLogs.Location = new Point(0, 0);
            textBoxLogs.Multiline = true;
            textBoxLogs.Name = "textBoxLogs";
            textBoxLogs.ReadOnly = true;
            textBoxLogs.ScrollBars = ScrollBars.Vertical;
            textBoxLogs.Size = new Size(1172, 649);
            textBoxLogs.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1172, 649);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Main";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(panel1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel2.Size = new Size(1166, 643);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1160, 58);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 6;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel4.Controls.Add(connectionIndicator_heater, 5, 0);
            tableLayoutPanel4.Controls.Add(connectionIndicator_chiller, 4, 0);
            tableLayoutPanel4.Controls.Add(connectionIndicator_relaymodule, 3, 0);
            tableLayoutPanel4.Controls.Add(connectionIndicator_iomodule, 2, 0);
            tableLayoutPanel4.Controls.Add(connectionIndicator_drypump, 1, 0);
            tableLayoutPanel4.Controls.Add(connectionIndicator_turbopump, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(699, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(458, 52);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // connectionIndicator_heater
            // 
            connectionIndicator_heater.ComponentName = "Heater";
            connectionIndicator_heater.ConnectedColor = Color.LimeGreen;
            connectionIndicator_heater.DataMember = null;
            connectionIndicator_heater.DataSource = null;
            connectionIndicator_heater.DisconnectedColor = Color.Red;
            connectionIndicator_heater.Dock = DockStyle.Fill;
            connectionIndicator_heater.Font = new Font("Arial Narrow", 6.75F);
            connectionIndicator_heater.IsConnected = false;
            connectionIndicator_heater.Location = new Point(383, 4);
            connectionIndicator_heater.Margin = new Padding(3, 4, 3, 4);
            connectionIndicator_heater.Name = "connectionIndicator_heater";
            connectionIndicator_heater.Size = new Size(72, 44);
            connectionIndicator_heater.TabIndex = 5;
            // 
            // connectionIndicator_chiller
            // 
            connectionIndicator_chiller.ComponentName = "Chiller";
            connectionIndicator_chiller.ConnectedColor = Color.LimeGreen;
            connectionIndicator_chiller.DataMember = null;
            connectionIndicator_chiller.DataSource = null;
            connectionIndicator_chiller.DisconnectedColor = Color.Red;
            connectionIndicator_chiller.Dock = DockStyle.Fill;
            connectionIndicator_chiller.Font = new Font("Arial Narrow", 6.75F);
            connectionIndicator_chiller.IsConnected = false;
            connectionIndicator_chiller.Location = new Point(307, 4);
            connectionIndicator_chiller.Margin = new Padding(3, 4, 3, 4);
            connectionIndicator_chiller.Name = "connectionIndicator_chiller";
            connectionIndicator_chiller.Size = new Size(70, 44);
            connectionIndicator_chiller.TabIndex = 4;
            // 
            // connectionIndicator_relaymodule
            // 
            connectionIndicator_relaymodule.ComponentName = "RelayModule";
            connectionIndicator_relaymodule.ConnectedColor = Color.LimeGreen;
            connectionIndicator_relaymodule.DataMember = null;
            connectionIndicator_relaymodule.DataSource = null;
            connectionIndicator_relaymodule.DisconnectedColor = Color.Red;
            connectionIndicator_relaymodule.Dock = DockStyle.Fill;
            connectionIndicator_relaymodule.Font = new Font("Arial Narrow", 6.75F);
            connectionIndicator_relaymodule.IsConnected = false;
            connectionIndicator_relaymodule.Location = new Point(231, 4);
            connectionIndicator_relaymodule.Margin = new Padding(3, 4, 3, 4);
            connectionIndicator_relaymodule.Name = "connectionIndicator_relaymodule";
            connectionIndicator_relaymodule.Size = new Size(70, 44);
            connectionIndicator_relaymodule.TabIndex = 3;
            // 
            // connectionIndicator_iomodule
            // 
            connectionIndicator_iomodule.ComponentName = "IO Module";
            connectionIndicator_iomodule.ConnectedColor = Color.LimeGreen;
            connectionIndicator_iomodule.DataMember = null;
            connectionIndicator_iomodule.DataSource = null;
            connectionIndicator_iomodule.DisconnectedColor = Color.Red;
            connectionIndicator_iomodule.Dock = DockStyle.Fill;
            connectionIndicator_iomodule.Font = new Font("Arial Narrow", 6.75F);
            connectionIndicator_iomodule.IsConnected = false;
            connectionIndicator_iomodule.Location = new Point(155, 4);
            connectionIndicator_iomodule.Margin = new Padding(3, 4, 3, 4);
            connectionIndicator_iomodule.Name = "connectionIndicator_iomodule";
            connectionIndicator_iomodule.Size = new Size(70, 44);
            connectionIndicator_iomodule.TabIndex = 2;
            // 
            // connectionIndicator_drypump
            // 
            connectionIndicator_drypump.ComponentName = "DryPump";
            connectionIndicator_drypump.ConnectedColor = Color.LimeGreen;
            connectionIndicator_drypump.DataMember = null;
            connectionIndicator_drypump.DataSource = null;
            connectionIndicator_drypump.DisconnectedColor = Color.Red;
            connectionIndicator_drypump.Dock = DockStyle.Fill;
            connectionIndicator_drypump.Font = new Font("Arial Narrow", 6.75F);
            connectionIndicator_drypump.IsConnected = false;
            connectionIndicator_drypump.Location = new Point(79, 4);
            connectionIndicator_drypump.Margin = new Padding(3, 4, 3, 4);
            connectionIndicator_drypump.Name = "connectionIndicator_drypump";
            connectionIndicator_drypump.Size = new Size(70, 44);
            connectionIndicator_drypump.TabIndex = 1;
            // 
            // connectionIndicator_turbopump
            // 
            connectionIndicator_turbopump.ComponentName = "TurboPump";
            connectionIndicator_turbopump.ConnectedColor = Color.LimeGreen;
            connectionIndicator_turbopump.DataMember = null;
            connectionIndicator_turbopump.DataSource = null;
            connectionIndicator_turbopump.DisconnectedColor = Color.Red;
            connectionIndicator_turbopump.Dock = DockStyle.Fill;
            connectionIndicator_turbopump.Font = new Font("Arial Narrow", 6.75F);
            connectionIndicator_turbopump.IsConnected = false;
            connectionIndicator_turbopump.Location = new Point(3, 4);
            connectionIndicator_turbopump.Margin = new Padding(3, 4, 3, 4);
            connectionIndicator_turbopump.Name = "connectionIndicator_turbopump";
            connectionIndicator_turbopump.Size = new Size(70, 44);
            connectionIndicator_turbopump.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(tableLayoutPanel5);
            panel1.Controls.Add(btn_iongauge);
            panel1.Controls.Add(bindableTextBox4);
            panel1.Controls.Add(bindableTextBox3);
            panel1.Controls.Add(bindableTextBox2);
            panel1.Controls.Add(bindableTextBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 67);
            panel1.Name = "panel1";
            panel1.Size = new Size(1160, 573);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel5.Controls.Add(btn_GV, 0, 1);
            tableLayoutPanel5.Controls.Add(btn_VV, 1, 1);
            tableLayoutPanel5.Controls.Add(btn_EV, 2, 1);
            tableLayoutPanel5.Controls.Add(label1, 0, 0);
            tableLayoutPanel5.Controls.Add(label2, 1, 0);
            tableLayoutPanel5.Controls.Add(label3, 2, 0);
            tableLayoutPanel5.Location = new Point(31, 121);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(294, 127);
            tableLayoutPanel5.TabIndex = 8;
            // 
            // btn_GV
            // 
            btn_GV.Dock = DockStyle.Fill;
            btn_GV.Location = new Point(3, 23);
            btn_GV.Name = "btn_GV";
            btn_GV.Size = new Size(91, 101);
            btn_GV.TabIndex = 0;
            btn_GV.Text = "button1";
            btn_GV.UseVisualStyleBackColor = true;
            btn_GV.Click += button1_Click;
            // 
            // btn_VV
            // 
            btn_VV.Dock = DockStyle.Fill;
            btn_VV.Location = new Point(100, 23);
            btn_VV.Name = "btn_VV";
            btn_VV.Size = new Size(92, 101);
            btn_VV.TabIndex = 1;
            btn_VV.Text = "button2";
            btn_VV.UseVisualStyleBackColor = true;
            // 
            // btn_EV
            // 
            btn_EV.Dock = DockStyle.Fill;
            btn_EV.Location = new Point(198, 23);
            btn_EV.Name = "btn_EV";
            btn_EV.Size = new Size(93, 101);
            btn_EV.TabIndex = 2;
            btn_EV.Text = "button3";
            btn_EV.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Dock = DockStyle.Fill;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(91, 20);
            label1.TabIndex = 3;
            label1.Text = "GateVavle";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Dock = DockStyle.Fill;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Location = new Point(100, 0);
            label2.Name = "label2";
            label2.Size = new Size(92, 20);
            label2.TabIndex = 4;
            label2.Text = "VentValve";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Dock = DockStyle.Fill;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Location = new Point(198, 0);
            label3.Name = "label3";
            label3.Size = new Size(93, 20);
            label3.TabIndex = 5;
            label3.Text = "ExhaustValve";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // btn_iongauge
            // 
            btn_iongauge.Location = new Point(1065, 16);
            btn_iongauge.Name = "btn_iongauge";
            btn_iongauge.Size = new Size(75, 23);
            btn_iongauge.TabIndex = 7;
            btn_iongauge.Text = "On";
            btn_iongauge.UseVisualStyleBackColor = true;
            btn_iongauge.Click += btn_iongauge_Click;
            // 
            // bindableTextBox4
            // 
            bindableTextBox4.DataMember = null;
            bindableTextBox4.DataSource = null;
            bindableTextBox4.FormatString = null;
            bindableTextBox4.IsReadOnly = false;
            bindableTextBox4.LabelText = "레이블:";
            bindableTextBox4.Location = new Point(809, 55);
            bindableTextBox4.Name = "bindableTextBox4";
            bindableTextBox4.Padding = new Padding(0, 0, 0, 3);
            bindableTextBox4.Size = new Size(346, 33);
            bindableTextBox4.TabIndex = 6;
            bindableTextBox4.TextValue = "";
            bindableTextBox4.Load += bindableTextBox4_Load_1;
            // 
            // bindableTextBox3
            // 
            bindableTextBox3.DataMember = null;
            bindableTextBox3.DataSource = null;
            bindableTextBox3.FormatString = null;
            bindableTextBox3.IsReadOnly = false;
            bindableTextBox3.LabelText = "레이블:";
            bindableTextBox3.Location = new Point(809, 16);
            bindableTextBox3.Name = "bindableTextBox3";
            bindableTextBox3.Padding = new Padding(0, 0, 0, 3);
            bindableTextBox3.Size = new Size(250, 33);
            bindableTextBox3.TabIndex = 5;
            bindableTextBox3.TextValue = "";
            bindableTextBox3.Load += bindableTextBox3_Load;
            // 
            // bindableTextBox2
            // 
            bindableTextBox2.DataMember = null;
            bindableTextBox2.DataSource = null;
            bindableTextBox2.FormatString = null;
            bindableTextBox2.IsReadOnly = false;
            bindableTextBox2.LabelText = "레이블:";
            bindableTextBox2.Location = new Point(417, 16);
            bindableTextBox2.Name = "bindableTextBox2";
            bindableTextBox2.Padding = new Padding(0, 0, 0, 3);
            bindableTextBox2.Size = new Size(250, 63);
            bindableTextBox2.TabIndex = 4;
            bindableTextBox2.TextValue = "";
            bindableTextBox2.Load += bindableTextBox2_Load;
            // 
            // bindableTextBox1
            // 
            bindableTextBox1.DataMember = null;
            bindableTextBox1.DataSource = null;
            bindableTextBox1.FormatString = null;
            bindableTextBox1.IsReadOnly = false;
            bindableTextBox1.LabelText = "ATM(kPa)";
            bindableTextBox1.Location = new Point(31, 16);
            bindableTextBox1.Name = "bindableTextBox1";
            bindableTextBox1.Padding = new Padding(0, 0, 0, 3);
            bindableTextBox1.Size = new Size(250, 63);
            bindableTextBox1.TabIndex = 3;
            bindableTextBox1.TextValue = "";
            bindableTextBox1.Load += bindableTextBox1_Load;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { menuFile, menuComm, menuHelp });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1186, 24);
            menuStrip.TabIndex = 1;
            // 
            // menuFile
            // 
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuFileExit });
            menuFile.Name = "menuFile";
            menuFile.Size = new Size(57, 20);
            menuFile.Text = "파일(&F)";
            // 
            // menuFileExit
            // 
            menuFileExit.Name = "menuFileExit";
            menuFileExit.Size = new Size(113, 22);
            menuFileExit.Text = "종료(&X)";
            menuFileExit.Click += menuFileExit_Click;
            // 
            // menuComm
            // 
            menuComm.DropDownItems.AddRange(new ToolStripItem[] { menuCommSettings });
            menuComm.Name = "menuComm";
            menuComm.Size = new Size(59, 20);
            menuComm.Text = "통신(&C)";
            // 
            // menuCommSettings
            // 
            menuCommSettings.Name = "menuCommSettings";
            menuCommSettings.Size = new Size(141, 22);
            menuCommSettings.Text = "통신 설정(&S)";
            menuCommSettings.Click += MenuCommSettings_Click;
            // 
            // menuHelp
            // 
            menuHelp.DropDownItems.AddRange(new ToolStripItem[] { menuHelpAbout });
            menuHelp.Name = "menuHelp";
            menuHelp.Size = new Size(72, 20);
            menuHelp.Text = "도움말(&H)";
            // 
            // menuHelpAbout
            // 
            menuHelpAbout.Name = "menuHelpAbout";
            menuHelpAbout.Size = new Size(114, 22);
            menuHelpAbout.Text = "정보(&A)";
            menuHelpAbout.Click += MenuHelpAbout_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusConnection });
            statusStrip.Location = new Point(0, 707);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1186, 22);
            statusStrip.TabIndex = 2;
            // 
            // toolStripStatusConnection
            // 
            toolStripStatusConnection.Name = "toolStripStatusConnection";
            toolStripStatusConnection.Size = new Size(31, 17);
            toolStripStatusConnection.Text = "준비";
            // 
            // _updateTimer
            // 
            _updateTimer.Interval = 200;
            _updateTimer.Tick += UpdateTimer_Tick;
            // 
            // gridViewMaster
            // 
            gridViewMaster.AllowUserToAddRows = false;
            gridViewMaster.AllowUserToDeleteRows = false;
            gridViewMaster.AllowUserToResizeRows = false;
            gridViewMaster.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewMaster.Dock = DockStyle.Fill;
            gridViewMaster.Location = new Point(0, 0);
            gridViewMaster.Name = "gridViewMaster";
            gridViewMaster.ReadOnly = true;
            gridViewMaster.RowHeadersVisible = false;
            gridViewMaster.Size = new Size(240, 150);
            gridViewMaster.TabIndex = 0;
            // 
            // gridViewExpansion
            // 
            gridViewExpansion.AllowUserToAddRows = false;
            gridViewExpansion.AllowUserToDeleteRows = false;
            gridViewExpansion.AllowUserToResizeRows = false;
            gridViewExpansion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewExpansion.Dock = DockStyle.Fill;
            gridViewExpansion.Location = new Point(0, 0);
            gridViewExpansion.Name = "gridViewExpansion";
            gridViewExpansion.ReadOnly = true;
            gridViewExpansion.RowHeadersVisible = false;
            gridViewExpansion.Size = new Size(240, 150);
            gridViewExpansion.TabIndex = 0;
            // 
            // MainForm
            // 
            ClientSize = new Size(1186, 729);
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VacX OutSense System Controller";
            tableLayoutPanelMain.ResumeLayout(false);
            tabControlMain.ResumeLayout(false);
            tabPageIOModule.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panelConnection.ResumeLayout(false);
            panelConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpdateInterval).EndInit();
            layout.ResumeLayout(false);
            controlPanel.ResumeLayout(false);
            controlPanel.PerformLayout();
            tabPageLog.ResumeLayout(false);
            tabPageLog.PerformLayout();
            tabPage1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewMaster).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewExpansion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        #region UI 컨트롤 변수

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageIOModule;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.DataGridView gridViewMaster;
        private System.Windows.Forms.DataGridView gridViewExpansion;

        private System.Windows.Forms.TextBox textBoxLogs;

        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusConnection;

        private MenuStrip menuStrip;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuFileExit;
        private ToolStripMenuItem menuComm;
        private ToolStripMenuItem menuCommSettings;
        private ToolStripMenuItem menuHelp;
        private ToolStripMenuItem menuHelpAbout;
        private StatusStrip statusStrip;

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel layout;
        private Panel controlPanel;
        private Label labelMasterRange;
        private ComboBox comboBoxMasterRange;
        private Label labelExpansionRange;
        private ComboBox comboBoxExpansionRange;
        private Button buttonSetRange;
        private TabPage tabPage1;
        private Panel panelConnection;
        private Label labelPort;
        private ComboBox comboBoxPorts;
        private Label labelBaudRate;
        private ComboBox comboBoxBaudRate;
        private Button buttonConnect;
        private Button buttonRefresh;
        private Label labelConnectionStatus;
        private Label labelDeviceInfo;
        private Label labelUpdateInterval;
        private NumericUpDown numericUpdateInterval;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Forms.UserControls.ConnectionIndicator connectionIndicator_turbopump;
        private TableLayoutPanel tableLayoutPanel4;
        private Forms.UserControls.ConnectionIndicator connectionIndicator_heater;
        private Forms.UserControls.ConnectionIndicator connectionIndicator_chiller;
        private Forms.UserControls.ConnectionIndicator connectionIndicator_relaymodule;
        private Forms.UserControls.ConnectionIndicator connectionIndicator_iomodule;
        private Forms.UserControls.ConnectionIndicator connectionIndicator_drypump;
        private Panel panel1;
        private Forms.UserControls.BindableTextBox bindableTextBox1;
        private Forms.UserControls.BindableTextBox bindableTextBox3;
        private Forms.UserControls.BindableTextBox bindableTextBox2;
        private Forms.UserControls.BindableTextBox bindableTextBox4;
        private Button btn_iongauge;
        private TableLayoutPanel tableLayoutPanel5;
        private Button btn_VV;
        private Button btn_EV;
        private Button btn_GV;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}