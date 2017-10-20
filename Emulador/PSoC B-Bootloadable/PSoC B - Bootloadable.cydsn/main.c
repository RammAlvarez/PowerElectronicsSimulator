/* ========================================
 *
 * Copyright Ramiro J. Alvarez Ramirez, 2015
 * All Rights Reserved
 * UNPUBLISHED, LICENSED SOFTWARE.
 *
 * CONFIDENTIAL AND PROPRIETARY INFORMATION
 * WHICH IS THE PROPERTY OF Ramiro J. Alvarez Ramirez.
 *
 * EMAIL: ramm_alvarez@outlook.com
 *
 * ========================================
*/

/*
    Anotaciones:
        [DEPRECATED] = Sera eliminada en las nuevas versiones.
        [NOTE] = Señala algun concepto especifico, razonamiento o situacion.
        [TEST] = Seccion de codigo que esta sujeto a pruebas.
        [TODO] = Seccion que debe ser revisada, corregida o agregada.
        [TOTEST] = Sentencia especifica que sera aplicada despues de alguna prueba.        
*/
#include <project.h>
#include "math.h" //Para usarla "Go to Project -> Build Settings -> Linker -> General -> Additional Libraries. Type 'm' in the Additional Libraries field."
#include "stdlib.h"

//***********************************************************************************************
//			                            Constants & Definitions
//***********************************************************************************************
#define FALSE 0u
#define TRUE !FALSE
#define NEGATIVE 0u
#define POSITIVE !NEGATIVE

#define I2C_PACKET_SIZE 8u

#define CMD_VT_DES 0 
#define CMD_VD_DES 1
#define CMD_R1 2
#define CMD_R2 3
#define CMD_C 4
#define CMD_L 5
#define CMD_K 6 
#define CMD_KP1 7
#define CMD_KP2 8
#define CMD_KI1 9
#define CMD_KI2 10
#define CMD_G1 11
#define CMD_G2 12
#define CMD_GAMMA1 13
#define CMD_GAMMA2 14

#define CMD_EMULATOR_MODE 15
#define CMD_CONTROLLER_MODE 16
#define CMD_REPEAT_REQUEST 17

#define SEE_VT_DES 18
#define SEE_VD_DES 19
#define SEE_R1 20
#define SEE_R2 21
#define SEE_C 22
#define SEE_L 23
#define SEE_K 24
#define SEE_KP1 25
#define SEE_KP2 26
#define SEE_KI1 27
#define SEE_KI2 28
#define SEE_G1 29
#define SEE_G2 30
#define SEE_GAMMA1 31
#define SEE_GAMMA2 32
#define SEE_V_T 33
#define SEE_V_D 34
#define SEE_I_S 35
#define SEE_U1 36
#define SEE_U2 37
#define SEE_VT_ERROR 38
#define SEE_VD_ERROR 39
#define SEE_IS_ERROR 40


//***********************************************************************************************
//			                            Unions & Structures
//***********************************************************************************************
typedef union StreamPackage
{
    uint16 value;
    uint8 bytes[2];//[0]-Menos significativo, [1]- Mas significativo
}StreamPackage;

typedef union ReceivedPackage{
	uint8 params[8];
	struct Inners{
        uint8 start;
		uint8 command;
		uint8 data_sign;
		uint8 data[2];//[0]-Menos significativo, [1]- Mas significativo
		uint8 exp_sign;
		uint8 exp;
        uint8 checksum;
	}Inners;
}ReceivedPackage;

//***********************************************************************************************
//			                                    Flags
//***********************************************************************************************
//uint8 volatile i2c_Read_DataReady_Flag = FALSE;
//uint8 volatile i2c_Write_DataReady_Flag = FALSE;

uint8 volatile uart_Read_DataReady_Flag = FALSE;
uint8 uart_PackageComplete_Flag = FALSE;

uint32 volatile adc_WindowFlag = FALSE;
uint8 volatile adc_DataReady = FALSE;
uint8 volatile IsEmulatorModeEnable = FALSE;//[NOTE] Todos los modulos activos
uint8 volatile IsControllerModeEnable = FALSE;//[NOTE] El modulo DAC se encuentra deshabilitado
uint8 volatile IsStream = FALSE;
//***********************************************************************************************
//			                              Global Variables
//***********************************************************************************************
    //General
