using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ramm;

namespace ROACH_0100
{
    /// <summary>
    /// Clase que 
    /// </summary>
    [Serializable]
    public class UIConfiguration
    {
        public struct UI_Params
        {
            public int RefreshRate;
            public int Range_Y_Minimum;
            public int Range_Y_Maximum;
            public int SamplingTime;   
        }

        public struct UART_Params
        {
            public string PortName;
            public int BaudRate;
            public string FlowControl;
            public string Parity;
            public int DataBits;
            public string StopBits;
        }

        //public struct Simulator_Params
        //{
        //    public string DllFilePath;
        //    public int ParamsQuantity;
        //    public List<string> ParamsName;
        //    public float[] OriginalParamsValues;
        //}

        public LinkerToUnamangedLibrary dllLinker;
        public UI_Params UIParams;
        public UART_Params UartParams;
        //public Simulator_Params SimulatorParams { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia para el guardado de datos de la configuración.
        /// </summary>
        public UIConfiguration()
        {
            UIParams = new UI_Params();
            UartParams = new UART_Params();            
        }
    }
}
