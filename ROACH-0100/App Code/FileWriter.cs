using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ROACH_0100
{
    /// <summary>
    /// Representa un objeto para escribir o guardar archivos en disco.
    /// </summary>
    class FileWriter : IDisposable
    {
        #region Fields
        /// <summary>
        /// Objeto encargado de escribir un archivo.
        /// </summary>
        private StreamWriter dataFile;
        /// <summary>
        /// Bandera que guarda si el objeto se despachara.
        /// </summary>
        private bool disposed = false;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Direccion del archivo.
        /// </summary>
        public string DataPath { get; private set; }
        #endregion Properties

        #region Constructors & Destructors
        /// <summary>
        /// Inicializa una nueva instancia de la clase System.IO.StreamWriter
        /// usando la direccion del archivo provista.
        /// </summary>
        /// <param name="dataPath">Direccion donde se guardara el archivo(Ejemplo: C:\\Desktop\\test.txt)</param>
        /// <param name="appendMode">Define el modo en que se escribira el archivo</param>
        public FileWriter(string dataPath, bool appendMode = false)
        {
            this.DataPath= dataPath;
            if(!appendMode)
                dataFile = new StreamWriter(dataPath);
            else
                dataFile = new StreamWriter(dataPath, true);        
        }

        /// <summary>
        /// Destructor por defecto.
        /// </summary>
        ~FileWriter()
        {
            Dispose(false);
        }
        #endregion Constructors & Destructors

        #region Methods
        /// <summary>
        /// Limpia todos los buffers y escribe cualquier dato pendiente en estos.
        /// </summary>
        public void Flush()
        {
            dataFile.Flush();
        }

        /// <summary>
        /// Escribe el valor especificado dentro del archivo.
        /// </summary>
        /// <param name="value">Acepta cualquier tipo de valor.</param>
        public void Write(ValueType value)
        {
            dataFile.Write(value);
        }

        /// <summary>
        /// Escribe el valor especificado dentro del archivo seguido del comando terminador de linea.
        /// </summary>
        /// <param name="value">Acepta cualquier tipo de valor.</param>
        public void WriteLine(ValueType value)
        {
            dataFile.WriteLine(value);            
        }

        /// <summary>
        /// Escribe el valor especificado dentro del archivo seguido del comando terminador de linea.
        /// Funciona unicamente con variables y no con constantes.
        /// </summary>
        /// <param name="text">Cadena a escribir.</param>
        public void WriteLine(string text)
        {
            dataFile.WriteLine(text);
        }

        /// <summary>
        /// Escribe una serie de cadenas y las imprime como una fila en formato .CSV.
        /// </summary>
        /// <param name="text">Cadenas(Palabras) a escribir en el archivo.</param>
        public void PrintCSV(params string[] text)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;

            do
            {
                sb.Append(text[index]);
                if (index + 1 != text.Length) sb.Append(",");
                index++;
            }
            while (index < text.Length);

            WriteLine(sb.ToString());
        }

        /// <summary>
        /// Devuelve una cadena que representa al objeto.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion Methods

        #region IDisposable Members
        /// <summary>
        /// Despacha todos los recursos utilizados por el objeto FileWriter.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Despacha todos los recursos utilizados por el objeto FileWriter.
        /// </summary>
        /// <param name="disposing">Es "true" si fue llamado a traves de "FileWriter.Dispose()".</param>
        protected void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    dataFile.Dispose();
                }                
                disposed = true;
            }
        }
        #endregion IDisposable Members
    }
}
