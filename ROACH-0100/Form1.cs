//#undef NOTHREADS

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ROACH_0100
{
    public partial class Form1 : Form
    {
        #region Constants
        /// <summary>
        /// Valor por defecto del tiempo de espera de los hilos(en milisegundos).
        /// </summary>
        const int DEFAULT_REFRESHRATE = 1;
        /// <summary>
        /// Valor por defecto del minimo valor en Y mostrado por la gráfica. 
        /// </summary>
        const int DEFAULT_RANGE_Y_MINIMUM = 0;
        /// <summary>
        /// Valor por defecto del maximo valor en Y mostrado por la gráfica.
        /// </summary>
        const int DEFAULT_RANGE_Y_MAXIMUM = 700;//Default: 1000
        /// <summary>
        /// Valor por defecto del maximo valor en X mostrado por la gráfica.
        /// </summary>
        const int DEFAULT_SAMPLINGTIME = 25000;//Default 25000

        /// <summary>
        /// Valor por defecto de la cantidad de datos que se saltarán en la recolección de datos.
        /// </summary>
        const int DEFAULT_SIMULATION_SKIPPEDDATANUMBER = 10;
        #endregion Constants

        #region Global Variables
        /// <summary>
        /// [Variable Global] Objeto que establece la comunicacion serial
        /// </summary>
        SerialUART uart;
        /// <summary>
        /// [Variable Global]
        /// </summary>
        FileWriter fileWriter;
        /// <summary>
        /// [Variable Global]Cola que almacena los datos recividos por la comunicacion serial y para la impresion.
        /// </summary>
        ConcurrentQueue<float> data = new ConcurrentQueue<float>();

        //List<float> datalist = new List<float>();//DEPRECATED: Lista para guardar los datos

        Stopwatch stopWatch_General = new Stopwatch();
        Stopwatch stopWatch_FilledChart = new Stopwatch();

        ///// <summary>
        ///// [Variable Global] Objeto que alamacena las varibles y metodos para la simulacion del modelo del rectificador por defecto.
        ///// </summary>
        //SIMDLLAccess simdllAcess = new SIMDLLAccess();

        /// <summary>
        /// [Variable Global] Objeto que permite la carga dinamica de DLLs.
        /// </summary>
        LinkerToUnamangedLibrary dllLinker;

        
        /// <summary>
        /// Tiempo de descanso de los hilos(En milisegundos).
        /// </summary>
        int refreshRate = DEFAULT_REFRESHRATE;
        /// <summary>
        /// [Variable Global] Valor minimo actual del rango en Y de la gráfica.
        /// </summary>
        int range_Y_Minimum = DEFAULT_RANGE_Y_MINIMUM;
        /// <summary>
        /// [Variable Global] Valor maximo actual del rango en Y de la gráfica.
        /// </summary>
        int range_Y_Maximum = DEFAULT_RANGE_Y_MAXIMUM;
        /// <summary>
        /// [Variable Global] Valor maximo actual del tiempo de muestreo(Valor maximo en el eje X de la gráfica).
        /// </summary>
        int samplingTime = DEFAULT_SAMPLINGTIME;

        /// <summary>
        /// [Variable Global] Valor de verificacion enviado al microcontrolador. Sirve para verificar que el dato haya sido recibido
        /// de forma satisfactoria.
        /// </summary>
        int current_checksum = 0;
        #endregion Global Variables

        public Form1()
        {
            InitializeComponent();
            
            Initialize_CommonControls();
            Initialize_Simulator();
            Initialize_EmulatorInterface();
        }

        #region Initializers
        /// <summary>
        /// Realiza las operaciones necesarias para la utilizacion de los controles comunes.
        /// </summary>
        public void Initialize_CommonControls()
        {
            #region MenuStrip
            #endregion MenuStrip

            #region StatusStrip
            #endregion StatusStrip

            #region Buttons
            #endregion Buttons

            Action<TrackBar, NumericUpDown> CopySliderProperties = (t, n) =>
            {
                n.Minimum = t.Minimum;
                n.Maximum = t.Maximum;
                n.Value = t.Value;
                n.Increment = t.SmallChange;
            };

            #region TrackBars and NumericUpDown
            //Tasa de actualizacion
            trackBar_RefreshRate.SetRange(1, 20);
            trackBar_RefreshRate.Value = refreshRate;
            trackBar_RefreshRate.SmallChange = 1;
            trackBar_RefreshRate.LargeChange = 5;
            CopySliderProperties(trackBar_RefreshRate, numericUpDown_RefreshRate);
            trackBar_RefreshRate.ValueChanged += new EventHandler(trackBar_ValueChanged);
            numericUpDown_RefreshRate.ValueChanged += new EventHandler(numericUpDown_ValueChanged);

            //Tiempo de muestreo
            trackBar_SamplingTime.SetRange(1000, 60000);
            trackBar_SamplingTime.Value = samplingTime;
            trackBar_SamplingTime.SmallChange = 250;
            trackBar_SamplingTime.LargeChange = 2500;
            CopySliderProperties(trackBar_SamplingTime, numericUpDown_SamplingTime);
            trackBar_SamplingTime.ValueChanged += new EventHandler(trackBar_ValueChanged);
            numericUpDown_SamplingTime.ValueChanged += new EventHandler(numericUpDown_ValueChanged);

            //Rango en Y de la grafica
            trackBar_RangeY_Minimum.SetRange(-1000, 1900); //trackBar_RangeY_Minimum.SetRange(-180, 1900);
            trackBar_RangeY_Minimum.Value = range_Y_Minimum;
            trackBar_RangeY_Minimum.SmallChange = 25;
            trackBar_RangeY_Minimum.LargeChange = 100;
            CopySliderProperties(trackBar_RangeY_Minimum, numericUpDown_RangeY_Minimum);
            trackBar_RangeY_Minimum.ValueChanged += new EventHandler(trackBar_ValueChanged);
            numericUpDown_RangeY_Minimum.ValueChanged += new EventHandler(numericUpDown_ValueChanged);

            trackBar_RangeY_Maximum.SetRange(-900, 2000); //trackBar_RangeY_Maximum.SetRange(-170, 2000);
            trackBar_RangeY_Maximum.Value = range_Y_Maximum;
            trackBar_RangeY_Maximum.SmallChange = 25;
            trackBar_RangeY_Maximum.LargeChange = 100;
            CopySliderProperties(trackBar_RangeY_Maximum, numericUpDown_RangeY_Maximum);
            trackBar_RangeY_Maximum.ValueChanged += new EventHandler(trackBar_ValueChanged);
            numericUpDown_RangeY_Maximum.ValueChanged += new EventHandler(numericUpDown_ValueChanged);
            #endregion

            #region Chart
            chart_DataOutput.Titles.Add("Datos recibidos");
            chart_DataOutput.Series.ElementAt<Series>(0).Name = "v_t";
            chart_DataOutput.Series.ElementAt<Series>(0).ChartType = SeriesChartType.FastPoint;
            chart_DataOutput.Series.ElementAt<Series>(0).BorderWidth = 3;

            chart_DataOutput.ChartAreas[0].AxisY.Minimum = range_Y_Minimum;
            chart_DataOutput.ChartAreas[0].AxisY.Maximum = range_Y_Maximum;
            chart_DataOutput.ChartAreas[0].AxisX.Minimum = 0;
            chart_DataOutput.ChartAreas[0].AxisX.Maximum = samplingTime;
            #endregion
        }

        /// <summary>
        /// Realiza las operaciones necesarias para la utilizacion del simulador.
        /// </summary>
        public void Initialize_Simulator()
        {
            #region Buttons
            button_Simulation_Start.Enabled = true;
            button_Simulation_Pause.Enabled = false;
            button_Simulation_Stop.Enabled = false;
            #endregion Buttons
                        
            #region ComboBoxes
            
            comboBox_Simulation_OutSignal.SelectedIndexChanged += 
                new EventHandler(comboBox_Simulation_OutSignal_SelectedIndexChanged);

            SetComboBoxAsNoEditable(comboBox_Simulation_OutSignal);
            #endregion ComboBoxes

            #region NumericsUpDowns
            numericUpDown_Simulation_NewValue.ValueChanged +=
                new EventHandler(numericUpDown_Simulation_NewValue_ValueChanged);
            #endregion NumericsUpDowns
        }

        /// <summary>
        /// Realiza las operaciones necesarias para la utilizacion de la interfaz del emulador.
        /// </summary>
        public void Initialize_EmulatorInterface()
        {
            #region Buttons
            //Habilitacion
            button_EmulatorInterface_Connect.Enabled = true;
            button_EmulatorInterface_Disconnect.Enabled = false;
            button_EmulatorInterface_SendCommand.Enabled = false;
            #endregion Buttons

            #region ComboBoxes
            //DataSources
            comboBox_EmulatorInterface_Ports.DataSource = SerialUART.GetPorts();
            comboBox_EmulatorInterface_BaudRates.DataSource = SerialUART.GetBaudRates();
            comboBox_EmulatorInterface_Parity.DataSource = SerialUART.GetParity();
            comboBox_EmulatorInterface_FlowControl.DataSource = SerialUART.GetFlowControl();
            comboBox_EmulatorInterface_StopBits.DataSource = SerialUART.GetStopBits();
            comboBox_EmulatorInterface_DataBits.DataSource = SerialUART.GetDataBits();

            //Indices por defecto
            comboBox_EmulatorInterface_BaudRates.SelectedIndex =
                comboBox_EmulatorInterface_BaudRates.FindStringExact("9600");
            comboBox_EmulatorInterface_Parity.SelectedIndex =
                comboBox_EmulatorInterface_Parity.FindStringExact("None");
            comboBox_EmulatorInterface_FlowControl.SelectedIndex =
                comboBox_EmulatorInterface_FlowControl.FindStringExact("None");
            comboBox_EmulatorInterface_StopBits.SelectedIndex =
                comboBox_EmulatorInterface_StopBits.FindStringExact("One");
            comboBox_EmulatorInterface_DataBits.SelectedIndex =
                comboBox_EmulatorInterface_DataBits.FindStringExact("8");

            //Comboboxes no editables
            SetComboBoxAsNoEditable(comboBox_EmulatorInterface_Ports,
                comboBox_EmulatorInterface_BaudRates,
                comboBox_EmulatorInterface_Parity,
                comboBox_EmulatorInterface_FlowControl,
                comboBox_EmulatorInterface_StopBits,
                comboBox_EmulatorInterface_DataBits);
            #endregion ComboBoxes

            #region Textboxes
            textBox_EmulatorInterface_ReceivedData.Multiline = true;
            #endregion
        }

        /// <summary>
        /// Establece la propiedad de los ComboBoxes en el argumento como DropDownList(No editable).
        /// </summary>
        /// <param name="comboBoxes"></param>
        public void SetComboBoxAsNoEditable(params ComboBox[] comboBoxes)
        {
            foreach(ComboBox item in comboBoxes)
                item.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        #endregion Initializers

        #region Miscellaneous Methods
        /// <summary>
        /// Invierte el estado de una bandera dada.
        /// </summary>
        /// <param name="flag">Estado a invertir.</param>
        public void InvertFlagState(ref bool flag)
        {
            flag = !flag;
        }

        /// <summary>
        /// Invierte el estado de dos banderas proporcionadas.
        /// </summary>
        /// <param name="flag0">Primera estado.</param>
        /// <param name="flag1">Segunda estado</param>
        public void InvertFlagState(ref bool flag0, ref bool flag1)
        {
            flag0 = !flag0;
            flag1 = !flag1;
        }

        /// <summary>
        /// Invierte el estado de tres banderas proporcionadas.
        /// </summary>
        /// <param name="flag0">Primera estado</param>
        /// <param name="flag1">Segunda estado</param>
        /// <param name="flag2">Tercer estado</param>
        public void InvertFlagState(ref bool flag0, ref bool flag1, ref bool flag2)
        {
            flag0 = !flag0;
            flag1 = !flag1;
            flag2 = !flag2;
        }

        /// <summary>
        /// Invierta el estado de la habilitacion de los botones.
        /// </summary>
        /// <param name="buttons">Botones a modificar.</param>
        public void InvertButtonsHabilitation(params Button[] buttons)
        {
            foreach (Button item in buttons)
            {
                item.Enabled = !item.Enabled;
            }
        }

        /// <summary>
        /// Invierta el estado de la habilitacion de los comboBoxes.
        /// </summary>
        /// <param name="comboBoxes">ComboBoxes a modificar.</param>
        public void InvertComboBoxHabilitation(params ComboBox[] comboBoxes)
        {
            foreach (ComboBox item in comboBoxes)
            {
                item.Enabled = !item.Enabled;
            }
        }

        /// <summary>
        /// Escribe el estado actual de la etiqueta de estado en la barra de estado.
        /// </summary>
        /// <param name="toolstriplabel">Objeto a modificar.</param>
        /// <param name="chain">Cadena a imprimir</param>
        public void ToolStripStatusLabel_Status_Write(ToolStripLabel toolstriplabel, string chain)
        {
            toolstriplabel.Text = "Estado: " + chain;
        }

        /// <summary>
        /// Escribe el estado actual de la etiqueta de tiempo transcurrido en la barra de estado.
        /// </summary>
        /// <param name="toolstriplabel">Objeto a modificar.</param>
        /// <param name="chain">Cadena a imprimir</param>
        public void ToolStripStatusLabel_Time_Write(ToolStripLabel toolstriplabel, string chain)
        {
            toolstriplabel.Text = "Tiempo transcurrido: " + chain + "s";
        }

        /// <summary>
        /// Escribe el estado actual de la etiqueta del tiempo que tarda en imprimirse por completo la grafica
        /// en la barra de estado.
        /// </summary>
        /// <param name="toolstriplabel">Objeto a modificar.</param>
        /// <param name="chain">Cadena a imprimir</param>
        public void ToolStripStatusLabel_ChartPrintingTime_Write(ToolStripLabel toolstriplabel, string chain)
        {
            toolstriplabel.Text = "Tiempo de impresion: " + chain + "ms";
        }

        /// <summary>
        /// Convierte una cadena de texto en un arreglo de Nytes.
        /// </summary>
        /// <param name="text">Cadena a convertir.</param>
        /// <returns></returns>
        public byte[] ConvertStringToByteArray(string text)
        {
            
            //Se revisa si la cadena termina en espacio, si no es asi se agrega uno
            //(Esto se hace para simplifaicar el algoritmo de conversion de cadena
            // a arreglo de comandos)
            if (text.Last<char>() != ' ')
                text += ' ';

            char[] string_buffer = text.ToCharArray();
            string next_number = "";
            byte[] command_buffer = new byte[8];
            int command_index = 0;

            //Algoritmo de conversion de cadena de texto a arreglo de bytes
            foreach (char item in string_buffer)
            {
                if (item != ' ')
                {
                    //Si no espacio se agrega el caracter a la cadena
                    next_number += item;
                }
                else
                {
                    //Si es una espacio, se convierte los caracteres concatenados en un Byte 
                    //y se agrega al indice correspondiente
                    command_buffer[command_index] = Convert.ToByte(next_number);
                    next_number = "";
                    command_index++;
                }
            }

            return command_buffer;
        }

        public delegate void PrintTextInTextBoxDelegate(TextBox textbox, string text, bool append);
        /// <summary>
        /// Cambia el valor del texto de un TextBox desde un hilo diferente al que se creo.
        /// </summary>
        /// <param name="textbox">Objeto a modificar.</param>
        /// <param name="text">Cadena a agregar.</param>
        /// <param name="append">Es modo anexable?</param>
        public void PrintTextInTextBox(TextBox textbox, string text, bool append)
        {
            if(textbox.InvokeRequired)
            {
                object[] args = new object[] { textbox, text, append };
                PrintTextInTextBoxDelegate pt = new PrintTextInTextBoxDelegate(PrintTextInTextBox);
                textbox.Invoke(pt, args);
            }
            else
            {
                if(append == true)
                    textbox.AppendText(text + " ");
                else
                    textbox.Text += text + " ";                
            }
        }
                
        public delegate void PrintTextInToolStripStatusLabelDelegate(ToolStrip toolstrip, string text);
        public void PrintTextInToolStripStatusLabel(ToolStrip toolstrip, string text)
        {
            if(toolstrip.InvokeRequired)
            {
                object[] args = new object[] { toolstrip, text };
                PrintTextInToolStripStatusLabelDelegate pt = 
                    new PrintTextInToolStripStatusLabelDelegate(PrintTextInToolStripStatusLabel);
                toolstrip.Invoke(pt, args);
            }
            else
            {
                toolstrip.Text = text;
            }
        }

        #endregion Miscellaneous Methods     

        

        

        
               

    }
}
