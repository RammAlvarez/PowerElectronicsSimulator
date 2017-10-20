using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Ramm
{
    class FileManagement
    {
        /// <summary>
        /// Direcccion del archivo.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Direcccion de la carpeta.
        /// </summary>
        public string FolderPath { get; set; }
        /// <summary>
        /// Establece si el archivo ha sido guardado anetriormente.
        /// </summary>
        private bool hasBeenSavedBefore = false;

        /// <summary>
        /// Inicializa una instancia del objeto.
        /// </summary>
        public FileManagement()
        {

        }

        /// <summary>
        /// Inicializa una instancia del objeto con la dirección del archivo a guardar.
        /// </summary>
        /// <param name="filePath"></param>
        public FileManagement(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// Serializa en XML el objeto pasado y guarda el archivo con el nombre deseado.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Save<T>(T variable, string projectName)
        {
            if(hasBeenSavedBefore == false)
            {                
                SaveXMLAs<T>(variable, projectName);
            }
            else
            {
                SaveXML<T>(variable);
            }
        }

        /// <summary>
        /// Guarda un archivo XML en una carpeta a escoger.
        /// </summary>
        /// <typeparam name="T">Serializable</typeparam>
        /// <param name="variable">Datos a guardar.</param>
        /// <param name="projectName">Nombre del Projecto</param>
        public void SaveXMLAs<T>(T variable, string projectName)
        {
            //Se busca el archivo
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            this.FolderPath = dialog.SelectedPath;
            this.FilePath = this.FolderPath + "//" + projectName;
            //Se guarda la variable deseada en un archivo XML
            SaveXML<T>(variable);
        }

        /// <summary>
        /// Invoca a una funcion de guardado siempre y cuando la información de guardado sea valida.
        /// </summary>
        /// <typeparam name="T">Serializable</typeparam>
        /// <param name="variable">Datos a guardar.</param>
        public void SaveXML<T>(T variable)
        {
            if(this.FilePath != null || this.FilePath != "")
            {                
                SaveXML<T>(variable, this.FilePath);
            }
            hasBeenSavedBefore = true;
        }

        /// <summary>
        /// Crea un archivo XML y escribe los datos proporcionados en una dirección de archivo proporcionada.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable">Datos a guardar.</param>
        /// <param name="pathFile">Dirección del archivo.</param>
        public void SaveXML<T>(T variable, string pathFile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(pathFile))
            {
                serializer.Serialize(writer, variable);
            }
            hasBeenSavedBefore = true;
        }

        /// <summary>
        /// Abre un archivo XML y lo carga en memoria.
        /// </summary>
        /// <typeparam name="T">Serializable</typeparam>
        /// <param name="pathfile">Dirección del archivo.</param>
        /// <returns></returns>
        public T OpenXMLProject<T>(string pathfile)
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using(StreamReader reader = new StreamReader(pathfile))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }

    }
}
