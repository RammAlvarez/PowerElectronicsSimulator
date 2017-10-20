using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Ramm
{
    /// <summary>
    /// Clase que permite la lectura de archivos.
    /// </summary>
    class FileReader : IDisposable
    {
        #region Fields
        private bool disposed = false;
        private StreamReader dataFile;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Obtiene o establece la dirección del archivo.
        /// </summary>
        public string filePath { get; set; }

        /// <summary>
        /// Obtiene la bandera que indica si se encuentra al final del archivo.
        /// </summary>
        public bool endOfFile { get { return dataFile.EndOfStream; } }
        #endregion Properties

        /// <summary>
        /// Inicializa una instancia del objeto a partir del archivo a leer.
        /// </summary>
        /// <param name="filePath">Dirreccion del archivo.</param>
        public FileReader(string filePath)
        {
            this.filePath = filePath;
            dataFile = new StreamReader(this.filePath);
        }

        #region Methods
        /// <summary>
        /// Devuelve una linea del archivo y mueve el cursor al inicio de la siguiente.
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            return dataFile.ReadLine();
        }

        /// <summary>
        /// Devuelve una cadena de texto con todo lo contenido dentro del archivo
        /// </summary>
        /// <returns></returns>
        public string ReadToEnd()
        {
            return dataFile.ReadToEnd();
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
        /// <param name="disposing">Es "true" si fue llamado a traves de "FileReader.Dispose()"</param>
        protected void Dispose(bool disposing)
        {
            if(this.disposed)
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
