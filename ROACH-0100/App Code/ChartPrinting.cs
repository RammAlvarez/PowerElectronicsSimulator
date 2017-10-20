using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Collections.Concurrent;

namespace ROACH_0100
{
    /// <summary>
    /// Maneja el comportamiento de la impresion y datos de una gráfica.
    /// </summary>
    static class ChartPrinting
    {
        #region Delegates
        public delegate bool PrintDelegate(Chart chart, List<float> data, int max_range, ref int index);
        public delegate void PrintConcurrentDelegate(Chart chart, ConcurrentQueue<float> data, int max_range);//, ref int chart_index);
        #endregion Delegates

        #region Printing Methods
        /// <summary>
        /// Imprime los datos en el Chart especificado. Diseñada para utilizarse dentro de un hilo de 
        /// computo diferente al base.
        /// </summary>
        /// <param name="chart">Objeto de la grafica.</param>
        /// <param name="data">Lista que contiene los datos a imprimirse.</param>
        /// <param name="max_range">Rango maximo en el eje X de la grafica.</param>
        /// <param name="chart_index">Indice actual de la lista a partir del que se imprimira.</param>
        /// <returns>Devuelve "true" si se reinicio la grafica y "false" si solo continuo imprimiendo.</returns>
        public static bool Print(Chart chart, List<float> data, int max_range, ref int chart_index)
        {
            //HACK: [ChartPrinting.Print] Puede que exista una mejor manera de imprimir en pantalla.
            try
            {
                if (chart.InvokeRequired)
                {
                    object[] args = new object[] { chart, data, max_range, chart_index };
                    PrintDelegate pd = new PrintDelegate(Print);
                    chart.Invoke(pd, args);
                }
                else
                {
                    //if (data.Count > max_range)
                    if (chart.Series.ElementAt<Series>(0).Points.Count > max_range)
                    {
                        data.RemoveAll(item => item == 0.0f || item != 0.0f);
                        chart.Series.ElementAt<Series>(0).Points.Clear();
                        return true;
                    }
                    else
                    {
                        for (int i = chart_index; i < data.Count; i++)
                            chart.Series.ElementAt<Series>(0).Points.Add(data[i]);//HACK: [ChartPrinting.Print] Posiblemente se este imprimiendo multiples veces y no sea adecuado

                        chart_index = chart.Series.ElementAt<Series>(0).Points.Count;
                        return false;
                    }
                }
                return false;
            }
            catch 
            {
                throw new MethodAccessException("Error intentando en la impresión de la gráfica.");
            }            
        }

        /// <summary>
        /// Imprime los datos de una fila concurrente en el Chart especificado. Diseñada para utilizarse dentro de un hilo de 
        /// computo diferente al base.
        /// </summary>
        /// <param name="chart">Objeto de la grafica.</param>
        /// <param name="data">Lista que contiene los datos a imprimirse.</param>
        /// <param name="max_range">Rango maximo en el eje X de la grafica.</param>
        /// <param name="chart_index">Indice actual de la lista a partir del que se imprimira.</param>
        /// <returns>Devuelve "true" si se reinicio la grafica y "false" si solo continuo imprimiendo.</returns>
        public static void PrintConcurrent(Chart chart, ConcurrentQueue<float> data, int max_range)//, ref int chart_index)
        {
            //HACK: [ChartPrinting.PrintConcurrent] Puede que exita una mejor manera de imprimir en pantalla.
            try
            {
                if(chart.InvokeRequired)
                {
                    object[] args = new object[] { chart, data, max_range};
                    PrintConcurrentDelegate pcd =  new PrintConcurrentDelegate(PrintConcurrent);
                    chart.Invoke(pcd, args);
                }
                else
                {
                    //if(data.Count > max_range)
                    if (chart.Series.ElementAt<Series>(0).Points.Count > max_range)
                    {
                        data = new ConcurrentQueue<float>();//HACK: [ChartPrinting.PrintConcurrent] Posiblemente no sea adecuado reiniciarla.
                        chart.Series.ElementAt<Series>(0).Points.Clear();
                    }
                    else
                    {
                        float aux;
                        //if(data.TryDequeue(out aux))
                        //{
                        //    chart.Series.ElementAt<Series>(0).Points.Add(aux);
                        //    //chart_index = chart.Series.ElementAt<Series>(0).Points.Count;
                        //}

                        for (int i = 0; i < data.Count; i++)
                        {
                            if(data.TryDequeue(out aux))
                                chart.Series.ElementAt<Series>(0).Points.Add(aux);
                        }
                    }
                }
            }
            catch
            {
                throw new MethodAccessException("Error intentando en la impresión concurrente de la gráfica.");
            }
        }
               
        #endregion Printing Methods

        #region Erase Methods
        /// <summary>
        /// Borra todos los elementos de la gráfica.
        /// </summary>
        /// <param name="chart">Gráfica a borrar.</param>
        public static void Erase(Chart chart)
        {
            try
            {                
                chart.Series.ElementAt<Series>(0).Points.Clear();
            }
            catch
            {
                throw new MethodAccessException("Error intentando borrar la gráfica.");
            }
        }

        /// <summary>
        /// Borra todos los elementos de la gráfica y detiene el hilo que imprime en él.
        /// </summary>
        /// <param name="chart">Grafica a borrar.</param>
        /// <param name="flag_IsPaused">Bandera del hilo que se debe pausar para ejecutar el borrado de la grafica.</param>
        /// <returns></returns>
        public static void Erase(Chart chart, ref bool flag_IsPaused)
        {
            try
            {
                flag_IsPaused = true;
                chart.Series.ElementAt<Series>(0).Points.Clear();
                flag_IsPaused = false;
            }
            catch
            {
                throw new MethodAccessException("Error intentando borrar la gráfica.");
            }
        }

        /// <summary>
        /// Borra todos los elementos de la grafica asi como de la lista de datos.
        /// </summary>
        /// <param name="chart">Grafica a borrar.</param>
        /// <param name="data">Lista a borrar.</param>
        /// <param name="flag">Bandera del hilo que se debe pausar para ejecutar el borrado de la grafica.</param>
        /// <returns></returns>
        public static void Erase(Chart chart, List<float> data)
        {
            try
            {
                data.RemoveAll(item => item == 0 || item != 0);
                chart.Series.ElementAt<Series>(0).Points.Clear();
            }
            catch
            {                
                throw new MethodAccessException("Error intentando borrar la gráfica.");
            }            
        }

        /// <summary>
        /// Borra todos los elementos de la grafica asi como de la fila concurrente que contiene los datos.
        /// </summary>
        /// <param name="chart">Grafica a borrar.</param>
        /// <param name="data">Fila concurrente a reiniciar.</param>
        /// <param name="flag">Bandera del hilo que se debe pausar para ejecutar el borrado de la grafica.</param>
        /// <returns></returns>
        public static void Erase(Chart chart, ref ConcurrentQueue<float> data)
        {
            try
            {
                data = new ConcurrentQueue<float>();
                chart.Series.ElementAt<Series>(0).Points.Clear();
            }
            catch
            {
                throw new MethodAccessException("Error intentando borrar la gráfica.");
            }
        }
        #endregion Erase Methods
    }
}
