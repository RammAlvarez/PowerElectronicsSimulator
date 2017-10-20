using System.Collections.Generic;
using System.IO.Ports;

namespace ROACH_0100
{
    class SerialUART : SerialPort
    {
        /// <summary>
        /// Contiene todos los datos recibidos en el puerto serial.
        /// </summary>
        public byte[] Data { get; private set; }

        #region Constructors
        /// <summary>
        /// Inicializa una nueva instancia de la clase System.IO.Ports.SerialPort con funciones adicionales
        /// para facilitar el uso del UART.
        /// </summary>
        public SerialUART()
        {
            
        }

        /// <summary>
        /// Inicializa una neva instancia de la clase System.IO.Ports.SerialPort 
        /// usando el nombre del puerto y baud rate provisto.
        /// </summary>
        /// <param name="portName">Puerto a usar(Ejemplo: "COM1").</param>
        /// <param name="baudRate">Tasa de baudios a utilizar.</param>
        /// <param name="autoOpen">Define si se abrira el puerto durante de la inicializacion.</param>
        public SerialUART(string portName, int baudRate, bool autoOpen = false)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            
            if (autoOpen) this.Open();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase System.IO.Ports.SerialPort 
        /// usando el nombre del puerto, baud rate, control de flujo y paridad
        /// provisto.
        /// </summary>
        /// <param name="portName">Puerto a usar(Ejemplo: "COM1").</param>
        /// <param name="baudRate">Tasa de baudios a utilizar.</param>
        /// <param name="flowControl">Tipo de control por flujo.</param>
        /// <param name="parity">Tipo de paridad.</param>
        /// <param name="autoOpen">Define si se abrira el puerto durante de la inicializacion.</param>
        public SerialUART(string portName, int baudRate, Handshake flowControl, Parity parity,
            bool autoOpen = false)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            this.Handshake = flowControl;
            this.Parity = parity;
            
            if (autoOpen) this.Open();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase System.IO.Ports.SerialPort
        /// usando el nombre del puerto, baud rate, control de flujo, paridad, bits de datos y 
        /// bits de parada provisto.
        /// </summary>
        /// <param name="portName">Puerto a usar(Ejemplo: "COM1").</param>
        /// <param name="baudRate">Tasa de baudios a utilizar.</param>
        /// <param name="flowControl">Tipo de control por flujo.</param>
        /// <param name="parity">Tipo de paridad.</param>
        /// <param name="dataBits">Tamaño del paquete.</param>
        /// <param name="stopBits">Bits de parada en el paquete.</param>
        /// <param name="autoOpen">Define si se abrira el puerto durante de la inicializacion.</param>
        public SerialUART(string portName, int baudRate, Handshake flowControl, Parity parity, int dataBits,
            StopBits stopBits, bool autoOpen = false)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            this.Handshake = flowControl;
            this.Parity = parity;
            this.DataBits = dataBits;
            this.StopBits = stopBits;
            
            if (autoOpen) this.Open();
        }
        #endregion Constructors

        #region Methods

        #region Acquirers
        /// <summary>
        /// Devuelve los puertos seriales disponibles e el sistema.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPorts()
        {
            List<string> aux_list = new List<string>();
            string[] aux_string = SerialPort.GetPortNames();

            foreach (string item in aux_string)
            {
                aux_list.Add(item);
            }
            aux_list.Sort();

            return aux_list;
        }

        /// <summary>
        /// Obtiene los baud rates disponbles.
        /// </summary>
        /// <returns></returns>
        public static List<int> GetBaudRates()
        {
            List<int> baudRate = new List<int>();

            baudRate.Add(110);
            baudRate.Add(300);
            baudRate.Add(1200);
            baudRate.Add(2400);
            baudRate.Add(4800);
            baudRate.Add(9600);
            baudRate.Add(19200);
            baudRate.Add(38400);
            baudRate.Add(57600);
            baudRate.Add(115200);
            baudRate.Add(230400);
            baudRate.Add(460800);
            baudRate.Add(921600);

            return baudRate;
        }

        /// <summary>
        /// Devuelve los tipos de paridad existentes en el puerto serial para ser desplegados
        /// en forma de texto.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetParity()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add(Parity.None.ToString());
            aux_list.Add(Parity.Odd.ToString());
            aux_list.Add(Parity.Even.ToString());
            aux_list.Add(Parity.Mark.ToString());
            aux_list.Add(Parity.Space.ToString());

