using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ROACH_0100
{
    public partial class Form1 : Form
    {
        #region Variables
        ConcurrentQueue<byte> textbox_Buffer = new ConcurrentQueue<byte>();
        
        /// <summary>
        /// Señal de la simulación seleccionada para imprimir en la gráfica.
        /// </summary>
        string simulator_SelectedSignal = "";
                
        /// <summary>
        /// Bandera que habilita o deshabilita el hilo de la comunicacion serial.
        /// Si es false cierra el hilo.
        /// </summary>
        bool flag_Thread_DataReception_UART_Enabled = false;
        /// <summary>
        /// Bandera que habilita o deshabilita el hilo de la comunicacion con el DLL en C del simulador.
        /// Si es false cierra el hilo.
        /// </summary>
        bool flag_Thread_DataReception_DLL_Enabled = false;
        /// <summary>
        /// Bandera que habilita o deshabilita el hilo de la impresion de la gráfica y 
        /// el hilo textos de la interfaz. Si es false cierra el hilo.
        /// </summary>
        bool flag_Thread_Printing_Enabled = false;
        /// <summary>
        /// Suspende el hilo de la comunicacion serial.
        /// </summary>
        bool flag_Thread_DataReception_UART_IsPaused = false;
        /// <summary>
        /// Suspende el hilo de la comunicacion con el DLL en C del simulador.
        /// </summary>
        bool flag_Thread_DataReception_DLL_IsPaused = false;
        /// <summary>
        /// Suspende el hilo de la impresion de la gráfica y el hilo textos de la interfaz.
        /// </summary>
        bool flag_Thread_Printing_IsPaused = false;
        #endregion Variables

        #region Threasd
        
        /// <summary>
        /// Hilo encargado de la recepcion de datos a traves del puerto serial.
        /// </summary>
        void Thread_DataReception_UART()
        {            
            try
            {                
                byte[] buffer;
                Queue<byte> buffer_queue = new Queue<byte>();
                float scale_factor = 59.577272f; //HACK: Puedes usar (182.041666f) para pruebas                

                Func<byte> Buffer_Queue_TryDequeue = () =>
                    {
                        if (buffer_queue.Count != 0)
                            return buffer_queue.Dequeue();
                        else
                            return 0;
                    };

                for(; ; )
                {
                    if(flag_Thread_DataReception_UART_Enabled)
                    {
                        if(refreshRate < 10)                        
                            Thread.Sleep(8);
                        else
                            Thread.Sleep(refreshRate);

                        if(!flag_Thread_DataReception_UART_IsPaused)
                        {                            
                            /* Pasos a ejecutar:
                             * 1. Leer los datos.
                             * 2. Armar el paquete(viene en paquetes de 4 bytes 2 de inicializacion y 2 de datos)
                             *      2.1 Guardar los datos en una cola
                             *      2.2 Buscar los bytes delimitadores
                             * 3. Agregar los datos la cola
                             */

                            //1. Leemos los datos datos recibidos
                            if (uart.IsOpen == false) uart.Open();
                            buffer = new byte[8];
                            uart.Read(buffer, 0, 8);
                            

                            //2. Armamos el paquete
                            for (int i = 0; i < buffer.Length; i++)
                            {
                                //2.1 Guardamos los datos en una cola
                                buffer_queue.Enqueue(buffer[i]);
                                textbox_Buffer.Enqueue(buffer[i]);
                            }

                            for (int i = 0; i < buffer_queue.Count; i++)
                            {
                                //2.2 Buscamos los Bytes delimitadores
                                if (buffer_queue.Peek() == 0)
                                {
                                    Buffer_Queue_TryDequeue();
                                    
                                    if (buffer_queue.Peek() == 255)
                                    {
                                        Buffer_Queue_TryDequeue();
                                        ////3.Agregamos los datos a la cola(si hay suficientes datos en la cola)                                                                                
                                        if (buffer_queue.Count > 2)
                                        {                                            
                                            float local_aux = buffer_queue.Dequeue();
                                            float sign = local_aux != 0 ? 1f : -1f;
                                            data.Enqueue(
                                               (sign * (buffer_queue.Dequeue() * 256 + buffer_queue.Dequeue()) / scale_factor)
                                               );
                                        }
                                    }
                                }
                                else
                                {
                                    //Como no es el inicio del Start_Delimiter lo descartamos
                                    Buffer_Queue_TryDequeue();
                                }
                            }
                        }                        
                    }
                    else
                    {
                        break;
                    }                    
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "Error en el hilo de comunicacion en el puerto serial";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hilo encargado de la interracion con el DLL en C de la simulacion del rectificador.
        /// </summary>
        void Thread_DataReception_DLL()
        {
            try
            {
                //float dumbValue = 0.0f;
                Debug.WriteLine("Iniciando hilo de recepción DLL...");
                for (; ; )
                {
                    if(flag_Thread_DataReception_DLL_Enabled)
                    {
                        Thread.Sleep(refreshRate);                        
                        if (!flag_Thread_DataReception_DLL_IsPaused)
                        {
                            /* Pasos a ejecutar:
                             * 0. Llamar a la funcion del DLL "SteppedSimulation"
                             * 1. Obtenemos el dato que queremos dezplegar
                             * 2. Agregamos el dato recibido a nuestra coleccion datos.
                             */

                            dllLinker.CallToUnmanagedMethod();
                            data.Enqueue(dllLinker.GetDesiredValue(simulator_SelectedSignal));
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Hilo de recepción DLL finalizado.");
                        break;
                    }                    
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "Error en el hilo de comunicacion con el SIMDLL";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hilo encargado de la impresion y comportamiento de la gráfica.
        /// </summary>
        void Thread_ChartPrinting()
        {
            try
            {
                Debug.WriteLine("Iniciando hilo de impresion de la gráfica...");
                int chartPointsCount = 0;

                //Inician los contadores
                stopWatch_FilledChart.Start();

                for (; ; )
                {
                    if(flag_Thread_Printing_Enabled)
                    {
                        Thread.Sleep(refreshRate);
                        if(!flag_Thread_Printing_IsPaused)
                        {
                            //Se reinicia el contador de tiempo en caso de que se haya pausado
                            if (stopWatch_General.ElapsedTicks != 0)//Se verifica de esta manera para que sea lo más rapido posible
                            {
                                stopWatch_FilledChart.Start();
                            } 
                            
                            #region UART Printing
                            if(flag_Thread_DataReception_UART_Enabled)
                            {                                
                                ChartPrinting.PrintConcurrent(chart_DataOutput, data, samplingTime);
                            }
                            #endregion

                            #region Simulation Printing
                            if (flag_Thread_DataReception_DLL_Enabled)
                            {
                                //[NOTE] Esta es la forma más rapida comparada a imprimir hasta esperar 
                                //por una cantidad finita de datos
                                ChartPrinting.PrintConcurrent(chart_DataOutput, data, samplingTime);
                            }
                            #endregion

                            #region User Interface                            
                            //Impresion del tiempo de impresion por pantalla
                            chartPointsCount = chart_DataOutput.Series.ElementAt<Series>(0).Points.Count;

                            if (chartPointsCount != 0 && stopWatch_FilledChart.IsRunning == false)
                            {
                                //Se han de perder algunos nanosegundos posiblemente, pero parece no ser significativo
                                stopWatch_FilledChart.Start();
                            }
                            else if (chartPointsCount == 0 && stopWatch_FilledChart.IsRunning == true)
                            {
                                ToolStripStatusLabel_ChartPrintingTime_Write(toolStripStatusLabel_ChartPrintingTime,
                                    stopWatch_FilledChart.ElapsedMilliseconds.ToString());
                                stopWatch_FilledChart.Reset();
                            }
                            #endregion
                        }
                        else//Si esta pausado
                        {
                            stopWatch_FilledChart.Stop();
                        }
                    }
                    else
                    {
                        //Se reinician los contadores
                        stopWatch_FilledChart.Reset();

                        ToolStripStatusLabel_ChartPrintingTime_Write(toolStripStatusLabel_ChartPrintingTime,
                                    stopWatch_FilledChart.ElapsedMilliseconds.ToString());

                        Debug.WriteLine("Hilo de impresion de la gráfica finalizado.");
                        break;
                    }                    
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "Error en el hilo de impresión de la gráfica";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Hilo encargado de la impresion de Textos.
        /// </summary>
        void Thread_TextPrinting()
        {
            try
            {
                Debug.WriteLine("Iniciando hilo de impresion de texto...");
                byte aux = 0;

                //Inician los contadores
                stopWatch_General.Start();

                for (; ; )
                {
                    if(flag_Thread_Printing_Enabled)
                    {
                        Thread.Sleep(50);
                        if(!flag_Thread_Printing_IsPaused)
                        {
                            //Impresion en el Textbox de los datos racibidos en UART 
                            if(flag_Thread_DataReception_UART_Enabled == true 
                                && flag_Thread_DataReception_UART_IsPaused == false)
                            {
                                if (textbox_Buffer.TryDequeue(out aux))
                                {
                                    PrintTextInTextBox(textBox_EmulatorInterface_ReceivedData, 
                                        aux.ToString(), true);
                                }
                            }

                            //Se reinicia el contador de tiempo en caso de que se haya pausado
                            if (stopWatch_General.ElapsedTicks != 0)//Se verifica de esta manera para que sea lo más rapido posible
                            {
                                stopWatch_General.Start();
                            }

                            //Impresion del tiempo transcurrido
                            ToolStripStatusLabel_Time_Write(toolStripStatusLabel_Time,
                                    Convert.ToString(stopWatch_General.ElapsedMilliseconds/1000));
                        }
                        else//Si esta pausado
                        {
                            stopWatch_General.Stop();
                        }
                    }
                    else
                    {
                        //Se reinician los contadores
                        stopWatch_General.Reset();
                        ToolStripStatusLabel_Time_Write(toolStripStatusLabel_Time,
                                    Convert.ToString(stopWatch_General.ElapsedMilliseconds / 1000));

                        Debug.WriteLine("Hilo de impresion de la gráfica finalizado.");
                        break;
                    }                    
                }
            }
            catch (Exception ex)
            {
                string windowTitle = "Error en el hilo de impresión de texto";
                MessageBox.Show(ex.Message, windowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Threads
    }
}