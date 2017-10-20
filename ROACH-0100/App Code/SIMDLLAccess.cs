using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

namespace ROACH_0100
{
    /// <summary>
    /// [Obsoleto] Clase que se encarga de de llamar a un dll en C.
    /// </summary>
    [Obsolete("Se utilizó al principio del proyecto cuando no existia la carga dínamica del DLL en C", true)]
    public class SIMDLLAccess
    {
        public const string filePath = @"C:\Users\Ramm\Documents\Visual Studio 2013\Projects\Tesis\ROACH-0100-Template\Debug\SIMDLL.dll";
        

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

        public delegate void UnmanagedMethod_48Params_Delegate(
            out float val1, out float val2, out float val3, out float val4, out float val5,
            out float val6, out float val7, out float val8, out float val9, out float val10,
            out float val11, out float val12, out float val13, out float val14, out float val15,
            out float val16, out float val17, out float val18, out float val19, out float val20,
            out float val21, out float val22, out float val23, out float val24, out float val25,

            out float val26, out float val27, out float val28, out float val29, out float val30,
            out float val31, out float val32, out float val33, out float val34, out float val35,
            out float val36, out float val37, out float val38, out float val39, out float val40,
            out float val41, out float val42, out float val43, out float val44, out float val45,
            out float val46, out float val47, out float val48);

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

        #region Constants
        public const float DEFAULT_V_T = 10.0F;
        public const float DEFAULT_TIMESTEP = 0.1666e-4f;
        public const float DEFAULT_I_S = 1.0f;
        public const float DEFAULT_G_OBSERVER = 0.02f;
        #endregion Constants

        #region Simulation's Values
        /// <summary>
        /// Periodo de muestreo en segundos.
        /// </summary>
        public float timeStep = DEFAULT_TIMESTEP;
        /// <summary>
        /// Tiempo total de evaluado.
        /// </summary>
        public float eval_time = 0.0f;

        public float vt_des = 70.0f; //Default Output VOltage = 500V
        public float vd_des = 0.0f;//Default difference = 0V
        public float vs_amplitude = 34.0f;//Default amplitude = 180V
        public float vs_frequency = 377.0f;//Default frequency = 120*pi ~= 377 rad/s
        
        public float r1 = 100.0f;
        public float r2 = 100.0f;
        public float C = 470e-6f;
        public float L = 10e-3f;

        #region Source's signals
        /// <summary>
        /// Rectificador: Señal de la fuente de entrada.
        /// </summary>
        public float v_s = 0.0f;
        /// <summary>
        /// Rectificador: Integral de la señal de la fuente de entrada.
        /// </summary>
        public float Iv_s = 0.0f;       
        #endregion Source's signals

        #region Rectifier's signals
        /// <summary>
        /// Rectificador: Voltaje total(suma de voltajes de los dos capacitores) deseado a la salida.
        /// </summary>
        public float v_t = DEFAULT_V_T;
        /// <summary>
        /// Rectificador: Diferencia de voltaje entre los dos capacitores a la salida.
        /// </summary>
        public float v_d = 0.0f;
        /// <summary>
        /// Rectificador: Señal de la corriente de salida.
        /// </summary>
        public float i_s = DEFAULT_I_S;

        //Variables de integracion del rectificador(Retroalimentacion)-----------

        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        public float v_t_former = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        public float v_d_former = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        public float i_s_former = 0.0f;

        //Salida del circuito----------------------------------------------------

        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        public float Dv_t = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        public float Dv_d = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        public float Di_s = 0.0f;
        #endregion Rectifier's signals

        #region Controller's signals
        public float k = 100000.0f;
        public float kp1 = 0.01f;
        public float kp2 = 250.0f;
        public float ki1 = 10.0f;
        public float ki2 = 500.0f;

        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        public float eta = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        public float eps = 0.0f;
        /// <summary>
        /// Controlador: Señal de control para el interruptor U1.
        /// </summary>
        public float u1 = 0.0f;
        /// <summary>
        /// Controlador: Señal de control para el interruptor U2.
        /// </summary>
        public float u2 = 0.0f;