            return aux_list;
        }

        /// <summary>
        /// Devuelve los tipos de bits de parada existentes en el puerto serial para ser desplegados
        /// en forma de texto.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetStopBits()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add(StopBits.None.ToString());
            aux_list.Add(StopBits.One.ToString());
            aux_list.Add(StopBits.OnePointFive.ToString());
            aux_list.Add(StopBits.Two.ToString());

            return aux_list;
        }

        /// <summary>
        /// Devuelve los tipos de control de flujo(Handshake) existentes en el puerto serial para ser 
        /// desplegados en forma de texto.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetFlowControl()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add(Handshake.None.ToString());
            aux_list.Add(Handshake.RequestToSend.ToString());
            aux_list.Add(Handshake.RequestToSendXOnXOff.ToString());
            aux_list.Add(Handshake.XOnXOff.ToString());

            return aux_list;
        }

        /// <summary>
        /// Devuelve las cantidades de bits de datos existentes en el puerto serial.
        /// </summary>
        /// <returns></returns>
        public static List<int> GetDataBits()
        {
            List<int> aux_list = new List<int>();

            aux_list.Add(5);
            aux_list.Add(6);
            aux_list.Add(7);
            aux_list.Add(8);

            return aux_list;
        }
        #endregion Acquirers

        #region Interpreters
        /// <summary>
        /// Devuelve la interpretacion del texto en una forma utilizable para 
        /// el establecimiento del control de flujo.
        /// </summary>
        /// <param name="flowControl">Control de flujo(Opciones: None, RequestToSend, RequestToSendXOnXOff, XOnXOff).</param>
        /// <returns></returns>
        public static Handshake InterpretFlowControl(string flowControl)
        {
            if (flowControl == Handshake.None.ToString())
            {
                return Handshake.None;
            }
            else if (flowControl == Handshake.RequestToSend.ToString())
            {
                return Handshake.RequestToSend;
            }
            else if (flowControl == Handshake.RequestToSendXOnXOff.ToString())
            {
                return Handshake.RequestToSendXOnXOff;
            }
            else if (flowControl == Handshake.XOnXOff.ToString())
            {
                return Handshake.XOnXOff;
            }
            else
            {
                return Handshake.None;
            }
        }

        /// <summary>
        /// Devuelve la interpretacion del texto en una forma utilizable para 
        /// el establecimiento de la paridad.
        /// </summary>
        /// <param name="parity">Tipo de paridad(Opciones: None, Odd, Even, Mark, Space).</param>
        /// <returns></returns>
        public static Parity InterpretParity(string parity)
        {
            if (parity == Parity.None.ToString())
            {
                return Parity.None;
            }
            else if (parity == Parity.Odd.ToString())
            {
                return Parity.Odd;
            }
            else if (parity == Parity.Even.ToString())
            {
                return Parity.Even;
            }
            else if (parity == Parity.Mark.ToString())
            {
                return Parity.Mark;
            }
            else if (parity == Parity.Space.ToString())
            {
                return Parity.Space;
            }
            else
            {
                return Parity.None;
            }
        }

        /// <summary>
        /// Devuelve la interpretacion del texto en una forma utilizable para 
        /// el establecimiento de los bits de parada.
        /// </summary>
        /// <param name="stopbits">Tipos de bits de parada(Opciones: None, One, OnePointFive, Two)</param>
        /// <returns></returns>
        public static StopBits InterpretStopBits(string stopbits)
        {
            if (stopbits == StopBits.None.ToString())
            {
                return StopBits.None;
            }
            else if (stopbits == StopBits.One.ToString())
            {
                return StopBits.One;
            }
            else if (stopbits == StopBits.OnePointFive.ToString())
            {
                return StopBits.OnePointFive;
            }
            else if (stopbits == StopBits.Two.ToString())
            {
                return StopBits.Two;
            }
            else
            {
                return StopBits.None;
            }
        }
        #endregion Interpreters

        /// <summary>
        /// Evento personalizado que lee los datos recibidos en el buffer de entrada
        /// y lo guarda en la propiedad Uart.dataReceived.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {            
            SerialPort sp = (SerialPort)sender;
            Data = new byte[8];
            sp.Read(buffer: Data, offset: 0, count: 1);
        }

        /// <summary>
        /// Devuelve una cadena que representa al objeto.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "PortName: " + this.PortName + ", BaudRate: " + this.BaudRate + ", FlowControl: "
                + this.Handshake.ToString() + ", Parity: " + this.Parity.ToString();
        }
        #endregion Methods
    }

}
