using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROACH_0100
{
    /// <summary>
    /// Clase que permite el uso de metodos no manejados y desconocidos en compilación.
    /// </summary>
    [Serializable]
    public class LinkerToUnamangedLibrary
    {
        #region Properties
        /// <summary>
        /// Direccion del archivo DLL a cargar.
        /// </summary>
        public string FilePath { get; set; }//private set; }

        /// <summary>
        /// Nombre de la funcion que llama.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Cantidad de parametros del metodo a llamar desde el DLL.
        /// </summary>
        public int ParamsQuantity { get; set; }//private set; }

        /// <summary>
        /// Arreglo de valores flotantes que guarda los datos actuales para la comunicacion con el DLL.
        /// </summary>
        public float[] ParamsValues { get; set; }

        /// <summary>
        /// Arreglo de valores que guardan una copia de los datos con que fue inicializada la simulación.
        /// </summary>
        public float[] ParamsOriginalValues { get; set; }//private set; }

        /// <summary>
        /// Nombres de los parámetros de la simulacion.
        /// </summary>
        public List<string> ParamsNames { get; set; }

        /// <summary>
        /// Objeto que se encarga de manejar el DLL en C requerida.
        /// </summary>
        private UnmanagedLibrary ul;

        private UnmanagedMethod_25Params_Delegate unmanaged25;
        private UnmanagedMethod_50Params_Delegate unmanaged50;
        private UnmanagedMethod_75Params_Delegate unmanaged75;
        #endregion Properties

        #region Constructors
        public LinkerToUnamangedLibrary()
        {

        }

        /// <summary>
        /// Inicializa una instancia del objeto y el arreglo de valores a una cantidad fija de valores.
        /// </summary>
        /// <param name="lenght"></param>
        /// <param name="filepath"></param>
        /// <param name="methodName"></param>
        public LinkerToUnamangedLibrary(int lenght, string filepath, string methodName)
        {
            ul = new UnmanagedLibrary(filepath);                        
            if (lenght <= 25)
            {
                this.ParamsQuantity = 25;
                unmanaged25 = ul.GetUnmanagedFunction<UnmanagedMethod_25Params_Delegate>(methodName);
            }
            else if (lenght <= 50)
            {
                this.ParamsQuantity = 50;
                unmanaged50 = ul.GetUnmanagedFunction<UnmanagedMethod_50Params_Delegate>(methodName);
            }
            else
            {
                this.ParamsQuantity = 75;
                unmanaged75 = ul.GetUnmanagedFunction<UnmanagedMethod_75Params_Delegate>(methodName);
            }
            this.MethodName = methodName;
            this.ParamsValues = this.ParamsOriginalValues = new float[ParamsQuantity];
            this.FilePath = filepath;
            this.ParamsNames = new List<string>();
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Asigna los valores de inicialización al arreglo de valores. Guarda una copia para concepto de
        /// reinicializacion.
        /// </summary>
        /// <param name="value"></param>
        public void AssignValues(params float[] value)
        {            
            try
            {
                for (int i = 0; i < this.ParamsValues.Length; i++)
                {
                    if (i < value.Length)
                        this.ParamsValues[i] = value[i];
                    else
                        this.ParamsValues[i] = 0.0f;
                }

                this.ParamsOriginalValues = (float[])this.ParamsValues.Clone();
            }
            catch(ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

        public void ChangeAValue(string paramName, float value)
        {
            ParamsValues[ParamsNames.IndexOf(paramName)] = value;
        }

        /// <summary>
        /// Permite el cambio de un valor en el arreglo de los valores iniciales de la simulación.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void ChangeAnOriginalValue(string paramName, float value)
        {
            ParamsOriginalValues[ParamsNames.IndexOf(paramName)] = value;
        }

        public void ChangeAllOriginalValues(float[] values)
        {
            ParamsValues = values;
            ParamsQuantity = ParamsValues.Length;
        }

        public void ChangeAllNames(List<string> names)
        {
            ParamsNames = names;
        }

        /// <summary>
        /// Devuelve el valor de la variable requerida en base al nombre de la variable.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public float GetDesiredValue(string paramName)
        {            
            return ParamsValues[ParamsNames.IndexOf(paramName)];
        }

        /// <summary>
        /// Reinicia el arreglo de las variables a sus valores originales.
        /// </summary>
        public void ResetValues()
        {
            ParamsValues = (float[])ParamsOriginalValues.Clone();
        }

        /// <summary>
        /// Se llama al metodo no administrado referido en la inicilizacion de este objeto.
        /// </summary>
        public void CallToUnmanagedMethod()
        {
            if(ParamsQuantity <= 50)
            {
                unmanaged50(
                    out ParamsValues[0], out ParamsValues[1], out ParamsValues[2], out ParamsValues[3], out ParamsValues[4],
                    out ParamsValues[5], out ParamsValues[6], out ParamsValues[7], out ParamsValues[8], out ParamsValues[9],
                    out ParamsValues[10], out ParamsValues[11], out ParamsValues[12], out ParamsValues[13], out ParamsValues[14],
                    out ParamsValues[15], out ParamsValues[16], out ParamsValues[17], out ParamsValues[18], out ParamsValues[19],
                    out ParamsValues[20], out ParamsValues[21], out ParamsValues[22], out ParamsValues[23], out ParamsValues[24],

                    out ParamsValues[25], out ParamsValues[26], out ParamsValues[27], out ParamsValues[28], out ParamsValues[29],
                    out ParamsValues[30], out ParamsValues[31], out ParamsValues[32], out ParamsValues[33], out ParamsValues[34],
                    out ParamsValues[35], out ParamsValues[36], out ParamsValues[37], out ParamsValues[38], out ParamsValues[39],
                    out ParamsValues[40], out ParamsValues[41], out ParamsValues[42], out ParamsValues[43], out ParamsValues[44],
                    out ParamsValues[45], out ParamsValues[46], out ParamsValues[47], out ParamsValues[48], out ParamsValues[49]);
            }
            else if(ParamsQuantity <= 25)
            {
                unmanaged25(
                    out ParamsValues[0], out ParamsValues[1], out ParamsValues[2], out ParamsValues[3], out ParamsValues[4],
                    out ParamsValues[5], out ParamsValues[6], out ParamsValues[7], out ParamsValues[8], out ParamsValues[9],
                    out ParamsValues[10], out ParamsValues[11], out ParamsValues[12], out ParamsValues[13], out ParamsValues[14],
                    out ParamsValues[15], out ParamsValues[16], out ParamsValues[17], out ParamsValues[18], out ParamsValues[19],
                    out ParamsValues[20], out ParamsValues[21], out ParamsValues[22], out ParamsValues[23], out ParamsValues[24]);
            }
            else
            {
                unmanaged75(
                    out ParamsValues[0], out ParamsValues[1], out ParamsValues[2], out ParamsValues[3], out ParamsValues[4],
                    out ParamsValues[5], out ParamsValues[6], out ParamsValues[7], out ParamsValues[8], out ParamsValues[9],
                    out ParamsValues[10], out ParamsValues[11], out ParamsValues[12], out ParamsValues[13], out ParamsValues[14],
                    out ParamsValues[15], out ParamsValues[16], out ParamsValues[17], out ParamsValues[18], out ParamsValues[19],
                    out ParamsValues[20], out ParamsValues[21], out ParamsValues[22], out ParamsValues[23], out ParamsValues[24],

                    out ParamsValues[25], out ParamsValues[26], out ParamsValues[27], out ParamsValues[28], out ParamsValues[29],
                    out ParamsValues[30], out ParamsValues[31], out ParamsValues[32], out ParamsValues[33], out ParamsValues[34],
                    out ParamsValues[35], out ParamsValues[36], out ParamsValues[37], out ParamsValues[38], out ParamsValues[39],
                    out ParamsValues[40], out ParamsValues[41], out ParamsValues[42], out ParamsValues[43], out ParamsValues[44],
                    out ParamsValues[45], out ParamsValues[46], out ParamsValues[47], out ParamsValues[48], out ParamsValues[49],

                    out ParamsValues[50], out ParamsValues[51], out ParamsValues[52], out ParamsValues[53], out ParamsValues[54],
                    out ParamsValues[55], out ParamsValues[56], out ParamsValues[57], out ParamsValues[58], out ParamsValues[59],
                    out ParamsValues[60], out ParamsValues[61], out ParamsValues[62], out ParamsValues[63], out ParamsValues[64],
                    out ParamsValues[65], out ParamsValues[66], out ParamsValues[67], out ParamsValues[68], out ParamsValues[69],
                    out ParamsValues[70], out ParamsValues[71], out ParamsValues[72], out ParamsValues[73], out ParamsValues[74]
                    );
            }
        }
        #endregion Methods

        #region Delegates
        /// <summary>
        /// Delegado a una función no manejada que toma 25 parametros pasados por referencia.
        /// </summary>        
        public delegate void UnmanagedMethod_25Params_Delegate(
            out float val1, out float val2, out float val3, out float val4, out float val5,
            out float val6, out float val7, out float val8, out float val9, out float val10,
            out float val11, out float val12, out float val13, out float val14, out float val15,
            out float val16, out float val17, out float val18, out float val19, out float val20,
            out float val21, out float val22, out float val23, out float val24, out float val25);

        /// <summary>
        /// Delegado a una función no manejada que toma 50 parametros pasados por referencia.
        /// </summary>        
        public delegate void UnmanagedMethod_50Params_Delegate(
            out float val1, out float val2, out float val3, out float val4, out float val5,
            out float val6, out float val7, out float val8, out float val9, out float val10,
            out float val11, out float val12, out float val13, out float val14, out float val15,
            out float val16, out float val17, out float val18, out float val19, out float val20,
            out float val21, out float val22, out float val23, out float val24, out float val25,            
            
            out float val26, out float val27, out float val28, out float val29, out float val30,
            out float val31, out float val32, out float val33, out float val34, out float val35,
            out float val36, out float val37, out float val38, out float val39, out float val40,
            out float val41, out float val42, out float val43, out float val44, out float val45,
            out float val46, out float val47, out float val48, out float val49, out float val50);

        /// <summary>
        /// Delegado a una función no manejada que toma 75 parametros pasados por referencia.
        /// </summary>
        public delegate void UnmanagedMethod_75Params_Delegate(
           out float val1, out float val2, out float val3, out float val4, out float val5,
           out float val6, out float val7, out float val8, out float val9, out float val10,
           out float val11, out float val12, out float val13, out float val14, out float val15,
           out float val16, out float val17, out float val18, out float val19, out float val20,
           out float val21, out float val22, out float val23, out float val24, out float val25,

           out float val26, out float val27, out float val28, out float val29, out float val30,
           out float val31, out float val32, out float val33, out float val34, out float val35,
           out float val36, out float val37, out float val38, out float val39, out float val40,
           out float val41, out float val42, out float val43, out float val44, out float val45,
           out float val46, out float val47, out float val48, out float val49, out float val50,
           
           out float val51, out float val52, out float val53, out float val54, out float val55,
           out float val56, out float val57, out float val58, out float val59, out float val60,
           out float val61, out float val62, out float val63, out float val64, out float val65,
           out float val66, out float val67, out float val68, out float val69, out float val70,
           out float val71, out float val72, out float val73, out float val74, out float val75);
        #endregion Delegates
    }
}