        //Variable de intregracion del controlador(Retroalimentacion)-----------
        
        /// <summary>
        /// Controlador: Necesario para el computo de la integracion.
        /// </summary>
        public float eta_former = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo de la integracion.
        /// </summary>
        public float eps_former = 0.0f;

        //Salidas del controlador-----------------------------------------------

        /// <summary>
        /// Controlador: Señal de corriente de salida deseada.
        /// </summary>
        public float i_s_des_out = 0.0f;
        /// <summary>
        /// Controlador: Error del voltaje total(suma de voltajes de los dos capacitores) de salida.
        /// </summary>
        public float v_t_error_out = 0.0f;
        /// <summary>
        /// Controlador: Error de la diferencia de voltaje entre los dos capacitores de salida.
        /// </summary>
        public float v_d_error_out = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        public float Deta = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        public float Deps = 0.0f;
        #endregion Controller's signals

        #region Observer's signals
        /// <summary>
        /// Observador: Señal parcial de salida del observador.
        /// </summary>
        public float beta1 = 0.0f;
        /// <summary>
        /// Observador: Señal parcial de salida del observador.
        /// </summary>
        public float beta2 = 0.0f;
        /// <summary>
        /// Observador: Señal de entrada/parcial de salida del observador.
        /// </summary>
        public float g_error1 = 0.0f;
        /// <summary>
        /// Observador: Señal de entrada/parcial de salida del observador.
        /// </summary>
        public float g_error2 = 0.0f;

        //Variable de intregracion del observador(Retroalimentacion)--------

        /// <summary>
        /// Observador: Necesario para el computo de la integracion.
        /// </summary>
        public float g_error1_former = 0.0f;
        /// <summary>
        /// Observador: Necesario para el computo de la integracion.
        /// </summary>
        public float g_error2_former = 0.0f;

        //Salida del observador----------------------------------------------

        /// <summary>
        /// Observador: Señal de pre-salida del observador.
        /// </summary>
        public float Dg_error1 = 0.0f;
        /// <summary>
        /// Observador: Señal de pre-salida del observador.
        /// </summary>
        public float Dg_error2 = 0.0f;

        //Auxiliares del observador------------------------------------------

        /// <summary>
        /// Observador: Señal de salida del observador(Resistencia estimada de R1).
        /// </summary>
        public float g1_Observer = DEFAULT_G_OBSERVER;
        /// <summary>
        /// Observador: Señal de salida del observador(Resistencia estimada de R2).
        /// </summary>
        public float g2_Observer = DEFAULT_G_OBSERVER;

        public float gamma1 = 100e-5f;
        public float gamma2 = 100e-5f;

            #endregion Observer's signals
        #endregion Simulation's values

        public UnmanagedMethod_48Params_Delegate UnmanagedCall;
        //public UnmanagedMethod_50Params_Delegate UnmanagedCall;

        #region Constructors
        /// <summary>
        /// Inicializa una nueva instancia que contiene las variables y métodos necesarios para la simulacion
        /// del modelo del rectificador.
        /// </summary>
        public SIMDLLAccess()
        {
            //LinkerToUnamangedLibrary lul = new LinkerToUnamangedLibrary(48, filePath);
            UnmanagedLibrary ul = new UnmanagedLibrary(filePath);

            UnmanagedCall =
                ul.GetUnmanagedFunction<UnmanagedMethod_48Params_Delegate>("SteppedSimulation");
            
            //UnmanagedCall =
            //    ul.GetUnmanagedFunction<UnmanagedMethod_50Params_Delegate>("SteppedSimulation");
            
        }
        #endregion Constructors