uint8 generalCounter = 0u;
StreamPackage stream_package;
ReceivedPackage received_package;
uint8 variableToStream = SEE_V_T;//[NOTE] Se envia "v_t" por defecto

    //UART
uint32 volatile uart_DataReceived[8];
uint8 volatile uart_PackageIndex = 0;

    //I2C
uint8 i2c_writebuffer[I2C_PACKET_SIZE];
uint8 i2c_readbuffer[I2C_PACKET_SIZE];
    
    //Timer
uint32 current_time = 0u;
uint32 former_time = 0u;
const uint32 period = 1e6;


//***********************************************************************************************
//			                             Circuit Variables
//***********************************************************************************************
//Solo la usamos para el ADC
//[TODO][DEPRECATED]: vs_p y timeStep deben ser revisadas y cambiadas
#define vs_p 30.0f
#define timeStep 0.1666e-4f
float volatile vt_des = 500.0f;
float volatile vd_des = 0.0f;

float R1 = 100.0f;
float R2 = 100.0f;
float C = 470e-6f;
float L = 10e-3f;

    //Constantes del controlador
float k = 100000.0f;
float kp1 = 0.01f;
float ki1 = 10.0f;
float ki2 = 500.0f;
float kp2 = 250.0f;

    //Variables del observador
float g1;
float g2;
float gamma1 = 100e-5f;
float gamma2 = 100e-5f;

//***********************************************************************************************
//			                            Model Variables
//***********************************************************************************************

//[TODO][DEPRECATED]: Estas variables deben ser cambiadas
float i = 0.0f;
//char time[100];//Tienen que ser arreglos(Aunque despues los uses como punteros)
float time = 0.0f;

    //Fuente de voltaje
float v_s = 0.0f;//, Iv_s = 0.0f;

//Señal del rectificador-----------------------------------------------------------------------
float v_t = 0.0f, v_d = 0.0f, i_s = 1.0f;
    //Variable de integracion del rectificador(Retroalimentacion)
float v_t_former = 0.0f, v_d_former = 0.0f, i_s_former = 0.0f;
    //Salida del circuito
float Dv_t = 0.0f, Dv_d = 0.0f, Di_s = 0.0f;

//Señal del controlador-----------------------------------------------------------------------
float eta = 0.0f, eps = 0.0f, u1 = 0.0f, u2 = 0.0f;
    //Variable de intregracion del controlador(Retroalimentacion)
float eta_former = 0.0f, eps_former = 0.0f;
    //Salida del controlador
float i_s_des_out = 0.0f, v_t_error_out = 0.0f, v_d_error_out = 0.0f, Deta = 0.0f, Deps = 0.0f;

//Señal del observador-----------------------------------------------------------------------
float beta1 = 0.0f, beta2 = 0.0, g_error1 = 0.0f, g_error2 = 0.0f;
    //Variable de intregracion del observador(Retroalimentacion)
float g_error1_former = 0.0f, g_error2_former;
    //Salida del observador
float Dg_error1 = 0.0f, Dg_error2 = 0.0f;
    //Auxiliares del observador
float g1_Observer = 0.02f, g2_Observer = 0.02f;

//***********************************************************************************************
//			                            Functions Prototypes
//***********************************************************************************************
    //General use
float Sign(float value);
float Integrate(float Du, float* u_former, float time_step);
uint8 CalculateChecksum(uint8* params);
uint8 VerifyChecksum(uint8* params);

    //Initializing
void Initialize_Variables(void);
void Initailize_Hardware(void);
void Initializing(void);

    //Inputs
void ReadPackage(uint8* params, uint8 packet_size);
float ConvertExpToFloat(uint8 exp);
float ConvertFrameDataToFloat(uint8* params);
void DecodifyPackage(uint8* params, uint8* flag);
void LCD_PrintCommands(void);
void Inputs(void);

    //Data Acquisition
