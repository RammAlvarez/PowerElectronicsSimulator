#ifndef SIMDLL_HEADER
#define SIMDLL_HEADER

#ifdef SIMDLL_EXPORTS
#define SIMDLL_API __declspec(dllexport)
#else
#define SIMDLL_API __declspec(dllimport)
#endif

__declspec(dllexport) void SteppedSimulation(float* eval_time, float* timeStep,  //Simulacion
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
	float* g1_Observer, float* g2_Observer, float* gamma1, float* gamma2);

#endif