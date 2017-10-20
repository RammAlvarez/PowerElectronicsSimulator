using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO.Ports;

namespace ROACH_0100
{
    /// <summary>
    /// Representa un objeto de comunicacion serial.
    /// </summary>
    class Uart : IDisposable
    {
        #region Variables
        /// <summary>
        /// Objeto que permite la comunicacion UART.
        /// </summary>
        private SerialPort serialport;
        /// <summary>
        /// Bandera que guarda si el objeto se despachara.
        /// </summary>
        private bool disposed = false;
        #endregion Variables

        #region Properties
        /// <summary>
        /// Obtiene el estado de la conexion serial.
        /// </summary>
        public bool IsOpen { get { return serialport.IsOpen; } }
        /// <summary>
        /// Obtiene o establece el puerto a utilizar.
        /// </summary>
        public string PortName { get { return PortName; } set { serialport.PortName = PortName = value; } }
        /// <summary>
        /// Obtiene o establece los baudrates disponibles
        /// </summary>
        public int BaudRate { get { return BaudRate; } set { serialport.BaudRate = BaudRate = value; } }
        /// <summary>
        /// Obtiene o establece el tipo de flujo de control de la conexion.
        /// </summary>
        public Handshake FlowControl { get { return FlowControl; } set { serialport.Handshake = FlowControl = value; } }
        /// <summary>
        /// Obtiene o establece el tipo de paridad de la conexion.
        /// </summary>
        public Parity ParityType { get { return ParityType; } set { serialport.Parity = ParityType = value; } }
        /// <summary>
        /// Obtiene o establece los bits de datos utilizados en la transmisión.
        /// </summary>
        public int DataBits { get { return DataBits; } set { serialport.DataBits = DataBits = value; } }
        /// <summary>
        /// Obtiene o establece la cantidad de bits de parada en la transmisión.
        /// </summary>
        public StopBits StopsBitsType { get { return StopsBitsType; } set { serialport.StopBits = StopsBitsType = value; } }
        /// <summary>
        /// Obtiene el dato recibido en el puerto serial.
        /// </summary>
        public string DataReceived { get; private set; }
        #endregion Properties

        #region Constructors & Destructors
        /// <summary>
        /// Inicializa una nueva instancia de la clase System.IO.Ports.SerialPort.
        /// </summary>
        public Uart()
        {
            serialport = new SerialPort();
            serialport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        /// <summary>
        /// Inicializa una neva instancia de la clase System.IO.Ports.SerialPort 
        /// usando el nombre del puerto y baud rate provisto.
        /// </summary>
        /// <param name="portName">Puerto a usar(Ejemplo: "COM1").</param>
        /// <param name="baudRate">Baud rate a utilizar.</param>
        /// <param name="autoOpen">Define si se abrira el puerto durante de la inicializacion.</param>
        public Uart(string portName, int baudRate, bool autoOpen = false)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            serialport = new SerialPort(portName, baudRate);
            serialport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            if (autoOpen) serialport.Open();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase System.IO.Ports.SerialPort 
        /// usando el nombre del puerto, baud rate, control de flujo y paridad
        /// provisto.
        /// </summary>
        /// <param name="portName">Puerto a usar(Ejemplo: "COM1").</param>
        /// <param name="baudRate">Baud rate a utilizar.</param>
        /// <param name="flowControl">Tipo de control por flujo.</param>
        /// <param name="parity">Tipo de paridad.</param>
        /// <param name="autoOpen">Define si se abrira el puerto durante de la inicializacion.</param>
        public Uart(string portName, int baudRate, Handshake flowControl, Parity parity, bool autoOpen = false)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            serialport.Handshake = this.FlowControl = flowControl;
            this.ParityType = parity;
            serialport = new SerialPort(portName, baudRate, parity);
            serialport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            if (autoOpen) serialport.Open();            
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase System.IO.Ports.SerialPort
        /// usando el nombre del puerto, baud rate, control de flujo, paridad, bits de datos y 
        /// bits de parada provisto.
        /// </summary>
        /// <param name="portName">Puerto a usar(Ejemplo: "COM1").</param>
        /// <param name="baudRate">Baud rate a utilizar.</param>
        /// <param name="flowControl">Tipo de control por flujo.</param>
        /// <param name="parity">Tipo de paridad.</param>
        /// <param name="dataBits">Tamaño del paquete.</param>
        /// <param name="stopBits">Bits de parada en el paquete.</param>
        /// <param name="autoOpen">Define si se abrira el puerto durante de la inicializacion.</param>
        public Uart(string portName, int baudRate, Handshake flowControl, Parity parity, int dataBits, 
            StopBits stopBits, bool autoOpen = false)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            serialport.Handshake = this.FlowControl = flowControl;
            this.ParityType = parity;
            this.DataBits = dataBits;
            this.StopsBitsType = stopBits;
            serialport = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            serialport.StopBits = stopBits;
            serialport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            
            if (autoOpen) serialport.Open();            
        }

        /// <summary>
        /// Destructor por defecto.
        /// </summary>
        ~Uart()
        {
            Dispose(false);
        }
        #endregion Constructors & Destructors

        #region Methods
        /// <summary>
        /// Abre la comunicacion serial.
        /// </summary>
        public void Open()
        {
            if (!serialport.IsOpen) serialport.Open();           
        }
        
        /// <summary>
        /// Cierra la comunicacion serial.
        /// </summary>
        public void Close()
        {
            if (serialport.IsOpen) serialport.Close();
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
            else if(parity==Parity.Mark.ToString())
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
        /// Devuelve la interpretacion del texto en una forma utilizable para 
        /// el establecimiento de los bits de parada.
        /// </summary>
        /// <param name="stopbits">Tipos de bits de parada(Opciones: None, One, OnePointFive, Two)</param>
        /// <returns></returns>
        public static StopBits InterpretStopBits(string stopbits)
        {
            if(stopbits == StopBits.None.ToString())
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
        /// Escribe una linea en el buffer de salida.
        /// </summary>
        /// <param name="command"></param>
        public void PutString(string command)
        {
            serialport.WriteLine(command + "\r\n");
        }

        /// <summary>
        /// Escribe una serie de bytes en el buffer de salida.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="offset"></param>
        public void PutByte(byte[] command, int offset)
        {
            serialport.Write(command, offset, command.Length);
            serialport.WriteLine("\r\n");
        }

        /// <summary>
        /// Evento personalizado que lee los datos recibidos en el buffer de entrada
        /// y lo guarda en la propiedad Uart.dataReceived.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            DataReceived = sp.ReadExisting();//TOTEST: [UART] Estamos recibiendo "string" posiblemente debamos recibir Bytes
        }
       
        /// <summary>
        /// Devuelve una cadena que representa al objeto.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {            
            return "PortName: " + serialport.PortName + ", BaudRate: " + serialport.BaudRate + ", FlowControl: " 
                + serialport.Handshake.ToString() + ", Parity: " + serialport.Parity.ToString();
        }
        #endregion Methods

        #region IDisposable Members
        /// <summary>
        /// Despacha todos los recursos utilizados por el objeto Uart.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Despacha todos los recursos utilizados por el objeto Uart.
        /// </summary>
        /// <param name="disposing">Es "true" si fue llamado a traves de "Uart.Dispose()".</param>
        protected void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    serialport.Dispose();
                }
                disposed = true;
            }
        }
        #endregion
    }
}