float ADC_ReadVsource_Volts(void);
void Timer_GetTime(uint32* current, uint32* former, uint32 period);
void Data_Acquisition(void);

    //Mathematical Treatment
void Rectifier(float v_s, float v_t, float v_d, float i_s, float u1, float u2, float g1, float g2, 
    float C, float L, float* Dv_t, float* Dv_d, float* Di_s);
void Controller(float v_s, float v_sp, float v_t, float v_d, float i_s, float v_t_des, 
    float v_d_des, float g1, float g2, float eta, float eps, float* u1, float* u2, 
    float* i_s_des_out, float* v_t_error_out, float* v_d_error_out, float* Deta, float* Deps);
void Observer(float v_t, float v_d, float i_s, float u1, float u2, float g_error1, float g_error2,
	float* Dg_error1, float* Dg_error2, float* beta1, float* beta2);
void Mathematical_Model(char* option);//[DEPRECATED]
void Integrating_Variables_and_Gs(void);
void Mathematical_Treatment(float i);

    //Outputs
void Switches_Write(float s1, float s2);
void DAC_Write(void);
void WritePackage(uint8* params, uint8 IsStream);
void SelectVariableToStream(uint8 value);
void SendStream(float value);
void Outputs(void);
//***********************************************************************************************
//			                                ISR Prototypes
//***********************************************************************************************
CY_ISR_PROTO(ADC_SAR_ISR_Handler);

//***********************************************************************************************
//			                                    MAIN
//***********************************************************************************************
int main()
{
    Initializing();
    
    for(;;)
    {
        //Si el boton en la placa es presionado, entra en modo BootLoader
        if(!SW1_Bootloader_Read()) Bootloadable_Load();
        
        ReadPackage(received_package.params, 8);
        DecodifyPackage(received_package.params, &uart_PackageComplete_Flag);
        Mathematical_Treatment(i);
        SelectVariableToStream(variableToStream);
        
        if(i++ >= 120.0f) i = 0.0f;//[DEPRECATED]
    }
}

//***********************************************************************************************
//			                                  General use
//***********************************************************************************************
float Sign(float value)
{
	//[NOTE]:  Se evita poner el cero para no tener indeterminaciones.	
    if (value > 0.0f)
		return 1.0f;
	else
		return -1.0f;
}

float Integrate(float Du, float* u_former, float time_step)
{	
	float u = (*u_former)+ Du*time_step;
	*u_former = u;
	return u;
}

uint8 CalculateChecksum(uint8* params)
{
	uint8 aux = 0;	
	for (generalCounter = 1; generalCounter < 7; generalCounter++)
		aux += params[generalCounter];

	return 255 - aux;
}

uint8 VerifyChecksum(uint8* params)
{
	uint8 aux = 0;
	for (generalCounter = 1; generalCounter < 8; generalCounter++)
		aux += params[generalCounter];

	if (aux == 255)
		return TRUE;
	else
		return FALSE;
}

//***********************************************************************************************
//			                                  Initializing
//***********************************************************************************************
void Initailize_Hardware(void)
{
    LCD_Start();    
    
//    I2C_I2CSlaveInitWriteBuf(i2c_writebuffer, I2C_PACKET_SIZE);
//    I2C_I2CSlaveInitReadBuf(i2c_readbuffer, I2C_PACKET_SIZE);
//    I2C_Start();
    
    ISR_UART_RX_FIFO_FULL_Start();
    UART_Start();
//    
//    T_Clock_Start();
//    Timer_Start();
//    
//    Opamp_1_Start();
//    Opamp_2_Start();    
//    ADC_SAR_Start();
//    ADC_SAR_IRQ_StartEx(ADC_SAR_ISR_Handler);
//    ADC_SAR_IRQ_Enable();
    
    CyGlobalIntEnable; 
    
//    ADC_SAR_StartConvert();
    
    LCD_Position(0u,0u);
    LCD_PrintString("Ready");
}

void Initialize_Variables(void)
{
    g1 = (1 / (2 * R1)) + (1 / (2 * R2));
    g2 = (1 / (2 * R1)) - (1 / (2 * R2));    
}

