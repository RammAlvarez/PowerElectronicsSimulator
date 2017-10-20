using System;

namespace ROACH_0100
{
    /// <summary>
    /// Clase que contiene la configuración de la interface(GUI) y la conexión UART.
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

        public LinkerToUnamangedLibrary dllLinker;
        public UI_Params UIParams;
        public UART_Params UartParams;

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
