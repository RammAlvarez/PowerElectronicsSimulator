//#define ONLYUI
//#undef ONLYUI


using Ramm;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ROACH_0100
{
    public partial class Form1 : Form
    {
        Thread thread_DataReception_UART, thread_DataReception_DLL, 
            thread_ChartPrinting, thread_TextPrinting;

        Form_NewProject newProject;

        UIConfiguration uiConfiguration;

        FileManagement fileManagement = new FileManagement();

        #region Menus
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newProject = new Form_NewProject(this);
            newProject.ShowDialog();

            //Se obtiene la informacion de la ventana de nuevo
            if(newProject.OriginalValues != null && newProject.DllFilePath != null && newProject.DllFilePath != ""
                && newProject.MethodName != null && newProject.MethodName != "" 
                && newProject.ProjectName != null && newProject.ProjectName != "")
            {
                dllLinker = new LinkerToUnamangedLibrary(newProject.OriginalValues.Count,
                    newProject.DllFilePath, newProject.MethodName);
                dllLinker.AssignValues(newProject.OriginalValues.Values.ToArray<float>());
                dllLinker.ParamsNames = newProject.OriginalValues.Keys.ToList<string>();
            }
        }
                
        public void UploadConfiguration()
        {
            numericUpDown_RefreshRate.Value = uiConfiguration.UIParams.RefreshRate;
            numericUpDown_RangeY_Minimum.Value = uiConfiguration.UIParams.Range_Y_Minimum;
            numericUpDown_RangeY_Maximum.Value = uiConfiguration.UIParams.Range_Y_Maximum;
            numericUpDown_SamplingTime.Value = uiConfiguration.UIParams.SamplingTime;

            comboBox_EmulatorInterface_Ports.SelectedIndex = 
                comboBox_EmulatorInterface_Ports.FindString(uiConfiguration.UartParams.PortName);
            comboBox_EmulatorInterface_BaudRates.SelectedIndex =
                comboBox_EmulatorInterface_BaudRates.FindString(uiConfiguration.UartParams.BaudRate.ToString());
            comboBox_EmulatorInterface_FlowControl.SelectedIndex =
                comboBox_EmulatorInterface_FlowControl.FindString(uiConfiguration.UartParams.FlowControl);
            comboBox_EmulatorInterface_Parity.SelectedIndex =
                comboBox_EmulatorInterface_Parity.FindString(uiConfiguration.UartParams.Parity);
            comboBox_EmulatorInterface_DataBits.SelectedIndex =
                comboBox_EmulatorInterface_DataBits.FindString(uiConfiguration.UartParams.DataBits.ToString());
            comboBox_EmulatorInterface_StopBits.SelectedIndex =
                comboBox_EmulatorInterface_StopBits.FindString(uiConfiguration.UartParams.StopBits);

            dllLinker = new LinkerToUnamangedLibrary(uiConfiguration.dllLinker.ParamsQuantity,
                    uiConfiguration.dllLinker.FilePath, uiConfiguration.dllLinker.MethodName);
            dllLinker.AssignValues(uiConfiguration.dllLinker.ParamsValues.ToArray<float>());
            dllLinker.ParamsNames = uiConfiguration.dllLinker.ParamsNames.ToList<string>();
                        
            comboBox_Simulation_OutSignal.DataSource = dllLinker.ParamsNames;
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "XML | *.xml";
            openDialog.ShowDialog();

            if(openDialog.FileName != null && openDialog.FileName != "")
            {
                //Se recupera la informacion
                uiConfiguration = fileManagement.OpenXMLProject<UIConfiguration>(openDialog.FileName);
                //Se inicializan las variables
                UploadConfiguration();                        
            }
        }

        private void SaveConfiguration()
        {
            uiConfiguration = new UIConfiguration();

            //Controles comunes
            uiConfiguration.UIParams.RefreshRate = (int)numericUpDown_RefreshRate.Value;
            uiConfiguration.UIParams.Range_Y_Minimum = (int)numericUpDown_RangeY_Minimum.Value;
            uiConfiguration.UIParams.Range_Y_Maximum = (int)numericUpDown_RangeY_Maximum.Value;
            uiConfiguration.UIParams.SamplingTime = (int)numericUpDown_SamplingTime.Value;

            //UART
            uiConfiguration.UartParams.PortName = comboBox_EmulatorInterface_Ports.SelectedItem.ToString();
            uiConfiguration.UartParams.BaudRate =
                Convert.ToInt32(comboBox_EmulatorInterface_BaudRates.SelectedItem.ToString());
            uiConfiguration.UartParams.FlowControl =
                comboBox_EmulatorInterface_FlowControl.SelectedItem.ToString();
            uiConfiguration.UartParams.Parity = comboBox_EmulatorInterface_Parity.SelectedItem.ToString();
            uiConfiguration.UartParams.DataBits =
                Convert.ToInt32(comboBox_EmulatorInterface_DataBits.SelectedItem.ToString());
            uiConfiguration.UartParams.StopBits = comboBox_EmulatorInterface_StopBits.SelectedItem.ToString();
            uiConfiguration.dllLinker = dllLinker;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfiguration();
                fileManagement.Save<UIConfiguration>(uiConfiguration, newProject.ProjectName);
                ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Proyecto guardado.");
                Debug.WriteLine("Se guardo el proyecto.");
            }
            catch(Exception ex)
            {
                string windowTitle = "Error al guardar";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfiguration();
                fileManagement.SaveXMLAs<UIConfiguration>(uiConfiguration, newProject.ProjectName);
                ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Proyecto guardado.");
                Debug.WriteLine("Se guardo el proyecto.");
            }
            catch(Exception ex)
            {
                string windowTitle = "Error al guardar como";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void Exportar_GraficatoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPointCollection data = chart_DataOutput.Series[0].Points;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV | *.csv";
            dialog.Title = "Exportar";
            dialog.ShowDialog();

            using(FileWriter writer = new FileWriter(dialog.FileName))
            {
                int i = 0;
                
                StringBuilder sb = new StringBuilder();

                foreach (DataPoint item in data)
                {
                    sb.AppendLine(i++ + "," + item.YValues.GetValue(0));                    
                }

                writer.WriteLine(sb.ToString());
                writer.Flush();
                data.Dispose();
            }

            ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Gráfica exportada");
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvertFlagState(ref flag_Thread_DataReception_DLL_Enabled, ref flag_Thread_Printing_Enabled);           
            
            this.Close();
        }
        #endregion Menus

        #region Common controls

        /// <summary>
        /// Ocurre cuando alguno de los TrackBars comunes cambia su valor.
        /// </summary>
        /// <param name="sender">TrackBar origen.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TrackBar currentTrackBar = (TrackBar)sender;

                if (currentTrackBar.Name == trackBar_RefreshRate.Name)
                {
                    numericUpDown_RefreshRate.Value = refreshRate = currentTrackBar.Value;
                }
                else if (currentTrackBar.Name == trackBar_SamplingTime.Name)
                {
                    numericUpDown_SamplingTime.Value = samplingTime = currentTrackBar.Value;                    
                    chart_DataOutput.ChartAreas[0].AxisX.Maximum = samplingTime;
                }
                else if (currentTrackBar.Name == trackBar_RangeY_Maximum.Name)
                {
                    numericUpDown_RangeY_Maximum.Value = range_Y_Maximum = currentTrackBar.Value;                    
                    chart_DataOutput.ChartAreas[0].AxisY.Maximum = range_Y_Maximum;
                }
                else if (currentTrackBar.Name == trackBar_RangeY_Minimum.Name)
                {
                    numericUpDown_RangeY_Minimum.Value = range_Y_Minimum = currentTrackBar.Value;
                    chart_DataOutput.ChartAreas[0].AxisY.Minimum = range_Y_Minimum;
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "TrackBar: Error actualizando cambios";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// Ocurre cuando alguno de los NumericUpDown cambia su valor.
        /// </summary>
        /// <param name="sender">TrackBar origen.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown currentNumericUpDown = (NumericUpDown)sender;

                if (currentNumericUpDown.Name == numericUpDown_RefreshRate.Name)
                {
                    trackBar_RefreshRate.Value = refreshRate = (int)currentNumericUpDown.Value;
                }
                else if (currentNumericUpDown.Name == numericUpDown_SamplingTime.Name)
                {
                    trackBar_SamplingTime.Value = samplingTime = (int)currentNumericUpDown.Value;
                }
                else if (currentNumericUpDown.Name == numericUpDown_RangeY_Maximum.Name)
                {
                    trackBar_RangeY_Maximum.Value = range_Y_Maximum = (int)currentNumericUpDown.Value;
                }
                else if (currentNumericUpDown.Name == numericUpDown_RangeY_Minimum.Name)
                {
                    trackBar_RangeY_Minimum.Value = range_Y_Minimum = (int)currentNumericUpDown.Value;
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "NumericUpDown: Error actualizando cambios";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Ajusta la gráfica automaticamente dependiendo de los datos impresos en ella.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Chart_AutoSet_Click(object sender, EventArgs e)
        {
            try
            {
                if( chart_DataOutput.Series.ElementAt<Series>(0).Points.Count != 0)
                {
                    //Obtenemos los puntos maximos y minimos de la grafica
                    DataPoint pointY_Max, pointY_Min;
                    pointY_Max = chart_DataOutput.Series.ElementAt<Series>(0).Points.FindMaxByValue();
                    pointY_Min = chart_DataOutput.Series.ElementAt<Series>(0).Points.FindMinByValue();

                    //Definimos un espacio extra en porcentaje que le agregaremos a la grafica
                    const double EXTRASPACE = 0.05;

                    //Aplicamos los cambios a la grafica
                    //chart_DataOutput.ChartAreas[0].AxisX.Maximum = samplingTime = DEFAULT_SAMPLINGTIME;//DEPRECATED: Ya no se cambiara el eje Y 
                    chart_DataOutput.ChartAreas[0].AxisY.Maximum = range_Y_Maximum = 
                        (int)(pointY_Max.YValues[0] * (1 + EXTRASPACE));

                    Func<int> FindMinimumValue = () =>
                        {
                            if ((int)pointY_Min.YValues[0] == 0)
                                return -(int)(pointY_Max.YValues[0]*EXTRASPACE);
                            else
                                return (int)(pointY_Min.YValues[0] * (1 - EXTRASPACE));
                        };
                    chart_DataOutput.ChartAreas[0].AxisY.Minimum = range_Y_Minimum = FindMinimumValue();

                    //Reflejamos los cambios en los controles
                    numericUpDown_SamplingTime.Value = samplingTime;
                    numericUpDown_RangeY_Maximum.Value = range_Y_Maximum;
                    numericUpDown_RangeY_Minimum.Value = range_Y_Minimum;
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "Chart: Error ajustando la gráfica";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Se reinicia la gráfica a su estado original.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                //[NOTE] No hay que cambiar los Trackbars por que al cambiar los valores de estos controles
                //      se levanta el evento que se encarga de reflejar estos valores.
                numericUpDown_RefreshRate.Value = DEFAULT_REFRESHRATE;
                numericUpDown_SamplingTime.Value = DEFAULT_SAMPLINGTIME;
                numericUpDown_RangeY_Maximum.Value = DEFAULT_RANGE_Y_MAXIMUM;
                numericUpDown_RangeY_Minimum.Value = DEFAULT_RANGE_Y_MINIMUM;
            }
            catch (Exception ex)
            {
                string windowTitle = "Error de reestablecimiento";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Realiza las acciones pertinentes antes de cerrar el programa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                flag_Thread_Printing_Enabled = false;
                flag_Thread_DataReception_DLL_Enabled = false;
                flag_Thread_DataReception_UART_Enabled = false;

                Thread.Sleep(100);

                if (uart != null) uart.Dispose();
                if (fileWriter != null) fileWriter.Dispose();      
            }
            catch (Exception ex)
            {
                string windowTitle = "Error cerrando la ventana";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                  
        }
        #endregion Common controls 
                
        #region Simulator controls 
        /// <summary>
        /// Realiza las acciones pertinentes para iniciar la simulación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Simulation_Start_Click(object sender, EventArgs e)
        {            
            try
            {
                if (dllLinker != null)
                {
                    #region User Interface
                    pictureBox_Simulation_State.BackColor = Color.Lime;

                    ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Simulación iniciada...");
                
                    simulator_SelectedSignal = comboBox_Simulation_OutSignal.SelectedItem.ToString();
                    #endregion

                    #region Flags
                    InvertFlagState(ref flag_Thread_DataReception_DLL_Enabled, ref flag_Thread_Printing_Enabled);
                    flag_Thread_Printing_IsPaused = false;
                    InvertButtonsHabilitation(button_Simulation_Start, button_Simulation_Pause, button_Simulation_Stop);
                    button_EmulatorInterface_Connect.Enabled = false;
                    #endregion

#if !ONLYUI //Si "ONLYUI" esta definido

#region Fixed Initialization
                //dllLinker = new LinkerToUnamangedLibrary(48,
                //    @"C:\Users\Ramm\Documents\Visual Studio 2013\Projects\Tesis\ROACH-0100-Template\Debug\SIMDLL.dll",
                //    "SteppedSimulation");

                ////Asignamos los valores
                //dllLinker.AssignValues(new float[] { 
                //    0.0f, //evaltime
                //    0.1666e-4f, //timeStep
                //    70.0f, //vt_des
                //    0.0f, //vd_des
                //    0.0f, //v_s
                //    34.0f, //vs_amplitude
                //    377.0f, //vs_frequency (120*pi ~= 377 rad/s)
                //    100.0f, //r1
                //    100.0f, //r2
                //    470.0e-6f, //C
                //    10e-3f, //L
                //    10.0f, //v_t
                //    0.0f, //v_d
                //    1.0f, //i_s
                //    0.0f, //v_t_former
                //    0.0f, //v_d_former                     
                //    0.0f, //i_s_former
                //    0.0f, //Dv_t
                //    0.0f, //Dv_d
                //    0.0f, //Di_s
                //    0.0f, //eta
                //    0.0f, //eps
                //    0.0f, //u1
                //    0.0f, //u2
                //    0.0f, //eta_former
                //    0.0f, //eps_former
                //    0.0f, //i_s_des_out
                //    0.0f, //v_t_error_out
                //    0.0f, //v_d_error_out
                //    0.0f, //Deta
                //    0.0f, //Deps
                //    100000.0f, //k
                //    0.01f, //kp1
                //    250.0f, //kp2
                //    10.0f, //ki1
                //    500.0f, //ki2
                //    0.0f, //beta1
                //    0.0f, //beta2
                //    0.0f, //g_error1
                //    0.0f, //g_error2
                //    0.0f, //g_error1_former
                //    0.0f, //g_error2_former
                //    0.0f, //Dg_error1
                //    0.0f, //Dg_error2
                //    0.0f, //g1_Observer
                //    0.0f, //g2_Observer
                //    100e-5f, //gamma1
                //    100e-5f //gamma2
                //});

                ////Asignamos los nombres
                //dllLinker.ParamsNames.Add("eval_time"); dllLinker.ParamsNames.Add("timeStep");
                //dllLinker.ParamsNames.Add("vt_des"); dllLinker.ParamsNames.Add("vd_des");
                //dllLinker.ParamsNames.Add("v_s"); dllLinker.ParamsNames.Add("vs_amplitude");
                //dllLinker.ParamsNames.Add("vs_frequency"); dllLinker.ParamsNames.Add("r1");
                //dllLinker.ParamsNames.Add("r2"); dllLinker.ParamsNames.Add("C");
                //dllLinker.ParamsNames.Add("L"); dllLinker.ParamsNames.Add("v_t");
                //dllLinker.ParamsNames.Add("v_d"); dllLinker.ParamsNames.Add("i_s");
                //dllLinker.ParamsNames.Add("v_t_former"); dllLinker.ParamsNames.Add("v_d_former");
                //dllLinker.ParamsNames.Add("i_s_former"); dllLinker.ParamsNames.Add("Dv_t");
                //dllLinker.ParamsNames.Add("Dv_d"); dllLinker.ParamsNames.Add("Di_s");
                //dllLinker.ParamsNames.Add("eta"); dllLinker.ParamsNames.Add("eps");
                //dllLinker.ParamsNames.Add("u1"); dllLinker.ParamsNames.Add("u2");
                //dllLinker.ParamsNames.Add("eta_former"); dllLinker.ParamsNames.Add("eps_former");
                //dllLinker.ParamsNames.Add("i_s_des_out"); dllLinker.ParamsNames.Add("v_t_error_out");
                //dllLinker.ParamsNames.Add("v_d_error_out"); dllLinker.ParamsNames.Add("Deta");
                //dllLinker.ParamsNames.Add("Deps"); dllLinker.ParamsNames.Add("k");
                //dllLinker.ParamsNames.Add("kp1"); dllLinker.ParamsNames.Add("kp2");
                //dllLinker.ParamsNames.Add("ki1"); dllLinker.ParamsNames.Add("ki2");
                //dllLinker.ParamsNames.Add("beta1"); dllLinker.ParamsNames.Add("beta2");
                //dllLinker.ParamsNames.Add("g_error1"); dllLinker.ParamsNames.Add("g_error2");
                //dllLinker.ParamsNames.Add("g_error1_former"); dllLinker.ParamsNames.Add("g_error2_former");
                //dllLinker.ParamsNames.Add("Dg_error1"); dllLinker.ParamsNames.Add("Dg_error2");
                //dllLinker.ParamsNames.Add("g1_Observer"); dllLinker.ParamsNames.Add("g2_Observer");
                //dllLinker.ParamsNames.Add("gamma1"); dllLinker.ParamsNames.Add("gamma2");
#endregion 
                                       

                    //Se inicializan los hilos
                    thread_DataReception_DLL = new Thread(new ThreadStart(Thread_DataReception_DLL));
                    thread_DataReception_DLL.IsBackground = true;
                    thread_DataReception_DLL.Priority = ThreadPriority.Highest;

                    thread_ChartPrinting = new Thread(new ThreadStart(Thread_ChartPrinting));
                    thread_ChartPrinting.IsBackground = true;
                    thread_ChartPrinting.Priority = ThreadPriority.Highest;

                    thread_TextPrinting = new Thread(new ThreadStart(Thread_TextPrinting));
                    thread_TextPrinting.IsBackground = true;
                    thread_TextPrinting.Priority = ThreadPriority.Lowest;

                    thread_DataReception_DLL.Start();
                    thread_ChartPrinting.Start();
                    thread_TextPrinting.Start();
#endif
                }

                #region Debug & Tracing
                Debug.WriteLine("Simulacion iniciada.");
                #endregion
            }
            catch (Exception ex)
            {
                string windowTitle = "Error inicializando la simulación";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Realiza las acciones pertinentes para pausar la simulación por un tiempo indefinido.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Simulation_Pause_Click(object sender, EventArgs e)
        {
            try
            {
                #region User Interface
                if (pictureBox_Simulation_State.BackColor != Color.Orange)
                    pictureBox_Simulation_State.BackColor = Color.Orange;
                else
                    pictureBox_Simulation_State.BackColor = Color.Lime;                
                #endregion

                #region Flags
                InvertFlagState(ref flag_Thread_DataReception_DLL_IsPaused, ref flag_Thread_Printing_IsPaused);
                ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Simulación pausada");
                #endregion

                #region Debug & Tracing
                Debug.WriteLine("Simulacion Pausada.");
                #endregion
            }
            catch (Exception ex)
            {
                string windowTitle = "Error pausando la simulación";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Realiza las acciones pertinentes para detener la simulación y luego reinicia las variables a sus
        /// valores originales.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Simulation_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                #region User Interface
                pictureBox_Simulation_State.BackColor = Color.Red;

                ToolStripStatusLabel_Time_Write(toolStripStatusLabel_Time, "0");
                ToolStripStatusLabel_ChartPrintingTime_Write(toolStripStatusLabel_ChartPrintingTime, "0");
                ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Simulación detenida");
                #endregion

                #region Flags
                InvertFlagState(ref flag_Thread_DataReception_DLL_Enabled, ref flag_Thread_Printing_Enabled);
                button_EmulatorInterface_Connect.Enabled = true;
                InvertButtonsHabilitation(button_Simulation_Start, button_Simulation_Pause, button_Simulation_Stop);

                flag_Thread_DataReception_DLL_IsPaused = flag_Thread_Printing_IsPaused = false;
                #endregion
                
                #region Reset
                dllLinker.ResetValues();
                ChartPrinting.Erase(chart_DataOutput, ref data);
                #endregion

                #region Debug & Tracing
                Debug.WriteLine("Simulacion detenida.");
                #endregion
            }
            catch (Exception ex)
            {
                string windowTitle = "Error pausando la simulación";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cambia la señal de salida a mostar en la gráfica.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Simulation_OutSignal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChartPrinting.Erase(chart_DataOutput, ref flag_Thread_Printing_IsPaused);
                simulator_SelectedSignal = comboBox_Simulation_OutSignal.SelectedItem.ToString();
                chart_DataOutput.Series.ElementAt<Series>(0).Name = simulator_SelectedSignal;
            }
            catch(Exception ex)
            {
                string windowTitle = "Simualador: Error cambiando la señal";
                MessageBox.Show(ex.Message + "\r\n Event: comboBox_Simulation_OutSignal_SelectedIndexChanged", 
                    windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        ///// <summary>
        ///// Cambia las señales a mostrar, ya sean las señales de salida o las señales de control(secundarias).
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void checkBox_Simulation_SeeControlSignals_CheckedChanged(object sender, EventArgs e)
        //{            
        //    try
        //    {
        //        ChartPrinting.Erase(chart_DataOutput, ref flag_Thread_Printing_IsPaused);//HACK: Porque se pausa el hilo de impresion?

        //        InvertComboBoxHabilitation(comboBox_Simulation_OutSignal, comboBox_Simulation_OtherSignal);

        //        if (checkBox_Simulation_SeeOtherSignals.Checked)
        //        {
        //            simulator_SelectedSignal = comboBox_Simulation_OtherSignal.SelectedItem.ToString();
        //            chart_DataOutput.Series.ElementAt<Series>(0).Name = simulator_SelectedSignal;
        //        }
        //        else
        //        {
        //            simulator_SelectedSignal = comboBox_Simulation_OutSignal.SelectedItem.ToString();
        //            chart_DataOutput.Series.ElementAt<Series>(0).Name = simulator_SelectedSignal;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string windowTitle = "Simulador: Error cambiando la señal";
        //        MessageBox.Show(ex.Message + "\r\n Event: checkBox_Simulation_SeeControlSignals_CheckedChanged", 
        //            windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        ///// <summary>
        ///// Cambia la señal de control(secundaria) a mostar en la gráfica.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void comboBox_Simulation_OtherSignal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ChartPrinting.Erase(chart_DataOutput, ref flag_Thread_Printing_IsPaused);//HACK: Porque se pausa el hilo de impresion?
        //        simulator_SelectedSignal = comboBox_Simulation_OtherSignal.SelectedItem.ToString();
        //        chart_DataOutput.Series.ElementAt<Series>(0).Name = simulator_SelectedSignal;
        //    }
        //    catch (Exception ex)
        //    {
        //        string windowTitle = "Simulador: Error cambiando la señal";
        //        MessageBox.Show(ex.Message + "\r\n Event: comboBox_Simulation_OtherSignal_SelectedIndexChanged", 
        //            windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        

        private void numericUpDown_Simulation_NewValue_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dllLinker.ChangeAValue(comboBox_Simulation_OutSignal.SelectedItem.ToString(),
                    (float)numericUpDown_Simulation_NewValue.Value);
            }
            catch (Exception ex)
            {
                string windowTitle = "Simulador: Error cambiando el valor de la señal";
                MessageBox.Show(ex.Message + "\r\n Event: comboBox_Simulation_OtherSignal_SelectedIndexChanged",
                    windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Simulator controls 
                
        #region Emulator interface controls 
        /// <summary>
        /// Realiza las acciones pertinentes para iniciar la conexión UART y recibir datos para mostrar en pantalla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_EmulatorInterface_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox_EmulatorInterface_ConnectionState.BackColor = Color.Lime;

                uart = new SerialUART(comboBox_EmulatorInterface_Ports.SelectedItem.ToString(),
                                    Convert.ToInt32(comboBox_EmulatorInterface_BaudRates.SelectedItem.ToString()),
                                    SerialUART.InterpretFlowControl(comboBox_EmulatorInterface_FlowControl.SelectedItem.ToString()),
                                    SerialUART.InterpretParity(comboBox_EmulatorInterface_Parity.SelectedItem.ToString()),
                                    (int)comboBox_EmulatorInterface_DataBits.SelectedItem,
                                    SerialUART.InterpretStopBits(comboBox_EmulatorInterface_StopBits.SelectedItem.ToString()),
                                    true);
#if !ONLYUI                            

                
                thread_DataReception_UART = new Thread(new ThreadStart(Thread_DataReception_UART));
                thread_DataReception_UART.IsBackground = true;
                thread_DataReception_UART.Priority = ThreadPriority.Highest;

                thread_ChartPrinting = new Thread(new ThreadStart(Thread_ChartPrinting));
                thread_ChartPrinting.IsBackground = true;
                thread_ChartPrinting.Priority = ThreadPriority.Normal;

                thread_TextPrinting = new Thread(new ThreadStart(Thread_TextPrinting));
                thread_TextPrinting.IsBackground = true;
                thread_TextPrinting.Priority = ThreadPriority.Lowest;

                thread_DataReception_UART.Start();
                thread_ChartPrinting.Start();
                thread_TextPrinting.Start();
#endif                       
                        
                InvertFlagState(ref flag_Thread_DataReception_UART_Enabled, ref flag_Thread_Printing_Enabled);

                InvertButtonsHabilitation(button_EmulatorInterface_Connect,
                                        button_EmulatorInterface_Disconnect, 
                                        button_EmulatorInterface_SendCommand,
                                        button_Simulation_Start);

                InvertComboBoxHabilitation(comboBox_EmulatorInterface_BaudRates, 
                                            comboBox_EmulatorInterface_FlowControl,
                                            comboBox_EmulatorInterface_Parity, 
                                            comboBox_EmulatorInterface_Ports,
                                            comboBox_EmulatorInterface_StopBits,
                                            comboBox_EmulatorInterface_DataBits);

                ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Interface conectada...");
                Debug.WriteLine("Conexion UART iniciada.");
            }
            catch(Exception ex)
            {
                string windowTitle = "Error de conexíon UART";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
#if !ONLYUI
                if(uart != null) uart.Dispose();    
#endif
            }
        }

        /// <summary>
        /// Finaliza la conexión UART y reinicializa todas las variables pertinentes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_EmulatorInterface_Disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox_EmulatorInterface_ConnectionState.BackColor = Color.Red;

                InvertFlagState(ref flag_Thread_DataReception_UART_Enabled, ref flag_Thread_Printing_Enabled);

                uart.Close();
                uart.Dispose();

                InvertButtonsHabilitation(button_EmulatorInterface_Connect,
                                        button_EmulatorInterface_Disconnect,
                                        button_EmulatorInterface_SendCommand,
                                        button_Simulation_Start);

                InvertComboBoxHabilitation(comboBox_EmulatorInterface_BaudRates,
                                            comboBox_EmulatorInterface_FlowControl,
                                            comboBox_EmulatorInterface_Parity,
                                            comboBox_EmulatorInterface_Ports,
                                            comboBox_EmulatorInterface_StopBits,
                                            comboBox_EmulatorInterface_DataBits);

                ToolStripStatusLabel_Status_Write(toolStripStatusLabel_ProgramStatus, "Interface desconectada");
                Debug.WriteLine("UART desconectado.");
            }
            catch(Exception ex)
            {
                string windowTitle = "Error de desconexíon UART";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
#if !ONLYUI
            if(uart != null) uart.Dispose();    
#endif
            }            
        }

        //[NOTE]: No es necesario usar los eventos de cambio de indice de los ComboBoxes
        
        /// <summary>
        /// Revisa si se presiono la tecla "Enter" para que se envien los datos por la conexión UART.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_EmulatorInterface_CommandToSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    byte[] command_buffer = ConvertStringToByteArray(textBox_EmulatorInterface_CommandToSend.Text);
                    uart.Write( command_buffer, 0, command_buffer.Length);

                    textBox_EmulatorInterface_CommandToSend.Text = " ";
                    Debug.WriteLine("Se envió el paquete por UART");
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "UART: Error de envio";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Envia los datos del a través de la conexión serial.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_EmulatorInterface_SendCommand_Click(object sender, EventArgs e)
        {
            try
            {               
                byte[] command_buffer = ConvertStringToByteArray(textBox_EmulatorInterface_CommandToSend.Text);
                uart.Write(command_buffer, 0, command_buffer.Length);

                textBox_EmulatorInterface_CommandToSend.Text = " ";
                Debug.WriteLine("Se envió el paquete por UART");
            }
            catch (Exception ex)
            {
                string windowTitle = "UART: Error en el envío del paquete.";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        #endregion Emulator interface controls
    }
}