void Initializing(void)
{
    Initailize_Hardware();
    Initialize_Variables();
}

//***********************************************************************************************
//			                                    Inputs
//***********************************************************************************************
void ReadPackage(uint8* params, uint8 packet_size)
{
    if(uart_Read_DataReady_Flag == TRUE)
    {
        uart_Read_DataReady_Flag = FALSE;
        
        //Leemos el dato recibido
        for(generalCounter = 0; generalCounter < packet_size; generalCounter++)
            params[generalCounter] = uart_DataReceived[generalCounter];
        
        uart_PackageComplete_Flag = TRUE;
               
        LCD_ClearDisplay();
        LCD_Position(0,0);
        LCD_PrintString("P Complete");
    }
}

float ConvertExpToFloat(uint8 exp)
{
    float aux = 1.0f;
    for(generalCounter = 0; generalCounter < exp; generalCounter++)
        aux *= 10.0f;
    
    return aux;
}

float ConvertFrameDataToFloat(uint8* params)
{
    float data = 0.0f;

    //Cifra significativa
	if (params[2] == NEGATIVE)
		data = -1.0f*(float)(params[4]*256 + params[3]);
	else
		data = (float)(params[4]*256 + params[3]);

    //Exponente
	if (params[5] == NEGATIVE)
		data = data / ConvertExpToFloat(params[6]);
	else
		data = data*ConvertExpToFloat(params[6]);

	return data;
}

void DecodifyPackage(uint8* params, uint8* flag)
{
    if(*flag == TRUE && params[0] == 126)
    {
        LCD_ClearDisplay();
        LCD_Position(0,0);
        LCD_PrintString("Changed Stream");
        *flag = FALSE;
        switch(params[1])
        {
            //---CHANGE------------------------------------------------------------------------------------
            case CMD_VT_DES:
                vt_des = ConvertFrameDataToFloat(params);
                break;
            case CMD_VD_DES:
                vd_des = ConvertFrameDataToFloat(params);
                break;
            case CMD_R1:
                R1 = ConvertFrameDataToFloat(params);
                break;
            case CMD_R2:
                R2 = ConvertFrameDataToFloat(params);
                break;
            case CMD_C:
                C = ConvertFrameDataToFloat(params);
                break;
            case CMD_L:
                L = ConvertFrameDataToFloat(params);
                break;
            case CMD_K:
                k = ConvertFrameDataToFloat(params);
                break;
            case CMD_KP1:
                kp1 = ConvertFrameDataToFloat(params);
                break;
            case CMD_KP2:
                kp2 = ConvertFrameDataToFloat(params);
                break;
            case CMD_KI1:
                ki1 = ConvertFrameDataToFloat(params);
                break;
            case CMD_KI2:
                ki2 = ConvertFrameDataToFloat(params);
                break;
            case CMD_G1:
                g1 = ConvertFrameDataToFloat(params);
                break;
            case CMD_G2:
                g2 = ConvertFrameDataToFloat(params);
                break;
            case CMD_GAMMA1:
                gamma1 = ConvertFrameDataToFloat(params);
                break;
            case CMD_GAMMA2:
                gamma2 = ConvertFrameDataToFloat(params);
                break;
            //---MODE--------------------------------------------------------------------------------------
            case CMD_EMULATOR_MODE:
                IsEmulatorModeEnable = TRUE;
                IsControllerModeEnable = FALSE;
                break;
            case CMD_CONTROLLER_MODE:
                IsEmulatorModeEnable = FALSE;
                IsControllerModeEnable = TRUE;
                break;
            case CMD_REPEAT_REQUEST:
                //[TODO]: En caso de haber un error en el checksum, pedir el paquete de nuevo.
                break;
            //---SEE---------------------------------------------------------------------------------------
            case SEE_VT_DES:
                variableToStream = SEE_VT_DES;
                break;
            case SEE_VD_DES:
                variableToStream = SEE_VD_DES;
                break;
            case SEE_R1:
                variableToStream = SEE_R1;
                break;
            case SEE_R2:
                variableToStream = SEE_R2;
                break;
            case SEE_C:
                variableToStream = SEE_C;
                break;
            case SEE_L:
                variableToStream = SEE_L;
                break;
            case SEE_K:
                variableToStream = SEE_K;
                break;
            case SEE_KP1:
                variableToStream = SEE_KP1;
                break;
            case SEE_KP2:
                variableToStream = SEE_KP2;
                break;
            case SEE_KI1:
                variableToStream = SEE_KI1;
                break;
            case SEE_KI2:
                variableToStream = SEE_KI2;
                break;
            case SEE_G1:
                variableToStream = SEE_G1;
                break;
            case SEE_G2:
                variableToStream = SEE_G2;
                break;
            case SEE_GAMMA1:
                variableToStream = SEE_GAMMA1;
                break;
            case SEE_GAMMA2:
                variableToStream = SEE_GAMMA2;
                break;       
            case SEE_V_T:
                variableToStream = SEE_V_T;
                break;
            case SEE_V_D:
                variableToStream = SEE_V_D;
                break;
            case SEE_I_S:
                variableToStream = SEE_I_S;
                break;
            case SEE_U1:
                variableToStream = SEE_U1;
                break;
            case SEE_U2:
                variableToStream = SEE_U2;
                break;
            case SEE_VT_ERROR:
                variableToStream = SEE_VT_ERROR;
                break;
            case SEE_VD_ERROR:
                variableToStream = SEE_VD_ERROR;
                break;
            case SEE_IS_ERROR:
                variableToStream = SEE_IS_ERROR;
                break;        
            default:
                break;
        }
    }
    
}