        #region C DLL Methods
        /// <summary>
        /// Realiza un paso en la simulacion del "Rectificador Multinivel Monofasico Hibrido".
        /// </summary>
        /// <param name="eval_time">Tiempo total de evaluado.</param>
        /// <param name="timeStep">Periodo de muestreo en segundos(Ejemplo: 0.1666e-4f)</param>
        /// <param name="desiredSimulation">Tipo de simulacion deseada(Opciones: "rectifier", "controller" u "observer")</param>
        /// <param name="v_s">Rectificador: Señal de la fuente de entrada.</param>
        /// <param name="v_t">Rectificador: Voltaje total(suma de voltajes de los dos capacitores) deseado a la salida.</param>
        /// <param name="v_d">Rectificador: Diferencia de voltaje entre los dos capacitores a la salida.</param>
        /// <param name="i_s">Rectificador: Señal de la corriente de salida.</param>
        /// <param name="v_t_former">Rectificador: Necesario para el computo de la integracion.</param>
        /// <param name="v_d_former">Rectificador: Necesario para el computo de la integracion.</param>
        /// <param name="i_s_former">Rectificador: Necesario para el computo de la integracion.</param>
        /// <param name="Dv_t">Rectificador: Necesario para el computo.</param>
        /// <param name="Dv_d">Rectificador: Necesario para el computo.</param>
        /// <param name="Di_s">Rectificador: Necesario para el computo.</param>
        /// <param name="eta">Controlador: Necesario para el computo.</param>
        /// <param name="eps">Controlador: Necesario para el computo.</param>
        /// <param name="u1">Controlador: Señal de control para el interruptor U1.</param>
        /// <param name="u2">Controlador: Señal de control para el interruptor U2.</param>
        /// <param name="eta_former">Controlador: Necesario para el computo de la integracion.</param>
        /// <param name="eps_former">Controlador: Necesario para el computo de la integracion.</param>
        /// <param name="i_s_des_out">Controlador: Señal de corriente de salida deseada.</param>
        /// <param name="v_t_error_out">Controlador: Error del voltaje total(suma de voltajes de los dos capacitores) de salida.</param>
        /// <param name="v_d_error_out">Controlador: Error de la diferencia de voltaje entre los dos capacitores de salida.</param>
        /// <param name="Deta">Controlador: Necesario para el computo.</param>
        /// <param name="Deps">Controlador: Necesario para el computo.</param>
        /// <param name="beta1">Observador: Señal parcial de salida del observador.</param>
        /// <param name="beta2">Observador: Señal parcial de salida del observador.</param>
        /// <param name="g_error1">Observador: Señal de entrada/parcial de salida del observador.</param>
        /// <param name="g_error2">Observador: Señal de entrada/parcial de salida del observador.</param>
        /// <param name="g_error1_former">Observador: Necesario para el computo de la integracion.</param>
        /// <param name="g_error2_former">Observador: Necesario para el computo de la integracion.</param>
        /// <param name="Dg_error1">Observador: Señal de pre-salida del observador.</param>
        /// <param name="Dg_error2">Observador: Señal de pre-salida del observador.</param>
        /// <param name="g1_Observer">Observador: Señal de salida del observador(Resistencia estimada de R1).</param>
        /// <param name="g2_Observer">Observador: Señal de salida del observador(Resistencia estimada de R2).</param>
        [DllImport(filePath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SteppedSimulation(out float eval_time, out float timeStep,  //Simulacion
            out float vt_des, out float vd_des, out float v_s, out float vs_amplitude, out float vs_frequency,
            out float r1, out float r2, out float C, out float L,
            out float v_t, out float v_d, out float i_s, //Rectificador
            out float v_t_former, out float v_d_former, out float i_s_former,
            out float Dv_t, out float Dv_d, out float Di_s,
            out float eta, out float eps, out float u1, out float u2, //Controlador
            out float eta_former, out float eps_former, out float i_s_des_out, out float v_t_error_out, out float v_d_error_out,
            out float Deta, out float Deps, out float k, out float kp1, out float kp2, out float ki1, out float ki2,
            out float beta1, out float beta2, out float g_error1, out float g_error2, //Observador
            out float g_error1_former, out float g_error2_former,
            out float Dg_error1, out float Dg_error2,
            out float g1_Observer, out float g2_Observer, out float gamma1, out float gamma2);
        #endregion C DLL Methods

        #region Methods
        /// <summary>
        /// Devuelve los tipos de señales de salida del RCO.
        /// </summary>
        /// <returns></returns>
        static public List<string> GetOutSignals()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add("v_t");
            aux_list.Add("v_d");
            aux_list.Add("i_s");

            return aux_list;
        }

        /// <summary>
        /// Devuele los tipos de señales internas del RCO.
        /// </summary>
        /// <returns></returns>
        static public List<string> GetControlSignals()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add("eta");
            aux_list.Add("eps");
            aux_list.Add("u1");
            aux_list.Add("u2");
            aux_list.Add("beta1");
            aux_list.Add("beta2");
            aux_list.Add("g_error1");
            aux_list.Add("g_error2");
            aux_list.Add("i_s_des");
            aux_list.Add("v_t_error");
            aux_list.Add("v_d_error");

            return aux_list;
        }

