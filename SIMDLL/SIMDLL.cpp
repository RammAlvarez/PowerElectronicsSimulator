#include <math.h>
//#include <../../../../../Users/Ramm/Documents/Visual Studio 2013/Projects/Tesis/ROACH-0100-Template/SIMDLL/Header.h>
#define EXPORT __declspec(dllexport)

//[TOTEST] Lo de abajo se esta probando
//#define CALL __stdcall

extern "C"
{
	/* NOTAS:
	* 1. Se deben modificar las propiedades de este proyecto para que sea compatible con
	*	el CLR, esto se hace haciendo click derecho en el proyecto del DLL en 
	*		Properties/Configuration Properties/General/Common Language Runtime Support
	*	Y escoges la opcion "Common Language Support(/clr)"
	* 2. Para que el programa en C# lo pueda correr debes cambiar en
	*		Build/Configuration Manager/
	*	y escoger(o crear) la opcion "x86" en la columna "Plataform" esto se debe hacer
	*	debido que el DLL en C es 32bits y la PC(al menos la mayoria) es de 64 bits(x64).
	* 3. La direccion del DLL tiene que ser absoluta y no relativo debido a que esto ultimo
	*	no esta permitido.
	*/
	
	
	//**********************************************************
	//			General use Mathematical functions
	//**********************************************************
	float Sign(float value)
	{
		if (value > 0.0f)
			return 1.0f;
		else
			return -1.0f;
	}

	float Integrate(float Du, float* u_former, float time_step)
	{
		float u = Du*time_step + *u_former;
		*u_former = u;

		return u;
	}

	
	//**********************************************************
	//					Models Parameters
	//**********************************************************
	//Constantes del controlador
	float k = 100000.0f;
	float kp1 = 0.01f;
	float ki1 = 10.0f;
	float ki2 = 500.0f;
	float kp2 = 250.0f;

	//Variables del circuito
	float Vt_des = 500.0f;
	float Vd_des = 0.0f;
	float Vs_Amplitude = 180.0f;
	float Vs_frequency = 377.0f;
	float timeStep = 0.1666e-4f;
	float R1 = 100.0f;
	float R2 = 100.0f;
	float C = 470e-6f;
	float L = 10e-3f;

	float g2 = (1 / (2 * R1)) - (1 / (2 * R2));
	float g1 = (1 / (2 * R1)) + (1 / (2 * R2));

	//Constantes del observador
	float gamma1 = 100e-5f;
	float gamma2 = 100e-5f;

	
	//**********************************************************
	//					Models functions
	//**********************************************************
	void Rectifier(float v_s, float v_t, float v_d, float i_s, 
		float u1, float u2, float g1, float g2, float C, float L,
		float* Dv_t, float* Dv_d, float* Di_s)
	{
		*Dv_t = (-v_t*g1 - v_d*g2 + i_s*Sign(i_s)*(2.0f - u1 - u2)) / C;
		*Dv_d = (-v_t*g2 - v_d*g1 + i_s*(u2 - u1)) / C;
		*Di_s = (-0.5f*Sign(i_s)*(v_t)*(2.0f - u1 - u2) - 0.5f*(v_d)*(u2 - u1) + v_s) / L;
	}

	void Controller(float v_s, float v_sp, float v_t, float v_d, float i_s, 
		float v_t_des, float v_d_des, float g1, float g2, float eta, float eps,		
		float* u1, float* u2, float* i_s_des_out, float* v_t_error_out, float* v_d_error_out, 
		float* Deta, float* Deps, float k, float kp1, float kp2, float ki1, float ki2)
	{		
		//Calculos previos
		float v_t_error = v_t - v_t_des;
		float i_s_des = -((v_s*v_t) / (2 * v_sp*v_sp))*(-g1*v_t - g2*v_d + kp1*(v_t_error)+ki1*eta);
		float i_s_error = i_s - i_s_des;
		float v_d_error = v_d - v_d_des;

		//Salidas del sistema
		float u1_v = -(1.0f / v_t)*Sign(i_s)*(v_s + k*i_s_error)
			+ (i_s_des / (2.0f + 2.0f * i_s_des*i_s_des))*(-g2*v_t - g1*v_d + kp2*v_d_error + ki2*eps) + 1.0f;
		
		if (u1_v > 1.0f)
			*u1 = 1.0f;
		else if (u1_v < 0.0f)
			*u1 = 0.0f;
		else
			*u1 = u1_v;

		float u2_v = -(1.0f / v_t)*Sign(i_s)*(v_s + k*i_s_error)
			- (i_s_des / (2.0f + 2.0f * i_s_des*i_s_des))*(-g2*v_t - g1*v_d + kp2*v_d_error + ki2*eps) + 1.0f;
		
		if (u2_v > 1.0f)
			*u2 = 1.0f;
		else if (u2_v < 0.0f)
			*u2 = 0.0f;
		else
			*u2 = u2_v;

		*Deta = v_t_error;
		*Deps = v_d_error;

		//Salidas para impresion y debug
		*i_s_des_out = i_s_des;
		*v_t_error_out = v_t_error;
		*v_d_error_out = v_d_error;

	}

	void Observer(float v_t, float v_d, float i_s, 
		float u1, float u2, float g_error1, float g_error2,		
		float* Dg_error1, float* Dg_error2, float* beta1, float* beta2,
		float gamma1, float gamma2)
	{		
		float gamma[2][2] = { { gamma1, 0 }, { 0, gamma2 } };
		float Gv[2][2] = { { v_t, v_d }, { v_d, v_t } };
		float F_is[2][1] = { { i_s*Sign(i_s)*(2 - u1 - u2) }, { i_s*(u2 - u1) } };
		float g_error[2][1] = { { g_error1 }, { g_error2 } };

		//Calculos intermedios------------------------------------------------------------------------------------------
		float Dbeta[2][2] = { { 0, 0 }, { 0, 0 } };
		for (int j = 0; j < 2; j++)
		{
			for (int i = 0; i < 2; i++)
			{
				for (int inner = 0; inner < 2; inner++)
				{
					Dbeta[j][i] += gamma[j][inner] * Gv[inner][i];
				}
				Dbeta[j][i] *= C;
			}
		}

		float beta_aux[2][1] = { { (v_t*v_t + v_d*v_d) / 2 }, { v_t*v_d } };
		float beta[2][1] = { { 0 }, { 0 } };

		for (int j = 0; j < 2; j++)
		{
			for (int i = 0; i < 1; i++)
			{
				for (int inner = 0; inner < 2; inner++)
				{
					beta[j][i] += gamma[j][inner] * beta_aux[inner][i];
				}
				beta[j][i] *= C;
			}
		}

		//Para la ecuacion general governante------------------------------------------------------------------------------------
		float Dg_error[2][1] = { { 0 }, { 0 } };
		float Dbeta_aux[2][2] = { { 0, 0 }, { 0, 0 } };

		for (int j = 0; j < 2; j++)
			for (int i = 0; i < 2; i++)
				Dbeta_aux[j][i] = Dbeta[j][i] / C;

		float g_error_minus_beta[2][1] = { { g_error[0][0] - beta[0][0] }, { g_error[1][0] - beta[1][0] } };

		float Gv_dot_g_error_minus_beta[2][1] = { { 0 }, { 0 } };

		for (int j = 0; j < 2; j++)
			for (int i = 0; i < 1; i++)
				for (int inner = 0; inner < 2; inner++)
					Gv_dot_g_error_minus_beta[j][i] += Gv[j][inner] * g_error_minus_beta[inner][i];

		float F_is_minus_Gv_dot_g_error_minus_beta[2][1] 
			= { { F_is[0][0] - Gv_dot_g_error_minus_beta[0][0] }, 
				{ F_is[1][0] - Gv_dot_g_error_minus_beta[1][0] } };
		
		for (int j = 0; j < 2; j++)
			for (int i = 0; i < 1; i++)
				for (int inner = 0; inner < 2; inner++)
					Dg_error[j][i] += Dbeta_aux[j][inner] * F_is_minus_Gv_dot_g_error_minus_beta[inner][i];

		//Salidas-----------------------------------------------------------------------------------------
		*Dg_error1 = Dg_error[0][0];
		*Dg_error2 = Dg_error[1][0];
		*beta1 = beta[0][0];
		*beta2 = beta[1][0];
	}

	//**********************************************************
	//					System Simulation
	//**********************************************************	
	EXPORT void SteppedSimulation(float* eval_time, float* timeStep,  //Simulacion
		float* vt_des, float* vd_des, float* v_s, float* vs_amplitude, float* vs_frequency,
		float* r1, float* r2, float* C, float* L,
		float* v_t, float* v_d, float* i_s, //Rectificador
		float* v_t_former, float* v_d_former, float* i_s_former,
		float* Dv_t, float* Dv_d, float* Di_s,
		float* eta, float* eps, float* u1, float* u2, //Controlador
		float* eta_former, float* eps_former, float* i_s_des_out, float* v_t_error_out, float* v_d_error_out,
		float* Deta, float* Deps, float* k, float* kp1, float* kp2, float* ki1, float* ki2,
		float* beta1, float* beta2, float* g_error1, float* g_error2, //Observador
		float* g_error1_former, float* g_error2_former, 
		float* Dg_error1, float* Dg_error2,
		float* g1_Observer, float* g2_Observer, float* gamma1, float* gamma2)
	{
				
		*v_s = (*vs_amplitude)*sinf((*vs_frequency)* (*eval_time));//Default Frequency = 120*pi ~= 377; Default Amplitude = 180

		//Se realiza la simulacion deseada		
		Rectifier(*v_s, *v_t, *v_d, *i_s, *u1, *u2, (1 / (2 * (*r1))) + (1 / (2 * (*r2))),
			(1 / (2 * (*r1))) - (1 / (2 * (*r2))), *C, *L, Dv_t, Dv_d, Di_s);
		Controller(*v_s, *vs_amplitude, *v_t, *v_d, *i_s, *vt_des, *vd_des, *g1_Observer, *g2_Observer, *eta,
			*eps, u1, u2, i_s_des_out, v_t_error_out, v_d_error_out, Deta, Deps,
			*k, *kp1, *kp2, *ki1, *ki2);//G's variables
		Observer(*v_t, *v_d, *i_s, *u1, *u2, *g_error1, *g_error2, Dg_error1, Dg_error2, beta1, beta2,
			*gamma1, *gamma2);

		//Integrando las variables---------------------------------------------------------------
		//Del rectificador
		*v_t = Integrate(*Dv_t, v_t_former, *timeStep);
		*v_d = Integrate(*Dv_d, v_d_former, *timeStep);
		*i_s = Integrate(*Di_s, i_s_former, *timeStep);

		//Del controlador
		*eta = Integrate(*Deta, eta_former, *timeStep);
		*eps = Integrate(*Deps, eps_former, *timeStep);

		//Del observador
		*g_error1 = Integrate(*Dg_error1, g_error1_former, *timeStep);
		*g_error2 = Integrate(*Dg_error2, g_error2_former, *timeStep);

		//G's calculadas del observador
		*g1_Observer = *g_error1 - *beta1;
		*g2_Observer = *g_error2 - *beta2;

		*eval_time += *timeStep;
	}

	EXPORT void ResetVariables(float* eval_time, float* timeStep,  //Simulacion
		float* vt_des, float* vd_des, float* v_s, float* vs_amplitude, float* vs_frequency,
		float* r1, float* r2, float* C, float* L,
		float* v_t, float* v_d, float* i_s, //Rectificador
		float* v_t_former, float* v_d_former, float* i_s_former,
		float* Dv_t, float* Dv_d, float* Di_s,
		float* eta, float* eps, float* u1, float* u2, //Controlador
		float* eta_former, float* eps_former, float* i_s_des_out, float* v_t_error_out, float* v_d_error_out,
		float* Deta, float* Deps, float* k, float* kp1, float* kp2, float* ki1, float* ki2,
		float* beta1, float* beta2, float* g_error1, float* g_error2, //Observador
		float* g_error1_former, float* g_error2_former,
		float* Dg_error1, float* Dg_error2,
		float* g1_Observer, float* g2_Observer, float* gamma1, float* gamma2)
	{
		*eval_time = 0.0f;
		*timeStep = 0.1666e-4f;

		*vt_des = 70.0f;
		*vd_des = 0.0f;
		*v_s = 0.0f;
		*vs_amplitude = 34.0f;
		*vs_frequency = 377.0f;

		*r1 = 100.0f;
		*r2 = 100.0f;
		*C = 470e-6f;
		*L = 10e-3f;

		*v_t = 10.0f;
		*v_d = 0.0f;
		*i_s = 1.0f;

		*v_t_former = 0.0f;
		*v_d_former = 0.0f;
		*i_s_former = 0.0f;

		*Dv_t = 0.0f;
		*Dv_d = 0.0f;
		*Di_s = 0.0f;

		*eta = 0.0f;
		*eps = 0.0f;
		*u1 = 0.0f;
		*u2 = 0.0f;

		*eta_former = 0.0f;
		*eps_former = 0.0f;
		*i_s_des_out = 0.0f;
		*v_t_error_out = 0.0f;
		*v_d_error_out = 0.0f;

		*Deta = 0.0f;
		*Deps = 0.0f;

		*k = 100000.0f;
		*kp1 = 0.01f;
		*kp2 = 250.0f;
		*ki1 = 10.0f;
		*ki2 = 500.0f;

		*beta1 = 0.0f;
		*beta2 = 0.0f;
		*g_error1 = 0.0f;
		*g_error2 = 0.0f;

		*g_error1_former = 0.0f;
		*g_error2_former = 0.0f;

		*Dg_error1 = 0.0f;
		*Dg_error2 = 0.0f;

		*g1_Observer = 0.02f;
		*g2_Observer = 0.02f;
		*gamma1 = 100e-5f;
		*gamma2 = 100e-5f;
	}

	
}