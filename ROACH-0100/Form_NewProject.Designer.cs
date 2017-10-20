namespace ROACH_0100
{
    partial class Form_NewProject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_FilePath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_DllFilePath = new System.Windows.Forms.TextBox();
            this.button_FindDll = new System.Windows.Forms.Button();
            this.textBox_DllMethodName = new System.Windows.Forms.TextBox();
            this.button_FindVariablesFile = new System.Windows.Forms.Button();
            this.textBox_DllVariablesNameValueFile = new System.Windows.Forms.TextBox();
            this.button_NewProject_Ok = new System.Windows.Forms.Button();
            this.button_NewProject_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_NewProject = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_FilePath
            // 
            this.label_FilePath.AutoSize = true;
            this.label_FilePath.Location = new System.Drawing.Point(13, 61);
            this.label_FilePath.Name = "label_FilePath";
            this.label_FilePath.Size = new System.Drawing.Size(239, 13);
            this.label_FilePath.TabIndex = 0;
            this.label_FilePath.Text = "Dirección del archivo .dll con el modelo a evaluar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nombre del método (Médoto expuesto por el dll)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Direccion del archivo con las variables de inicio(.CSV)";
            // 
            // textBox_DllFilePath
            // 
            this.textBox_DllFilePath.Location = new System.Drawing.Point(12, 77);
            this.textBox_DllFilePath.Name = "textBox_DllFilePath";
            this.textBox_DllFilePath.Size = new System.Drawing.Size(373, 20);
            this.textBox_DllFilePath.TabIndex = 6;
            // 
            // button_FindDll
            // 
            this.button_FindDll.Location = new System.Drawing.Point(391, 75);
            this.button_FindDll.Name = "button_FindDll";
            this.button_FindDll.Size = new System.Drawing.Size(75, 23);
            this.button_FindDll.TabIndex = 2;
            this.button_FindDll.Text = "Buscar";
            this.button_FindDll.UseVisualStyleBackColor = true;
            this.button_FindDll.Click += new System.EventHandler(this.button_FindDll_Click);
            // 
            // textBox_DllMethodName
            // 
            this.textBox_DllMethodName.Location = new System.Drawing.Point(12, 127);
            this.textBox_DllMethodName.Name = "textBox_DllMethodName";
            this.textBox_DllMethodName.Size = new System.Drawing.Size(454, 20);
            this.textBox_DllMethodName.TabIndex = 3;
            this.textBox_DllMethodName.Text = "SteppedSimulation";
            // 
            // button_FindVariablesFile
            // 
            this.button_FindVariablesFile.Location = new System.Drawing.Point(391, 175);
            this.button_FindVariablesFile.Name = "button_FindVariablesFile";
            this.button_FindVariablesFile.Size = new System.Drawing.Size(75, 23);
            this.button_FindVariablesFile.TabIndex = 4;
            this.button_FindVariablesFile.Text = "Buscar";
            this.button_FindVariablesFile.UseVisualStyleBackColor = true;
            this.button_FindVariablesFile.Click += new System.EventHandler(this.button_FindVariablesFile_Click);
            // 
            // textBox_DllVariablesNameValueFile
            // 
            this.textBox_DllVariablesNameValueFile.Location = new System.Drawing.Point(12, 177);
            this.textBox_DllVariablesNameValueFile.Name = "textBox_DllVariablesNameValueFile";
            this.textBox_DllVariablesNameValueFile.Size = new System.Drawing.Size(372, 20);
            this.textBox_DllVariablesNameValueFile.TabIndex = 7;
            // 
            // button_NewProject_Ok
            // 
            this.button_NewProject_Ok.Location = new System.Drawing.Point(161, 219);
            this.button_NewProject_Ok.Name = "button_NewProject_Ok";
            this.button_NewProject_Ok.Size = new System.Drawing.Size(74, 23);
            this.button_NewProject_Ok.TabIndex = 5;
            this.button_NewProject_Ok.Text = "Aceptar";
            this.button_NewProject_Ok.UseVisualStyleBackColor = true;
            this.button_NewProject_Ok.Click += new System.EventHandler(this.button_NewProject_Ok_Click);
            // 
            // button_NewProject_Cancel
            // 
            this.button_NewProject_Cancel.Location = new System.Drawing.Point(242, 219);
            this.button_NewProject_Cancel.Name = "button_NewProject_Cancel";
            this.button_NewProject_Cancel.Size = new System.Drawing.Size(74, 23);
            this.button_NewProject_Cancel.TabIndex = 8;
            this.button_NewProject_Cancel.Text = "Cancelar";
            this.button_NewProject_Cancel.UseVisualStyleBackColor = true;
            this.button_NewProject_Cancel.Click += new System.EventHandler(this.button_NewProject_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nombre del projecto";
            // 
            // textBox_NewProject
            // 
            this.textBox_NewProject.Location = new System.Drawing.Point(13, 30);
            this.textBox_NewProject.Name = "textBox_NewProject";
            this.textBox_NewProject.Size = new System.Drawing.Size(453, 20);
            this.textBox_NewProject.TabIndex = 1;
            this.textBox_NewProject.Leave += new System.EventHandler(this.textBox_NewProject_Leave);
            // 
            // Form_NewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 254);
            this.Controls.Add(this.textBox_NewProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_NewProject_Cancel);
            this.Controls.Add(this.button_NewProject_Ok);
            this.Controls.Add(this.button_FindVariablesFile);
            this.Controls.Add(this.textBox_DllVariablesNameValueFile);
            this.Controls.Add(this.button_FindDll);
            this.Controls.Add(this.textBox_DllMethodName);
            this.Controls.Add(this.textBox_DllFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_FilePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_NewProject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crear nuevo proyecto...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_FilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_DllFilePath;
        private System.Windows.Forms.Button button_FindDll;
        private System.Windows.Forms.TextBox textBox_DllMethodName;
        private System.Windows.Forms.Button button_FindVariablesFile;
        private System.Windows.Forms.TextBox textBox_DllVariablesNameValueFile;
        private System.Windows.Forms.Button button_NewProject_Ok;
        private System.Windows.Forms.Button button_NewProject_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_NewProject;
    }
}