        /// <summary>
        /// Devuelve todas las señales, tanto internas como externas, del RCO.
        /// </summary>
        /// <returns></returns>
        static public List<string> GetSignalsNames()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add("vt_des");//Parametros de circuito
            aux_list.Add("vd_des");
            aux_list.Add("v_s");
            aux_list.Add("vs_amplitude");
            aux_list.Add("vs_frequency");
            aux_list.Add("r1");
            aux_list.Add("r2");
            aux_list.Add("C");
            aux_list.Add("L");

            aux_list.Add("v_t");//Rectificador
            aux_list.Add("v_d");
            aux_list.Add("i_s");

            aux_list.Add("eta");//Controlador
            aux_list.Add("eps");
            aux_list.Add("u1");
            aux_list.Add("u2");

            aux_list.Add("beta1");//Observador
            aux_list.Add("beta2");
            aux_list.Add("g_error1");
            aux_list.Add("g_error2");
            aux_list.Add("i_s_des");
            aux_list.Add("v_t_error");
            aux_list.Add("v_d_error");

            return aux_list;
        }

        /// <summary>
        /// Devuelve el valor deseado de la simulación dependiendo la cadena pasada.
        /// </summary>
        /// <param name="value">Cadena con el nombre de la señal que se desea obtener.</param>
        /// <returns></returns>
        public float GetDesiredValue(string value)
        {
            switch (value)
            {
                case "v_t":
                    return v_t;
                case "v_d":
                    return v_d;
                case "i_s":
                    return i_s;
                case "eta":
                    return eta;
                case "eps":
                    return eps;
                case "u1":
                    return u1;
                case "u2":
                    return u2;
                case "beta1":
                    return beta1;
                case "beta2":
                    return beta2;
                case "g_error1":
                    return g_error1;
                case "g_error2":
                    return g_error2;
                case "i_s_des":
                    return i_s_des_out;
                case "v_t_error":
                    return v_t_error_out;
                case "v_d_error":
                    return v_d_error_out;
                default:
                    return v_t;
            }
        }