void LCD_PrintCommands(void)
{
    LCD_ClearDisplay();
    LCD_Position(0u,0u);
    LCD_PrintString("I2C: Comando recibido!");    
    LCD_Position(1u,0u);
    LCD_PrintInt8(received_package.Inners.command);
}

void Inputs(void)
{
    ReadPackage(received_package.params,I2C_PACKET_SIZE);
    //[TODO]: VerifyChecksum(); //Verificar la integridad del paquete y tomar acciones en consecuencia
//    if(i2c_Read_DataReady_Flag == TRUE)
//    {
//        DecodifyPackage(received_package.params);
//        LCD_PrintCommands();    
//        i2c_Read_DataReady_Flag = FALSE;
//    }    
}

//***********************************************************************************************
//			                                Data Adquisition
//***********************************************************************************************
float ADC_ReadVsource_Volts(void)
{    
    int16 aux = ADC_SAR_CountsTo_mVolts(0u, ADC_SAR_GetResult16(0u));
    
    return ((float)aux)/1000.0f;
}

void Timer_GetTime(uint32* current, uint32* former, uint32 period)
{
    *former = *current;
    *current = period - Timer_ReadCounter();
}

void Data_Acquisition(void)
{
    while(adc_DataReady == FALSE);
    
    if(adc_DataReady == TRUE)
    {
        v_s = ADC_ReadVsource_Volts();
        adc_DataReady = FALSE;
    }
    
    Timer_GetTime(&current_time, &former_time, period);
}

//***********************************************************************************************
//			                            Mathematical Treatment
//***********************************************************************************************
void Rectifier(float v_s, float v_t, float v_d, float i_s, float u1, float u2, float g1, float g2, 
    float C, float L, float* Dv_t, float* Dv_d, float* Di_s)
{
	*Dv_t = (-v_t*g1 - v_d*g2 + i_s*Sign(i_s)*(2.0f - u1 - u2)) / C;
	*Dv_d = (-v_t*g2 - v_d*g1 + i_s*(u2 - u1)) / C;
	*Di_s = (-0.5f*Sign(i_s)*(v_t)*(2.0f - u1 - u2) - 0.5f*(v_d)*(u2 - u1) + v_s) / L;	
}

