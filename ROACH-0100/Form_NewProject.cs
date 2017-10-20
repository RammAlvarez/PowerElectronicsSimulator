using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ramm;

namespace ROACH_0100
{
    public partial class Form_NewProject : Form
    {
        public string ProjectName { get; private set; }
        public string DllFilePath { get; private set; }
        public string MethodName { get; private set; }
        public Dictionary<string, float> OriginalValues { get; private set; }
        private Form1 ParentForm;

        public Form_NewProject()
        {
            InitializeComponent();
            OriginalValues = new Dictionary<string, float>();

            textBox_DllFilePath.KeyPress += new KeyPressEventHandler(KeyPress);
            textBox_DllMethodName.KeyPress += new KeyPressEventHandler(KeyPress);
            textBox_DllVariablesNameValueFile.KeyPress += new KeyPressEventHandler(KeyPress);
            button_FindDll.KeyPress += new KeyPressEventHandler(KeyPress);
            button_FindVariablesFile.KeyPress += new KeyPressEventHandler(KeyPress);
            button_NewProject_Ok.KeyPress += new KeyPressEventHandler(KeyPress);
            button_NewProject_Cancel.KeyPress += new KeyPressEventHandler(KeyPress);
        }

        public Form_NewProject(Form1 parent)
        {
            InitializeComponent();
            OriginalValues = new Dictionary<string, float>();
            ParentForm = parent;

            textBox_DllFilePath.KeyPress += new KeyPressEventHandler(KeyPress);
            textBox_DllMethodName.KeyPress += new KeyPressEventHandler(KeyPress);
            textBox_DllVariablesNameValueFile.KeyPress += new KeyPressEventHandler(KeyPress);
            button_FindDll.KeyPress += new KeyPressEventHandler(KeyPress);
            button_FindVariablesFile.KeyPress += new KeyPressEventHandler(KeyPress);
            button_NewProject_Ok.KeyPress += new KeyPressEventHandler(KeyPress);
            button_NewProject_Cancel.KeyPress += new KeyPressEventHandler(KeyPress);  
            
        }

        

        private void button_NewProject_Ok_Click(object sender, EventArgs e)
        {
            if (textBox_DllFilePath.Text != "" && textBox_DllMethodName.Text != "" 
                && textBox_DllVariablesNameValueFile.Text != "")
            {
                this.ProjectName = textBox_NewProject.Text;
                this.DllFilePath = textBox_DllFilePath.Text;
                this.MethodName = textBox_DllMethodName.Text;

                //Se lee el archivo .CSV donde se encuentran las variables de inicio
                using(FileReader reader = new FileReader(textBox_DllVariablesNameValueFile.Text))
                {
                    string aux = "", variableName = "";                    
                    StringBuilder sb = new StringBuilder();

                    do
                    {
                        aux = reader.ReadLine();
                        //Hacer la logica de la lectura del CSV para obtener las variables
                        foreach (char item in aux)
                        {
                            if (item == ',')
                            {
                                variableName = sb.ToString();
                                sb.Clear();
                            }
                            else
                                sb.Append(item);
                        }

                        OriginalValues.Add( variableName.Trim(), value: float.Parse(sb.ToString()));
                        sb.Clear();

                    } while (reader.endOfFile == false);
                }
                //Cambia el titulo de la la ventana original y cierra la actual
                ParentForm.Text = DllFilePath + " - ROACH";
                this.Close();        
            }
        }

        private void button_NewProject_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_FindDll_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "dll | *.dll";
            dialog.ShowDialog();

            textBox_DllFilePath.Text = dialog.FileName;
        }

        private void button_FindVariablesFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV | *.csv";
            dialog.ShowDialog();

            textBox_DllVariablesNameValueFile.Text = dialog.FileName;
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                button_NewProject_Ok_Click(null, null);
            }
            else if(e.KeyChar == (char)Keys.Escape)
            {
                button_NewProject_Cancel_Click(null, null);
            }
        }

        private void textBox_NewProject_Leave(object sender, EventArgs e)
        {
            bool containsFileTermination = false;
            //Se busca si ya se puso la terminacion de archivo
            foreach (char item in textBox_NewProject.Text)
            {
                if(item == '.')
                    containsFileTermination = true;
            }
            //Sino la tiene se le pone
            if(containsFileTermination == false)
                textBox_NewProject.Text += ".xml"; 
        }
    }
}