        /// <summary>
        /// Cambia el valor de una variable según el nombre de esta.
        /// </summary>
        /// <param name="variableName">Nombre de la variable.</param>
        /// <param name="value">Valor flotante.</param>
        public void ChangeDesiredValue(string variableName, float value)
        {
            switch (variableName)
            {
                case "vt_des":
                    vt_des = value;
                    return;
                case "vd_des":
                    vd_des = value;
                    return;
                case "v_t":
                    v_t = value;
                    return;
                case "v_d":
                    v_d = value;
                    return;
                case "i_s":
                    i_s = value;
                    return;
                case "eta":
                    eta = value;
                    return;
                case "eps":
                    eps = value;
                    return;
                case "u1":
                    u1 = value;
                    return;
                case "u2":
                    u2 = value;
                    return;
                case "beta1":
                    beta1 = value;
                    return;
                case "beta2":
                    beta2 = value;
                    return;
                case "g_error1":
                    g_error1 = value;
                    return;
                case "g_error2":
                    g_error2 = value;
                    return;
                case "i_s_des":
                    i_s_des_out = value;
                    return;
                case "v_t_error":
                    v_t_error_out = value;
                    return;
                case "v_d_error":
                    v_d_error_out = value;
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// Reinicializa las variables del objeto para la simulación.
        /// </summary>
        public void ResetVariables()
        {
            timeStep = DEFAULT_TIMESTEP;
            eval_time = 0.0f;

            v_s = 0.0f;
            Iv_s = 0.0f;

            v_t = DEFAULT_V_T;
            v_d = 0.0f;
            i_s = DEFAULT_I_S;

            v_t_former = 0.0f;
            v_d_former = 0.0f;
            i_s_former = 0.0f;

            Dv_t = 0.0f;
            Dv_d = 0.0f;
            Di_s = 0.0f;

            eta = 0.0f;
            eps = 0.0f;
            u1 = 0.0f;
            u2 = 0.0f;
            eta_former = 0.0f;
            eps_former = 0.0f;

            i_s_des_out = 0.0f;
            v_t_error_out = 0.0f;
            v_d_error_out = 0.0f;
            Deta = 0.0f;
            Deps = 0.0f;

            beta1 = 0.0f;
            beta2 = 0.0f;
            g_error1 = 0.0f;
            g_error2 = 0.0f;
            g_error1_former = 0.0f;
            g_error2_former = 0.0f;

            Dg_error1 = 0.0f;
            Dg_error2 = 0.0f;

            g1_Observer = g2_Observer = DEFAULT_G_OBSERVER;
        }
        #endregion Methods
    }
}

#region Legacy
/*
        #region Simulation's Values
        /// <summary>
        /// Periodo de muestreo en segundos.
        /// </summary>
        static float timeStep = 0.1666e-4f;
        /// <summary>
        /// Tiempo total de evaluado.
        /// </summary>
        static float eval_time = 0.0f;

        #region Source's signals
        /// <summary>
        /// Rectificador: Señal de la fuente de entrada.
        /// </summary>
        static float v_s = 0.0f;
        /// <summary>
        /// Rectificador: Integral de la señal de la fuente de entrada.
        /// </summary>
        static float Iv_s = 0.0f;
        /// <summary>
        /// [SIMDLL_Access] No se para que sirve por el momento.
        /// </summary>
        static float aux = 0.0f;
        #endregion

        #region Rectifier's signals
        /// <summary>
        /// Rectificador: Voltaje total(suma de voltajes de los dos capacitores) deseado a la salida.
        /// </summary>
        static float v_t = 10.0f;
        /// <summary>
        /// Rectificador: Diferencia de voltaje entre los dos capacitores a la salida.
        /// </summary>
        static float v_d = 0.0f;
        /// <summary>
        /// Rectificador: Señal de la corriente de salida.
        /// </summary>
        static float i_s = 1.0f;

        //Variables de integracion del rectificador(Retroalimentacion)-----------

        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        static float v_t_former = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        static float v_d_former = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        static float i_s_former = 0.0f;

        //Salida del circuito----------------------------------------------------

        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        static float Dv_t = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        static float Dv_d = 0.0f;
        /// <summary>
        /// Rectificador: Necesario para el computo de la integracion.
        /// </summary>
        static float Di_s = 0.0f;
        #endregion Rectifier's signals

        #region Controller's signals
        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        static float eta = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        static float eps = 0.0f;
        /// <summary>
        /// Controlador: Señal de control para el interruptor U1.
        /// </summary>
        static float u1 = 0.0f;
        /// <summary>
        /// Controlador: Señal de control para el interruptor U2.
        /// </summary>
        static float u2 = 0.0f;

        //Variable de intregracion del controlador(Retroalimentacion)-----------

        /// <summary>
        /// Controlador: Necesario para el computo de la integracion.
        /// </summary>
        static float eta_former = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo de la integracion.
        /// </summary>
        static float eps_former = 0.0f;

        //Salidas del controlador-----------------------------------------------

        /// <summary>
        /// Controlador: Señal de corriente de salida deseada.
        /// </summary>
        static float i_s_des_out = 0.0f;
        /// <summary>
        /// Controlador: Error del voltaje total(suma de voltajes de los dos capacitores) de salida.
        /// </summary>
        static float v_t_error_out = 0.0f;
        /// <summary>
        /// Controlador: Error de la diferencia de voltaje entre los dos capacitores de salida.
        /// </summary>
        static float v_d_error_out = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        static float Deta = 0.0f;
        /// <summary>
        /// Controlador: Necesario para el computo.
        /// </summary>
        static float Deps = 0.0f;
        #endregion Controller's signals

        #region Observer's signals
        /// <summary>
        /// Observador: Señal parcial de salida del observador.
        /// </summary>
        static float beta1 = 0.0f;
        /// <summary>
        /// Observador: Señal parcial de salida del observador.
        /// </summary>
        static float beta2 = 0.0f;
        /// <summary>
        /// Observador: Señal de entrada/parcial de salida del observador.
        /// </summary>
        static float g_error1 = 0.0f;
        /// <summary>
        /// Observador: Señal de entrada/parcial de salida del observador.
        /// </summary>
        static float g_error2 = 0.0f;

        //Variable de intregracion del observador(Retroalimentacion)--------

        /// <summary>
        /// Observador: Necesario para el computo de la integracion.
        /// </summary>
        static float g_error1_former = 0.0f;
        /// <summary>
        /// Observador: Necesario para el computo de la integracion.
        /// </summary>
        static float g_error2_former = 0.0f;

        //Salida del observador----------------------------------------------

        /// <summary>
        /// Observador: Señal de pre-salida del observador.
        /// </summary>
        static float Dg_error1 = 0.0f;
        /// <summary>
        /// Observador: Señal de pre-salida del observador.
        /// </summary>
        static float Dg_error2 = 0.0f;

        //Auxiliares del observador------------------------------------------

        /// <summary>
        /// Observador: Señal de salida del observador(Resistencia estimada de R1).
        /// </summary>
        static float g1_Observer = 0.02f;
        /// <summary>
        /// Observador: Señal de salida del observador(Resistencia estimada de R2).
        /// </summary>
        static float g2_Observer = 0.02f;
        #endregion Observer's signals
        #endregion SImulation's values

        #region C DLL Methods
        /// <summary>
        /// Realiza un paso en la simulacion del "Rectificador Multinivel Monofasico Hibrido".
        /// </summary>
        /// <param name="eval_time">Tiempo total de evaluado.</param>
        /// <param name="timeStep">Periodo de muestreo en segundos(Ejemplo: 0.1666e-4f)</param>
        /// <param name="desiredSimulation">Tipo de simulacion deseada(Opciones: "rectifier", "controller" u "observer")</param>
        /// <param name="v_s">Rectificador: Señal de la fuente de entrada.</param>
        /// <param name="v_t">Rectificador: Voltaje total(suma de voltajes de los dos capacitores) deseado a la salida.</param>
        /// <param name="v_d">Rectificador: Diferencia de voltaje entre los dos capacitores a la salida.</param>
        /// <param name="i_s">Rectificador: Señal de la corriente de salida.</param>
        /// <param name="v_t_former">Rectificador: Necesario para el computo de la integracion.</param>
        /// <param name="v_d_former">Rectificador: Necesario para el computo de la integracion.</param>
        /// <param name="i_s_former">Rectificador: Necesario para el computo de la integracion.</param>
        /// <param name="Dv_t">Rectificador: Necesario para el computo.</param>
        /// <param name="Dv_d">Rectificador: Necesario para el computo.</param>
        /// <param name="Di_s">Rectificador: Necesario para el computo.</param>
        /// <param name="eta">Controlador: Necesario para el computo.</param>
        /// <param name="eps">Controlador: Necesario para el computo.</param>
        /// <param name="u1">Controlador: Señal de control para el interruptor U1.</param>
        /// <param name="u2">Controlador: Señal de control para el interruptor U2.</param>
        /// <param name="eta_former">Controlador: Necesario para el computo de la integracion.</param>
        /// <param name="eps_former">Controlador: Necesario para el computo de la integracion.</param>
        /// <param name="i_s_des_out">Controlador: Señal de corriente de salida deseada.</param>
        /// <param name="v_t_error_out">Controlador: Error del voltaje total(suma de voltajes de los dos capacitores) de salida.</param>
        /// <param name="v_d_error_out">Controlador: Error de la diferencia de voltaje entre los dos capacitores de salida.</param>
        /// <param name="Deta">Controlador: Necesario para el computo.</param>
        /// <param name="Deps">Controlador: Necesario para el computo.</param>
        /// <param name="beta1">Observador: Señal parcial de salida del observador.</param>
        /// <param name="beta2">Observador: Señal parcial de salida del observador.</param>
        /// <param name="g_error1">Observador: Señal de entrada/parcial de salida del observador.</param>
        /// <param name="g_error2">Observador: Señal de entrada/parcial de salida del observador.</param>
        /// <param name="g_error1_former">Observador: Necesario para el computo de la integracion.</param>
        /// <param name="g_error2_former">Observador: Necesario para el computo de la integracion.</param>
        /// <param name="Dg_error1">Observador: Señal de pre-salida del observador.</param>
        /// <param name="Dg_error2">Observador: Señal de pre-salida del observador.</param>
        /// <param name="g1_Observer">Observador: Señal de salida del observador(Resistencia estimada de R1).</param>
        /// <param name="g2_Observer">Observador: Señal de salida del observador(Resistencia estimada de R2).</param>
        [DllImport(@"..\..\..\Debug\SIMDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SteppedSimulation(out float eval_time, float timeStep, string desiredSimulation, //Simulacion
        out float v_s, out float v_t, out float v_d, out float i_s, //Rectificador
        out float v_t_former, out float v_d_former, out float i_s_former,
        out float Dv_t, out float Dv_d, out float Di_s,
        out float eta, out float eps, out float u1, out float u2, //Controlador
        out float eta_former, out float eps_former, out float i_s_des_out, out float v_t_error_out, out float v_d_error_out,
        out float Deta, out float Deps,
        out float beta1, out float beta2, out float g_error1, out float g_error2, //Observador
        out float g_error1_former, out float g_error2_former,
        out float Dg_error1, out float Dg_error2,
        out float g1_Observer, out float g2_Observer);

        #endregion C DLL Methods
        
        #region Methods
        /// <summary>
        /// Devuelve los tipos de señales de salida del RCO.
        /// </summary>
        /// <returns></returns>
        static public List<string> GetOutSignals()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add("v_t");
            aux_list.Add("v_d");
            aux_list.Add("i_s");

            return aux_list;
        }

        /// <summary>
        /// Devuele los tipos de sañales internas del RCO.
        /// </summary>
        /// <returns></returns>
        static public List<string> GetControlSignals()
        {
            List<string> aux_list = new List<string>();

            aux_list.Add("eta");
            aux_list.Add("eps");
            aux_list.Add("u1");
            aux_list.Add("u2");
            aux_list.Add("beta1");
            aux_list.Add("beta2");
            aux_list.Add("g_error1");
            aux_list.Add("g_error2");

            return aux_list;
        }
        #endregion
 */
#endregion
