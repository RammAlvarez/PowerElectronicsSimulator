namespace ROACH_0100
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_Simulator = new System.Windows.Forms.TabPage();
            this.numericUpDown_Simulation_NewValue = new System.Windows.Forms.NumericUpDown();
            this.label_Simulation_NewValue = new System.Windows.Forms.Label();
            this.pictureBox_Simulation_State = new System.Windows.Forms.PictureBox();
            this.comboBox_Simulation_OutSignal = new System.Windows.Forms.ComboBox();
            this.label_Simulation_SignalToWatch = new System.Windows.Forms.Label();
            this.button_Simulation_Stop = new System.Windows.Forms.Button();
            this.button_Simulation_Pause = new System.Windows.Forms.Button();
            this.button_Simulation_Start = new System.Windows.Forms.Button();
            this.tabPage_EmulatorInterface = new System.Windows.Forms.TabPage();
            this.label_EmulatorInterface_DataBits = new System.Windows.Forms.Label();
            this.comboBox_EmulatorInterface_DataBits = new System.Windows.Forms.ComboBox();
            this.button_EmulatorInterface_SendCommand = new System.Windows.Forms.Button();
            this.textBox_EmulatorInterface_ReceivedData = new System.Windows.Forms.TextBox();
            this.label_EmulatorInterface_ReceivedData = new System.Windows.Forms.Label();
            this.textBox_EmulatorInterface_CommandToSend = new System.Windows.Forms.TextBox();
            this.label_EmulatorInterface_CommandToSend = new System.Windows.Forms.Label();
            this.comboBox_EmulatorInterface_StopBits = new System.Windows.Forms.ComboBox();
            this.label_EmulatorInterface_StopBits = new System.Windows.Forms.Label();
            this.comboBox_EmulatorInterface_FlowControl = new System.Windows.Forms.ComboBox();
            this.label_EmulatorInterface_FlowControl = new System.Windows.Forms.Label();
            this.comboBox_EmulatorInterface_Parity = new System.Windows.Forms.ComboBox();
            this.label_EmulatorInterface_Parity = new System.Windows.Forms.Label();
            this.comboBox_EmulatorInterface_BaudRates = new System.Windows.Forms.ComboBox();
            this.label_EmulatorInterface_BaudRate = new System.Windows.Forms.Label();
            this.comboBox_EmulatorInterface_Ports = new System.Windows.Forms.ComboBox();
            this.label_EmulatorInterface_Port = new System.Windows.Forms.Label();
            this.pictureBox_EmulatorInterface_ConnectionState = new System.Windows.Forms.PictureBox();
            this.button_EmulatorInterface_Disconnect = new System.Windows.Forms.Button();
            this.button_EmulatorInterface_Connect = new System.Windows.Forms.Button();
            this.label_EmulatorInterface_Connection = new System.Windows.Forms.Label();
            this.chart_DataOutput = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_ProgramStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ChartPrintingTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Exportar_GraficatoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_RefreshRate = new System.Windows.Forms.Label();
            this.label_SamplingTime = new System.Windows.Forms.Label();
            this.label_MaximumVerticalValue = new System.Windows.Forms.Label();
            this.label_MinimumVerticalValue = new System.Windows.Forms.Label();
            this.button_Chart_AutoSet = new System.Windows.Forms.Button();
            this.trackBar_RangeY_Maximum = new System.Windows.Forms.TrackBar();
            this.trackBar_SamplingTime = new System.Windows.Forms.TrackBar();
            this.trackBar_RefreshRate = new System.Windows.Forms.TrackBar();
            this.trackBar_RangeY_Minimum = new System.Windows.Forms.TrackBar();
            this.numericUpDown_RefreshRate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_SamplingTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_RangeY_Maximum = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_RangeY_Minimum = new System.Windows.Forms.NumericUpDown();
            this.button_Reset = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage_Simulator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Simulation_NewValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Simulation_State)).BeginInit();
            this.tabPage_EmulatorInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_EmulatorInterface_ConnectionState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_DataOutput)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RangeY_Maximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SamplingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RefreshRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RangeY_Minimum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RefreshRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SamplingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RangeY_Maximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RangeY_Minimum)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage_Simulator);
            this.tabControl.Controls.Add(this.tabPage_EmulatorInterface);
            this.tabControl.Location = new System.Drawing.Point(486, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(200, 481);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage_Simulator
            // 
            this.tabPage_Simulator.Controls.Add(this.numericUpDown_Simulation_NewValue);
            this.tabPage_Simulator.Controls.Add(this.label_Simulation_NewValue);
            this.tabPage_Simulator.Controls.Add(this.pictureBox_Simulation_State);
            this.tabPage_Simulator.Controls.Add(this.comboBox_Simulation_OutSignal);
            this.tabPage_Simulator.Controls.Add(this.label_Simulation_SignalToWatch);
            this.tabPage_Simulator.Controls.Add(this.button_Simulation_Stop);
            this.tabPage_Simulator.Controls.Add(this.button_Simulation_Pause);
            this.tabPage_Simulator.Controls.Add(this.button_Simulation_Start);
            this.tabPage_Simulator.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Simulator.Name = "tabPage_Simulator";
            this.tabPage_Simulator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Simulator.Size = new System.Drawing.Size(192, 455);
            this.tabPage_Simulator.TabIndex = 0;
            this.tabPage_Simulator.Text = "Simulador";
            this.tabPage_Simulator.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_Simulation_NewValue
            // 
            this.numericUpDown_Simulation_NewValue.Location = new System.Drawing.Point(6, 172);
            this.numericUpDown_Simulation_NewValue.Name = "numericUpDown_Simulation_NewValue";
            this.numericUpDown_Simulation_NewValue.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Simulation_NewValue.TabIndex = 11;
            this.numericUpDown_Simulation_NewValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_Simulation_NewValue.ValueChanged += new System.EventHandler(this.numericUpDown_Simulation_NewValue_ValueChanged);
            // 
            // label_Simulation_NewValue
            // 
            this.label_Simulation_NewValue.AutoSize = true;
            this.label_Simulation_NewValue.Location = new System.Drawing.Point(6, 155);
            this.label_Simulation_NewValue.Name = "label_Simulation_NewValue";
            this.label_Simulation_NewValue.Size = new System.Drawing.Size(66, 13);
            this.label_Simulation_NewValue.TabIndex = 10;
            this.label_Simulation_NewValue.Text = "Nuevo Valor";
            // 
            // pictureBox_Simulation_State
            // 
            this.pictureBox_Simulation_State.BackColor = System.Drawing.Color.Red;
            this.pictureBox_Simulation_State.Location = new System.Drawing.Point(106, 7);
            this.pictureBox_Simulation_State.Name = "pictureBox_Simulation_State";
            this.pictureBox_Simulation_State.Size = new System.Drawing.Size(80, 80);
            this.pictureBox_Simulation_State.TabIndex = 7;
            this.pictureBox_Simulation_State.TabStop = false;
            // 
            // comboBox_Simulation_OutSignal
            // 
            this.comboBox_Simulation_OutSignal.FormattingEnabled = true;
            this.comboBox_Simulation_OutSignal.Location = new System.Drawing.Point(7, 131);
            this.comboBox_Simulation_OutSignal.Name = "comboBox_Simulation_OutSignal";
            this.comboBox_Simulation_OutSignal.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Simulation_OutSignal.TabIndex = 2;
            // 
            // label_Simulation_SignalToWatch
            // 
            this.label_Simulation_SignalToWatch.AutoSize = true;
            this.label_Simulation_SignalToWatch.Location = new System.Drawing.Point(4, 114);
            this.label_Simulation_SignalToWatch.Name = "label_Simulation_SignalToWatch";
            this.label_Simulation_SignalToWatch.Size = new System.Drawing.Size(140, 13);
            this.label_Simulation_SignalToWatch.TabIndex = 1;
            this.label_Simulation_SignalToWatch.Text = "Señal de salida a monitorear";
            // 
            // button_Simulation_Stop
            // 
            this.button_Simulation_Stop.Enabled = false;
            this.button_Simulation_Stop.Location = new System.Drawing.Point(6, 64);
            this.button_Simulation_Stop.Name = "button_Simulation_Stop";
            this.button_Simulation_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Simulation_Stop.TabIndex = 0;
            this.button_Simulation_Stop.Text = "Detener";
            this.button_Simulation_Stop.UseVisualStyleBackColor = true;
            this.button_Simulation_Stop.Click += new System.EventHandler(this.button_Simulation_Stop_Click);
            // 
            // button_Simulation_Pause
            // 
            this.button_Simulation_Pause.Enabled = false;
            this.button_Simulation_Pause.Location = new System.Drawing.Point(6, 35);
            this.button_Simulation_Pause.Name = "button_Simulation_Pause";
            this.button_Simulation_Pause.Size = new System.Drawing.Size(75, 23);
            this.button_Simulation_Pause.TabIndex = 0;
            this.button_Simulation_Pause.Text = "Pausar";
            this.button_Simulation_Pause.UseVisualStyleBackColor = true;
            this.button_Simulation_Pause.Click += new System.EventHandler(this.button_Simulation_Pause_Click);
            // 
            // button_Simulation_Start
            // 
            this.button_Simulation_Start.Location = new System.Drawing.Point(6, 6);
            this.button_Simulation_Start.Name = "button_Simulation_Start";
            this.button_Simulation_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Simulation_Start.TabIndex = 0;
            this.button_Simulation_Start.Text = "Iniciar";
            this.button_Simulation_Start.UseVisualStyleBackColor = true;
            this.button_Simulation_Start.Click += new System.EventHandler(this.button_Simulation_Start_Click);
            // 
            // tabPage_EmulatorInterface
            // 
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_DataBits);
            this.tabPage_EmulatorInterface.Controls.Add(this.comboBox_EmulatorInterface_DataBits);
            this.tabPage_EmulatorInterface.Controls.Add(this.button_EmulatorInterface_SendCommand);
            this.tabPage_EmulatorInterface.Controls.Add(this.textBox_EmulatorInterface_ReceivedData);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_ReceivedData);
            this.tabPage_EmulatorInterface.Controls.Add(this.textBox_EmulatorInterface_CommandToSend);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_CommandToSend);
            this.tabPage_EmulatorInterface.Controls.Add(this.comboBox_EmulatorInterface_StopBits);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_StopBits);
            this.tabPage_EmulatorInterface.Controls.Add(this.comboBox_EmulatorInterface_FlowControl);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_FlowControl);
            this.tabPage_EmulatorInterface.Controls.Add(this.comboBox_EmulatorInterface_Parity);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_Parity);
            this.tabPage_EmulatorInterface.Controls.Add(this.comboBox_EmulatorInterface_BaudRates);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_BaudRate);
            this.tabPage_EmulatorInterface.Controls.Add(this.comboBox_EmulatorInterface_Ports);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_Port);
            this.tabPage_EmulatorInterface.Controls.Add(this.pictureBox_EmulatorInterface_ConnectionState);
            this.tabPage_EmulatorInterface.Controls.Add(this.button_EmulatorInterface_Disconnect);
            this.tabPage_EmulatorInterface.Controls.Add(this.button_EmulatorInterface_Connect);
            this.tabPage_EmulatorInterface.Controls.Add(this.label_EmulatorInterface_Connection);
            this.tabPage_EmulatorInterface.Location = new System.Drawing.Point(4, 22);
            this.tabPage_EmulatorInterface.Name = "tabPage_EmulatorInterface";
            this.tabPage_EmulatorInterface.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_EmulatorInterface.Size = new System.Drawing.Size(192, 455);
            this.tabPage_EmulatorInterface.TabIndex = 1;
            this.tabPage_EmulatorInterface.Text = "Interface a emulador";
            this.tabPage_EmulatorInterface.UseVisualStyleBackColor = true;
            // 
            // label_EmulatorInterface_DataBits
            // 
            this.label_EmulatorInterface_DataBits.AutoSize = true;
            this.label_EmulatorInterface_DataBits.Location = new System.Drawing.Point(106, 214);
            this.label_EmulatorInterface_DataBits.Name = "label_EmulatorInterface_DataBits";
            this.label_EmulatorInterface_DataBits.Size = new System.Drawing.Size(50, 13);
            this.label_EmulatorInterface_DataBits.TabIndex = 11;
            this.label_EmulatorInterface_DataBits.Text = "Data Bits";
            // 
            // comboBox_EmulatorInterface_DataBits
            // 
            this.comboBox_EmulatorInterface_DataBits.FormattingEnabled = true;
            this.comboBox_EmulatorInterface_DataBits.Location = new System.Drawing.Point(106, 230);
            this.comboBox_EmulatorInterface_DataBits.Name = "comboBox_EmulatorInterface_DataBits";
            this.comboBox_EmulatorInterface_DataBits.Size = new System.Drawing.Size(80, 21);
            this.comboBox_EmulatorInterface_DataBits.TabIndex = 10;
            // 
            // button_EmulatorInterface_SendCommand
            // 
            this.button_EmulatorInterface_SendCommand.Enabled = false;
            this.button_EmulatorInterface_SendCommand.Location = new System.Drawing.Point(7, 346);
            this.button_EmulatorInterface_SendCommand.Name = "button_EmulatorInterface_SendCommand";
            this.button_EmulatorInterface_SendCommand.Size = new System.Drawing.Size(75, 23);
            this.button_EmulatorInterface_SendCommand.TabIndex = 9;
            this.button_EmulatorInterface_SendCommand.Text = "Enviar";
            this.button_EmulatorInterface_SendCommand.UseVisualStyleBackColor = true;
            this.button_EmulatorInterface_SendCommand.Click += new System.EventHandler(this.button_EmulatorInterface_SendCommand_Click);
            // 
            // textBox_EmulatorInterface_ReceivedData
            // 
            this.textBox_EmulatorInterface_ReceivedData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_EmulatorInterface_ReceivedData.Location = new System.Drawing.Point(7, 388);
            this.textBox_EmulatorInterface_ReceivedData.Multiline = true;
            this.textBox_EmulatorInterface_ReceivedData.Name = "textBox_EmulatorInterface_ReceivedData";
            this.textBox_EmulatorInterface_ReceivedData.ReadOnly = true;
            this.textBox_EmulatorInterface_ReceivedData.Size = new System.Drawing.Size(179, 61);
            this.textBox_EmulatorInterface_ReceivedData.TabIndex = 8;
            // 
            // label_EmulatorInterface_ReceivedData
            // 
            this.label_EmulatorInterface_ReceivedData.AutoSize = true;
            this.label_EmulatorInterface_ReceivedData.Location = new System.Drawing.Point(7, 372);
            this.label_EmulatorInterface_ReceivedData.Name = "label_EmulatorInterface_ReceivedData";
            this.label_EmulatorInterface_ReceivedData.Size = new System.Drawing.Size(80, 13);
            this.label_EmulatorInterface_ReceivedData.TabIndex = 7;
            this.label_EmulatorInterface_ReceivedData.Text = "Datos recibidos";
            // 
            // textBox_EmulatorInterface_CommandToSend
            // 
            this.textBox_EmulatorInterface_CommandToSend.Location = new System.Drawing.Point(8, 281);
            this.textBox_EmulatorInterface_CommandToSend.Multiline = true;
            this.textBox_EmulatorInterface_CommandToSend.Name = "textBox_EmulatorInterface_CommandToSend";
            this.textBox_EmulatorInterface_CommandToSend.Size = new System.Drawing.Size(178, 59);
            this.textBox_EmulatorInterface_CommandToSend.TabIndex = 6;
            this.textBox_EmulatorInterface_CommandToSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_EmulatorInterface_CommandToSend_KeyPress);
            // 
            // label_EmulatorInterface_CommandToSend
            // 
            this.label_EmulatorInterface_CommandToSend.AutoSize = true;
            this.label_EmulatorInterface_CommandToSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmulatorInterface_CommandToSend.Location = new System.Drawing.Point(7, 264);
            this.label_EmulatorInterface_CommandToSend.Name = "label_EmulatorInterface_CommandToSend";
            this.label_EmulatorInterface_CommandToSend.Size = new System.Drawing.Size(93, 13);
            this.label_EmulatorInterface_CommandToSend.TabIndex = 5;
            this.label_EmulatorInterface_CommandToSend.Text = "Comando a enviar";
            // 
            // comboBox_EmulatorInterface_StopBits
            // 
            this.comboBox_EmulatorInterface_StopBits.FormattingEnabled = true;
            this.comboBox_EmulatorInterface_StopBits.Location = new System.Drawing.Point(6, 230);
            this.comboBox_EmulatorInterface_StopBits.Name = "comboBox_EmulatorInterface_StopBits";
            this.comboBox_EmulatorInterface_StopBits.Size = new System.Drawing.Size(81, 21);
            this.comboBox_EmulatorInterface_StopBits.TabIndex = 4;
            // 
            // label_EmulatorInterface_StopBits
            // 
            this.label_EmulatorInterface_StopBits.AutoSize = true;
            this.label_EmulatorInterface_StopBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmulatorInterface_StopBits.Location = new System.Drawing.Point(6, 214);
            this.label_EmulatorInterface_StopBits.Name = "label_EmulatorInterface_StopBits";
            this.label_EmulatorInterface_StopBits.Size = new System.Drawing.Size(48, 13);
            this.label_EmulatorInterface_StopBits.TabIndex = 3;
            this.label_EmulatorInterface_StopBits.Text = "Stop bits";
            // 
            // comboBox_EmulatorInterface_FlowControl
            // 
            this.comboBox_EmulatorInterface_FlowControl.FormattingEnabled = true;
            this.comboBox_EmulatorInterface_FlowControl.Location = new System.Drawing.Point(6, 188);
            this.comboBox_EmulatorInterface_FlowControl.Name = "comboBox_EmulatorInterface_FlowControl";
            this.comboBox_EmulatorInterface_FlowControl.Size = new System.Drawing.Size(180, 21);
            this.comboBox_EmulatorInterface_FlowControl.TabIndex = 4;
            // 
            // label_EmulatorInterface_FlowControl
            // 
            this.label_EmulatorInterface_FlowControl.AutoSize = true;
            this.label_EmulatorInterface_FlowControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmulatorInterface_FlowControl.Location = new System.Drawing.Point(6, 172);
            this.label_EmulatorInterface_FlowControl.Name = "label_EmulatorInterface_FlowControl";
            this.label_EmulatorInterface_FlowControl.Size = new System.Drawing.Size(77, 13);
            this.label_EmulatorInterface_FlowControl.TabIndex = 3;
            this.label_EmulatorInterface_FlowControl.Text = "Control de flujo";
            // 
            // comboBox_EmulatorInterface_Parity
            // 
            this.comboBox_EmulatorInterface_Parity.FormattingEnabled = true;
            this.comboBox_EmulatorInterface_Parity.Location = new System.Drawing.Point(6, 147);
            this.comboBox_EmulatorInterface_Parity.Name = "comboBox_EmulatorInterface_Parity";
            this.comboBox_EmulatorInterface_Parity.Size = new System.Drawing.Size(180, 21);
            this.comboBox_EmulatorInterface_Parity.TabIndex = 4;
            // 
            // label_EmulatorInterface_Parity
            // 
            this.label_EmulatorInterface_Parity.AutoSize = true;
            this.label_EmulatorInterface_Parity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmulatorInterface_Parity.Location = new System.Drawing.Point(6, 131);
            this.label_EmulatorInterface_Parity.Name = "label_EmulatorInterface_Parity";
            this.label_EmulatorInterface_Parity.Size = new System.Drawing.Size(43, 13);
            this.label_EmulatorInterface_Parity.TabIndex = 3;
            this.label_EmulatorInterface_Parity.Text = "Paridad";
            // 
            // comboBox_EmulatorInterface_BaudRates
            // 
            this.comboBox_EmulatorInterface_BaudRates.FormattingEnabled = true;
            this.comboBox_EmulatorInterface_BaudRates.Location = new System.Drawing.Point(106, 106);
            this.comboBox_EmulatorInterface_BaudRates.Name = "comboBox_EmulatorInterface_BaudRates";
            this.comboBox_EmulatorInterface_BaudRates.Size = new System.Drawing.Size(80, 21);
            this.comboBox_EmulatorInterface_BaudRates.TabIndex = 4;
            // 
            // label_EmulatorInterface_BaudRate
            // 
            this.label_EmulatorInterface_BaudRate.AutoSize = true;
            this.label_EmulatorInterface_BaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmulatorInterface_BaudRate.Location = new System.Drawing.Point(106, 90);
            this.label_EmulatorInterface_BaudRate.Name = "label_EmulatorInterface_BaudRate";
            this.label_EmulatorInterface_BaudRate.Size = new System.Drawing.Size(53, 13);
            this.label_EmulatorInterface_BaudRate.TabIndex = 3;
            this.label_EmulatorInterface_BaudRate.Text = "Baud rate";
            // 
            // comboBox_EmulatorInterface_Ports
            // 
            this.comboBox_EmulatorInterface_Ports.FormattingEnabled = true;
            this.comboBox_EmulatorInterface_Ports.Location = new System.Drawing.Point(7, 106);
            this.comboBox_EmulatorInterface_Ports.Name = "comboBox_EmulatorInterface_Ports";
            this.comboBox_EmulatorInterface_Ports.Size = new System.Drawing.Size(80, 21);
            this.comboBox_EmulatorInterface_Ports.TabIndex = 4;
            // 
            // label_EmulatorInterface_Port
            // 
            this.label_EmulatorInterface_Port.AutoSize = true;
            this.label_EmulatorInterface_Port.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmulatorInterface_Port.Location = new System.Drawing.Point(6, 89);
            this.label_EmulatorInterface_Port.Name = "label_EmulatorInterface_Port";
            this.label_EmulatorInterface_Port.Size = new System.Drawing.Size(38, 13);
            this.label_EmulatorInterface_Port.TabIndex = 3;
            this.label_EmulatorInterface_Port.Text = "Puerto";
            // 
            // pictureBox_EmulatorInterface_ConnectionState
            // 
            this.pictureBox_EmulatorInterface_ConnectionState.BackColor = System.Drawing.Color.Red;
            this.pictureBox_EmulatorInterface_ConnectionState.Location = new System.Drawing.Point(134, 23);
            this.pictureBox_EmulatorInterface_ConnectionState.Name = "pictureBox_EmulatorInterface_ConnectionState";
            this.pictureBox_EmulatorInterface_ConnectionState.Size = new System.Drawing.Size(52, 52);
            this.pictureBox_EmulatorInterface_ConnectionState.TabIndex = 2;
            this.pictureBox_EmulatorInterface_ConnectionState.TabStop = false;
            // 
            // button_EmulatorInterface_Disconnect
            // 
            this.button_EmulatorInterface_Disconnect.Enabled = false;
            this.button_EmulatorInterface_Disconnect.Location = new System.Drawing.Point(6, 52);
            this.button_EmulatorInterface_Disconnect.Name = "button_EmulatorInterface_Disconnect";
            this.button_EmulatorInterface_Disconnect.Size = new System.Drawing.Size(122, 23);
            this.button_EmulatorInterface_Disconnect.TabIndex = 1;
            this.button_EmulatorInterface_Disconnect.Text = "Desconectar";
            this.button_EmulatorInterface_Disconnect.UseVisualStyleBackColor = true;
            this.button_EmulatorInterface_Disconnect.Click += new System.EventHandler(this.button_EmulatorInterface_Disconnect_Click);
            // 
            // button_EmulatorInterface_Connect
            // 
            this.button_EmulatorInterface_Connect.Location = new System.Drawing.Point(6, 23);
            this.button_EmulatorInterface_Connect.Name = "button_EmulatorInterface_Connect";
            this.button_EmulatorInterface_Connect.Size = new System.Drawing.Size(122, 23);
            this.button_EmulatorInterface_Connect.TabIndex = 1;
            this.button_EmulatorInterface_Connect.Text = "Conectar";
            this.button_EmulatorInterface_Connect.UseVisualStyleBackColor = true;
            this.button_EmulatorInterface_Connect.Click += new System.EventHandler(this.button_EmulatorInterface_Connect_Click);
            // 
            // label_EmulatorInterface_Connection
            // 
            this.label_EmulatorInterface_Connection.AutoSize = true;
            this.label_EmulatorInterface_Connection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmulatorInterface_Connection.Location = new System.Drawing.Point(7, 7);
            this.label_EmulatorInterface_Connection.Name = "label_EmulatorInterface_Connection";
            this.label_EmulatorInterface_Connection.Size = new System.Drawing.Size(51, 13);
            this.label_EmulatorInterface_Connection.TabIndex = 0;
            this.label_EmulatorInterface_Connection.Text = "Conexión";
            // 
            // chart_DataOutput
            // 
            this.chart_DataOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart_DataOutput.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_DataOutput.Legends.Add(legend1);
            this.chart_DataOutput.Location = new System.Drawing.Point(12, 24);
            this.chart_DataOutput.Name = "chart_DataOutput";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_DataOutput.Series.Add(series1);
            this.chart_DataOutput.Size = new System.Drawing.Size(468, 481);
            this.chart_DataOutput.TabIndex = 1;
            this.chart_DataOutput.Text = "chart";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_ProgramStatus,
            this.toolStripStatusLabel_Time,
            this.toolStripStatusLabel_ChartPrintingTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 511);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(904, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel_ProgramStatus
            // 
            this.toolStripStatusLabel_ProgramStatus.Name = "toolStripStatusLabel_ProgramStatus";
            this.toolStripStatusLabel_ProgramStatus.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel_ProgramStatus.Text = "Estado: Listo";
            // 
            // toolStripStatusLabel_Time
            // 
            this.toolStripStatusLabel_Time.Name = "toolStripStatusLabel_Time";
            this.toolStripStatusLabel_Time.Size = new System.Drawing.Size(129, 17);
            this.toolStripStatusLabel_Time.Text = "Tiempo Transcurrido: 0";
            // 
            // toolStripStatusLabel_ChartPrintingTime
            // 
            this.toolStripStatusLabel_ChartPrintingTime.Name = "toolStripStatusLabel_ChartPrintingTime";
            this.toolStripStatusLabel_ChartPrintingTime.Size = new System.Drawing.Size(132, 17);
            this.toolStripStatusLabel_ChartPrintingTime.Text = "Tiempo de impresion: 0";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(904, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.Exportar_GraficatoolStripMenuItem,
            this.cerrarToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como...";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // Exportar_GraficatoolStripMenuItem
            // 
            this.Exportar_GraficatoolStripMenuItem.Name = "Exportar_GraficatoolStripMenuItem";
            this.Exportar_GraficatoolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.Exportar_GraficatoolStripMenuItem.Text = "Exportar Gráfica";
            this.Exportar_GraficatoolStripMenuItem.Click += new System.EventHandler(this.Exportar_GraficatoolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // label_RefreshRate
            // 
            this.label_RefreshRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_RefreshRate.AutoSize = true;
            this.label_RefreshRate.Location = new System.Drawing.Point(688, 50);
            this.label_RefreshRate.Name = "label_RefreshRate";
            this.label_RefreshRate.Size = new System.Drawing.Size(140, 13);
            this.label_RefreshRate.TabIndex = 7;
            this.label_RefreshRate.Text = "Frecuencia de actualización";
            // 
            // label_SamplingTime
            // 
            this.label_SamplingTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_SamplingTime.AutoSize = true;
            this.label_SamplingTime.Location = new System.Drawing.Point(692, 114);
            this.label_SamplingTime.Name = "label_SamplingTime";
            this.label_SamplingTime.Size = new System.Drawing.Size(103, 13);
            this.label_SamplingTime.TabIndex = 7;
            this.label_SamplingTime.Text = "Tiempo de muestreo";
            // 
            // label_MaximumVerticalValue
            // 
            this.label_MaximumVerticalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_MaximumVerticalValue.AutoSize = true;
            this.label_MaximumVerticalValue.Location = new System.Drawing.Point(692, 178);
            this.label_MaximumVerticalValue.Name = "label_MaximumVerticalValue";
            this.label_MaximumVerticalValue.Size = new System.Drawing.Size(106, 13);
            this.label_MaximumVerticalValue.TabIndex = 7;
            this.label_MaximumVerticalValue.Text = "Valor vertical máximo";
            // 
            // label_MinimumVerticalValue
            // 
            this.label_MinimumVerticalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_MinimumVerticalValue.AutoSize = true;
            this.label_MinimumVerticalValue.Location = new System.Drawing.Point(692, 242);
            this.label_MinimumVerticalValue.Name = "label_MinimumVerticalValue";
            this.label_MinimumVerticalValue.Size = new System.Drawing.Size(105, 13);
            this.label_MinimumVerticalValue.TabIndex = 7;
            this.label_MinimumVerticalValue.Text = "Valor vertical mínimo";
            // 
            // button_Chart_AutoSet
            // 
            this.button_Chart_AutoSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Chart_AutoSet.Location = new System.Drawing.Point(731, 306);
            this.button_Chart_AutoSet.Name = "button_Chart_AutoSet";
            this.button_Chart_AutoSet.Size = new System.Drawing.Size(75, 23);
            this.button_Chart_AutoSet.TabIndex = 9;
            this.button_Chart_AutoSet.Text = "Autoset";
            this.button_Chart_AutoSet.UseVisualStyleBackColor = true;
            this.button_Chart_AutoSet.Click += new System.EventHandler(this.button_Chart_AutoSet_Click);
            // 
            // trackBar_RangeY_Maximum
            // 
            this.trackBar_RangeY_Maximum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_RangeY_Maximum.Location = new System.Drawing.Point(688, 194);
            this.trackBar_RangeY_Maximum.Name = "trackBar_RangeY_Maximum";
            this.trackBar_RangeY_Maximum.Size = new System.Drawing.Size(143, 45);
            this.trackBar_RangeY_Maximum.TabIndex = 11;
            // 
            // trackBar_SamplingTime
            // 
            this.trackBar_SamplingTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_SamplingTime.Location = new System.Drawing.Point(688, 130);
            this.trackBar_SamplingTime.Name = "trackBar_SamplingTime";
            this.trackBar_SamplingTime.Size = new System.Drawing.Size(143, 45);
            this.trackBar_SamplingTime.TabIndex = 11;
            // 
            // trackBar_RefreshRate
            // 
            this.trackBar_RefreshRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_RefreshRate.Location = new System.Drawing.Point(688, 66);
            this.trackBar_RefreshRate.Name = "trackBar_RefreshRate";
            this.trackBar_RefreshRate.Size = new System.Drawing.Size(143, 45);
            this.trackBar_RefreshRate.TabIndex = 11;
            // 
            // trackBar_RangeY_Minimum
            // 
            this.trackBar_RangeY_Minimum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_RangeY_Minimum.Location = new System.Drawing.Point(688, 255);
            this.trackBar_RangeY_Minimum.Name = "trackBar_RangeY_Minimum";
            this.trackBar_RangeY_Minimum.Size = new System.Drawing.Size(143, 45);
            this.trackBar_RangeY_Minimum.TabIndex = 11;
            // 
            // numericUpDown_RefreshRate
            // 
            this.numericUpDown_RefreshRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_RefreshRate.Location = new System.Drawing.Point(837, 66);
            this.numericUpDown_RefreshRate.Name = "numericUpDown_RefreshRate";
            this.numericUpDown_RefreshRate.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_RefreshRate.TabIndex = 12;
            // 
            // numericUpDown_SamplingTime
            // 
            this.numericUpDown_SamplingTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_SamplingTime.Location = new System.Drawing.Point(837, 130);
            this.numericUpDown_SamplingTime.Name = "numericUpDown_SamplingTime";
            this.numericUpDown_SamplingTime.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_SamplingTime.TabIndex = 12;
            // 
            // numericUpDown_RangeY_Maximum
            // 
            this.numericUpDown_RangeY_Maximum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_RangeY_Maximum.Location = new System.Drawing.Point(837, 194);
            this.numericUpDown_RangeY_Maximum.Name = "numericUpDown_RangeY_Maximum";
            this.numericUpDown_RangeY_Maximum.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_RangeY_Maximum.TabIndex = 12;
            // 
            // numericUpDown_RangeY_Minimum
            // 
            this.numericUpDown_RangeY_Minimum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_RangeY_Minimum.Location = new System.Drawing.Point(837, 255);
            this.numericUpDown_RangeY_Minimum.Name = "numericUpDown_RangeY_Minimum";
            this.numericUpDown_RangeY_Minimum.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_RangeY_Minimum.TabIndex = 12;
            // 
            // button_Reset
            // 
            this.button_Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Reset.Location = new System.Drawing.Point(731, 336);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(75, 23);
            this.button_Reset.TabIndex = 13;
            this.button_Reset.Text = "RESET";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 533);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.numericUpDown_RangeY_Minimum);
            this.Controls.Add(this.numericUpDown_RangeY_Maximum);
            this.Controls.Add(this.numericUpDown_SamplingTime);
            this.Controls.Add(this.numericUpDown_RefreshRate);
            this.Controls.Add(this.trackBar_RefreshRate);
            this.Controls.Add(this.trackBar_SamplingTime);
            this.Controls.Add(this.trackBar_RangeY_Minimum);
            this.Controls.Add(this.trackBar_RangeY_Maximum);
            this.Controls.Add(this.button_Chart_AutoSet);
            this.Controls.Add(this.label_MinimumVerticalValue);
            this.Controls.Add(this.label_MaximumVerticalValue);
            this.Controls.Add(this.label_SamplingTime);
            this.Controls.Add(this.label_RefreshRate);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.chart_DataOutput);
            this.Controls.Add(this.tabControl);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ROACH";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPage_Simulator.ResumeLayout(false);
            this.tabPage_Simulator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Simulation_NewValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Simulation_State)).EndInit();
            this.tabPage_EmulatorInterface.ResumeLayout(false);
            this.tabPage_EmulatorInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_EmulatorInterface_ConnectionState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_DataOutput)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RangeY_Maximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SamplingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RefreshRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RangeY_Minimum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RefreshRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SamplingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RangeY_Maximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RangeY_Minimum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_Simulator;
        private System.Windows.Forms.TabPage tabPage_EmulatorInterface;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_DataOutput;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ProgramStatus;
        private System.Windows.Forms.PictureBox pictureBox_Simulation_State;
        private System.Windows.Forms.ComboBox comboBox_Simulation_OutSignal;
        private System.Windows.Forms.Label label_Simulation_SignalToWatch;
        private System.Windows.Forms.Button button_Simulation_Stop;
        private System.Windows.Forms.Button button_Simulation_Pause;
        private System.Windows.Forms.Button button_Simulation_Start;
        private System.Windows.Forms.PictureBox pictureBox_EmulatorInterface_ConnectionState;
        private System.Windows.Forms.Button button_EmulatorInterface_Disconnect;
        private System.Windows.Forms.Button button_EmulatorInterface_Connect;
        private System.Windows.Forms.Label label_EmulatorInterface_Connection;
        private System.Windows.Forms.Label label_RefreshRate;
        private System.Windows.Forms.Label label_SamplingTime;
        private System.Windows.Forms.Label label_MaximumVerticalValue;
        private System.Windows.Forms.Label label_MinimumVerticalValue;
        private System.Windows.Forms.Button button_Chart_AutoSet;
        private System.Windows.Forms.Button button_EmulatorInterface_SendCommand;
        private System.Windows.Forms.TextBox textBox_EmulatorInterface_ReceivedData;
        private System.Windows.Forms.Label label_EmulatorInterface_ReceivedData;
        private System.Windows.Forms.TextBox textBox_EmulatorInterface_CommandToSend;
        private System.Windows.Forms.Label label_EmulatorInterface_CommandToSend;
        private System.Windows.Forms.ComboBox comboBox_EmulatorInterface_BaudRates;
        private System.Windows.Forms.Label label_EmulatorInterface_BaudRate;
        private System.Windows.Forms.ComboBox comboBox_EmulatorInterface_Ports;
        private System.Windows.Forms.Label label_EmulatorInterface_Port;
        private System.Windows.Forms.ComboBox comboBox_EmulatorInterface_StopBits;
        private System.Windows.Forms.Label label_EmulatorInterface_StopBits;
        private System.Windows.Forms.ComboBox comboBox_EmulatorInterface_FlowControl;
        private System.Windows.Forms.Label label_EmulatorInterface_FlowControl;
        private System.Windows.Forms.ComboBox comboBox_EmulatorInterface_Parity;
        private System.Windows.Forms.Label label_EmulatorInterface_Parity;
        private System.Windows.Forms.TrackBar trackBar_RangeY_Maximum;
        private System.Windows.Forms.TrackBar trackBar_SamplingTime;
        private System.Windows.Forms.TrackBar trackBar_RefreshRate;
        private System.Windows.Forms.TrackBar trackBar_RangeY_Minimum;
        private System.Windows.Forms.NumericUpDown numericUpDown_RefreshRate;
        private System.Windows.Forms.NumericUpDown numericUpDown_SamplingTime;
        private System.Windows.Forms.NumericUpDown numericUpDown_RangeY_Maximum;
        private System.Windows.Forms.NumericUpDown numericUpDown_RangeY_Minimum;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Label label_EmulatorInterface_DataBits;
        private System.Windows.Forms.ComboBox comboBox_EmulatorInterface_DataBits;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Time;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ChartPrintingTime;
        private System.Windows.Forms.NumericUpDown numericUpDown_Simulation_NewValue;
        private System.Windows.Forms.Label label_Simulation_NewValue;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Exportar_GraficatoolStripMenuItem;
    }
}