void Controller(float v_s, float v_sp, float v_t, float v_d, float i_s, float v_t_des, 
    float v_d_des, float g1, float g2, float eta, float eps, float* u1, float* u2, 
    float* i_s_des_out, float* v_t_error_out, float* v_d_error_out, float* Deta, float* Deps)
{	
	//Calculos previos
	float v_t_error = v_t - v_t_des;
	float i_s_des = -((v_s*v_t) / (2*v_sp*v_sp))*(-g1*v_t - g2*v_d + kp1*(v_t_error) + ki1*eta);
	float i_s_error = i_s - i_s_des;
	float v_d_error = v_d - v_d_des;

	//Salidas del sistema
	float u1_v = -(1.0f / v_t)*Sign(i_s)*(v_s + k*i_s_error) 
        + (i_s_des / (2.0f + 2.0f * i_s_des*i_s_des))*(-g2*v_t - g1*v_d + kp2*v_d_error + ki2*eps) 
        + 1.0f;
	
    if (u1_v > 1)
	    *u1 = 1.0f;
	else if (u1_v < 0)
		*u1 = 0.0f;
	else
		*u1 = u1_v;

	float u2_v = -(1.0f / v_t)*Sign(i_s)*(v_s + k*i_s_error) 
        - (i_s_des / (2.0f + 2.0f * i_s_des*i_s_des))*(-g2*v_t - g1*v_d + kp2*v_d_error + ki2*eps) 
        + 1.0f;
	
    if (u2_v > 1)
		*u2 = 1.0f;
	else if (u2_v < 0)
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

void Observer(float v_t, float v_d, float i_s, float u1, float u2, float g_error1, float g_error2,
	float* Dg_error1, float* Dg_error2, float* beta1, float* beta2)
{	
	float gamma[2][2] = { { gamma1, 0 }, { 0, gamma2 } };
	float Gv[2][2] = { { v_t, v_d }, { v_d, v_t } };
	float F_is[2][1] = { { i_s*Sign(i_s)*(2 - u1 - u2) }, { i_s*(u2 - u1) } };
	float g_error[2][1] = { { g_error1 }, { g_error2 } };

	//Calculos intermedios-------------------------------------------------------------------------
	float Dbeta[2][2] = { { 0, 0 }, { 0, 0 } };
    int j = 0, i = 0, inner = 0;
    
	for (j = 0; j < 2; j++)
	{
		for (i = 0; i < 2; i++)
		{
			for (inner = 0; inner < 2; inner++)
			{
				Dbeta[j][i] += gamma[j][inner] * Gv[inner][i];
			}
			Dbeta[j][i] *= C;
		}
	}

	float beta_aux[2][1] = { { (v_t*v_t + v_d*v_d) / 2 }, { v_t*v_d } };
	float beta[2][1] = { { 0 }, { 0 } };
    
	for (j = 0; j < 2; j++)
	{
		for (i = 0; i < 1; i++)
		{
			for (inner = 0; inner < 2; inner++)
			{
				beta[j][i] += gamma[j][inner] * beta_aux[inner][i];
			}
			beta[j][i] *= C;
		}
	}

	//Para la ecuacion general governante---------------------------------------------------------
	float Dg_error[2][1] = { { 0 }, { 0 } };
	float Dbeta_aux[2][2] = { { 0, 0 }, { 0, 0 } };
    
	for (j = 0; j < 2; j++)
		for (i = 0; i < 2; i++)
			Dbeta_aux[j][i] = Dbeta[j][i] / C;
    
	float g_error_minus_beta[2][1] = 
        { { g_error[0][0] - beta[0][0] }, { g_error[1][0] - beta[1][0] } };

	float Gv_dot_g_error_minus_beta[2][1] = { { 0 }, { 0 } };
    
	for (j = 0; j < 2; j++)
		for (i = 0; i < 1; i++)
			for (inner = 0; inner < 2; inner++)
				Gv_dot_g_error_minus_beta[j][i] += Gv[j][inner] * g_error_minus_beta[inner][i];

	float F_is_minus_Gv_dot_g_error_minus_beta[2][1] = 
        { { F_is[0][0] - Gv_dot_g_error_minus_beta[0][0] }, 
          { F_is[1][0] - Gv_dot_g_error_minus_beta[1][0] } };
    
	for (j = 0; j < 2; j++)
		for (i = 0; i < 1; i++)
			for (inner = 0; inner < 2; inner++)
				Dg_error[j][i] += 
                    Dbeta_aux[j][inner] * F_is_minus_Gv_dot_g_error_minus_beta[inner][i];

	//Salidas-------------------------------------------------------------------------------------
	*Dg_error1 = Dg_error[0][0];
	*Dg_error2 = Dg_error[1][0];
	*beta1 = beta[0][0];
	*beta2 = beta[1][0];
}

void Integrating_Variables_and_Gs(void)
{
    //[TODO]: Obtener el tiempo a traves del Timer
        //time_count = Timer16_wReadTimer();   
    	//Elapsed_time=ClockPeriod*(PeriodValue- CounterValue);
        //elapsed_time=76923.0f*((float)(65535-time_count));
	
    //Del rectificador 
	v_t = Integrate(Dv_t, &v_t_former, timeStep);
	v_d = Integrate(Dv_d, &v_d_former, timeStep);
	i_s = Integrate(Di_s, &i_s_former, timeStep);
	
	//Del controlador 
	eta = Integrate(Deta, &eta_former, timeStep);
	eps = Integrate(Deps, &eps_former, timeStep);
		
	//Del observador
	g_error1 = Integrate(Dg_error1, &g_error1_former, timeStep);
	g_error2 = Integrate(Dg_error2, &g_error2_former, timeStep);

	//G's calculadas del observador
	g1_Observer = g_error1 - beta1;
	g2_Observer = g_error2 - beta2;
}

void Mathematical_Model(char* option)//[DEPRECATED]
{
    if(strcmp(option, "rectifier") == 0)
    {
        Rectifier(v_s, v_t, v_d, i_s, u1, u2, g1, g2, C, L, &Dv_t, &Dv_d, &Di_s);
    }
    else if(strcmp(option, "controller") == 0)
    {
        Rectifier(v_s, v_t, v_d, i_s, u1, u2, g1, g2, C, L, &Dv_t, &Dv_d, &Di_s);		
        Controller(v_s, 180.0f, v_t, v_d, i_s, vt_des, vd_des, g1, g2, eta, eps, 
            &u1, &u2, &i_s_des_out, &v_t_error_out, &v_d_error_out, &Deta, &Deps);//G's variables    
    }
    else
    {
        Rectifier(v_s, v_t, v_d, i_s, u1, u2, g1, g2, C, L, &Dv_t, &Dv_d, &Di_s);		
    	Controller(v_s, 180.0f, v_t, v_d, i_s, vt_des, vd_des, g1_Observer, g2_Observer, eta, eps, 
                &u1, &u2, &i_s_des_out, &v_t_error_out, &v_d_error_out, &Deta, &Deps);//G's variables
    	Observer(v_t, v_d, i_s, u1, u2, g_error1, g_error2, &Dg_error1, &Dg_error2, &beta1, &beta2);	
    }    
}

void Mathematical_Treatment(float i)
{        
	//[NOTE]: Estamos usando "sin()" en vez de "sinf()" que es como esta en el codigo de VisualStudio	
	v_s = 180.0f*sinf(377.0f*i);//[DEPRECATED]Por el momento utilizaremos la señal generada por codigo
	
    Rectifier(v_s, v_t, v_d, i_s, u1, u2, g1, g2, C, L, &Dv_t, &Dv_d, &Di_s);		
	Controller(v_s, 180.0f, v_t, v_d, i_s, vt_des, vd_des, g1_Observer, g2_Observer, eta, eps, 
            &u1, &u2, &i_s_des_out, &v_t_error_out, &v_d_error_out, &Deta, &Deps);//G's variables
	Observer(v_t, v_d, i_s, u1, u2, g_error1, g_error2, &Dg_error1, &Dg_error2, &beta1, &beta2);	
        
	Integrating_Variables_and_Gs();	
}

//***********************************************************************************************
//			                                    Outputs
//***********************************************************************************************
void Switches_Write(float s1, float s2)
{
    if(s1 > 0.0f)
        Digital_Output_Pin_U1_Write(1u);
    else
        Digital_Output_Pin_U1_Write(0u);
        
    if(s2 >0.0f)
        Digital_Output_Pin_U2_Write(1u);
    else
        Digital_Output_Pin_U2_Write(0u);
}

void DAC_Write(void)
{
    //[TODO]: Aplicar la comunicacion SPI para el modulo DAC externo
}

void SelectVariableToStream(uint8 value)
{
    switch(value)
    {
        //---Commons---------------------------------------------------------------------------------
        case SEE_V_T:
            SendStream(v_t);
            break;
        case SEE_V_D:
            SendStream(v_d);
            break;
        case SEE_I_S:
            SendStream(i_s);
            break;        
        case SEE_VT_ERROR:
            SendStream(v_t_error_out);
            break;
        case SEE_VD_ERROR:
            SendStream(v_d_error_out);
            break;
        case SEE_IS_ERROR:
            SendStream(i_s_des_out);
            break;
        case SEE_U1:
            SendStream(u1);
            break;
        case SEE_U2:
            SendStream(u2);
            break;
        //---Optional---------------------------------------------------------------------------------
        case SEE_VT_DES:
            SendStream(vt_des);
            break;
        case SEE_VD_DES:
            SendStream(vd_des);
            break;
        case SEE_R1:
            SendStream(R1);
            break;
        case SEE_R2:
            SendStream(R2);
            break;
        case SEE_C:
            SendStream(C);
            break;
        case SEE_L:
            SendStream(L);
            break;
        case SEE_K:
            SendStream(k);
            break;
        case SEE_KP1:
            SendStream(kp1);
            break;
        case SEE_KP2:
            SendStream(kp2);
            break;
        case SEE_KI1:
            SendStream(ki1);
            break;
        case SEE_KI2:
            SendStream(ki2);
            break;
        case SEE_G1:
            SendStream(g1);
            break;
        case SEE_G2:
            SendStream(g2);
            break;
        case SEE_GAMMA1:
            SendStream(gamma1);
            break;
        case SEE_GAMMA2:
            SendStream(gamma2);
            break;
        default:
            SendStream(v_t);
            break;
    }
}

void SendStream(float value)
{    
    /*[NOTE]:
        Value = 59.577272 x 1100V = 65535, 
        Esto es para escalar el valor maximo de salida en los dos bytes de transmision 
    
        Protocolo de Comunicacion: 
            Paquete a enviar{0, 255, D1, D0}
    */    
    stream_package.value = (uint16)(59.577272f*value); 
    uint8 aux[] = {0, 255, stream_package.bytes[1],stream_package.bytes[0]};
    
    WritePackage(aux, TRUE);
}

void WritePackage(uint8* params, uint8 IsStream)//[TEST]
{
    for(generalCounter = 0; generalCounter < 4; generalCounter++)
    {
        UART_SpiUartWriteTxData(params[generalCounter]);
    }
}

void Outputs(void)
{
    Switches_Write(u1, u2);
    //DAC_Write();//[TOTEST]: Probar despues de checar el funcionamiento del Stream 
    
    //[TODO]: Falta habilitar la seleccion de señal de salida, Si es STream o NO
    SelectVariableToStream(variableToStream);
}

//***********************************************************************************************
//			                                Interrupttions
//***********************************************************************************************
CY_ISR(ADC_SAR_ISR_Handler)
{
    uint32 intr_status;
    
    /* Read interrupt status registers */
    intr_status = ADC_SAR_SAR_INTR_MASK_REG;
    /* Check for End of Scan interrupt */
    if((intr_status & ADC_SAR_EOS_MASK) != 0u)
    {
        /* Read range interrupt status and raise the flag */
        adc_WindowFlag = ADC_SAR_SAR_RANGE_INTR_MASK_REG;
        /* Clear range detect status */
        ADC_SAR_SAR_RANGE_INTR_REG = adc_WindowFlag;
        adc_DataReady = TRUE;
    }
    
    /* Clear handled interrupt */
    ADC_SAR_SAR_INTR_REG = intr_status;
}
/* [] END OF FILE */

