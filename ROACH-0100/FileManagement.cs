using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Ramm
{
    class FileManagement
    {
        /// <summary>
        /// 
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FolderPath { get; set; }
        /// <summary>
        /// 
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

        public void SaveXML<T>(T variable)
        {
            if(this.FilePath != null || this.FilePath != "")
            {                
                SaveXML<T>(variable, this.FilePath);
            }
            hasBeenSavedBefore = true;
        }

        public void SaveXML<T>(T variable, string pathFile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(pathFile))
            {
                serializer.Serialize(writer, variable);
            }
            hasBeenSavedBefore = true;
        }

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
