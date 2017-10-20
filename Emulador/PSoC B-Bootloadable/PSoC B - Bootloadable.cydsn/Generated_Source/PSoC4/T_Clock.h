/*******************************************************************************
* File Name: T_Clock.h
* Version 2.20
*
*  Description:
*   Provides the function and constant definitions for the clock component.
*
*  Note:
*
********************************************************************************
* Copyright 2008-2012, Cypress Semiconductor Corporation.  All rights reserved.
* You may use this file only in accordance with the license, terms, conditions, 
* disclaimers, and limitations in the end user license agreement accompanying 
* the software package with which this file was provided.
*******************************************************************************/

#if !defined(CY_CLOCK_T_Clock_H)
#define CY_CLOCK_T_Clock_H

#include <cytypes.h>
#include <cyfitter.h>


/***************************************
*        Function Prototypes
***************************************/
#if defined CYREG_PERI_DIV_CMD

void T_Clock_StartEx(uint32 alignClkDiv);
#define T_Clock_Start() \
    T_Clock_StartEx(T_Clock__PA_DIV_ID)

#else

void T_Clock_Start(void);

#endif/* CYREG_PERI_DIV_CMD */

void T_Clock_Stop(void);

void T_Clock_SetFractionalDividerRegister(uint16 clkDivider, uint8 clkFractional);

uint16 T_Clock_GetDividerRegister(void);
uint8  T_Clock_GetFractionalDividerRegister(void);

#define T_Clock_Enable()                         T_Clock_Start()
#define T_Clock_Disable()                        T_Clock_Stop()
#define T_Clock_SetDividerRegister(clkDivider, reset)  \
    T_Clock_SetFractionalDividerRegister((clkDivider), 0u)
#define T_Clock_SetDivider(clkDivider)           T_Clock_SetDividerRegister((clkDivider), 1u)
#define T_Clock_SetDividerValue(clkDivider)      T_Clock_SetDividerRegister((clkDivider) - 1u, 1u)


/***************************************
*             Registers
***************************************/
#if defined CYREG_PERI_DIV_CMD

#define T_Clock_DIV_ID     T_Clock__DIV_ID

#define T_Clock_CMD_REG    (*(reg32 *)CYREG_PERI_DIV_CMD)
#define T_Clock_CTRL_REG   (*(reg32 *)T_Clock__CTRL_REGISTER)
#define T_Clock_DIV_REG    (*(reg32 *)T_Clock__DIV_REGISTER)

#define T_Clock_CMD_DIV_SHIFT          (0u)
#define T_Clock_CMD_PA_DIV_SHIFT       (8u)
#define T_Clock_CMD_DISABLE_SHIFT      (30u)
#define T_Clock_CMD_ENABLE_SHIFT       (31u)

#define T_Clock_CMD_DISABLE_MASK       ((uint32)((uint32)1u << T_Clock_CMD_DISABLE_SHIFT))
#define T_Clock_CMD_ENABLE_MASK        ((uint32)((uint32)1u << T_Clock_CMD_ENABLE_SHIFT))

#define T_Clock_DIV_FRAC_MASK  (0x000000F8u)
#define T_Clock_DIV_FRAC_SHIFT (3u)
#define T_Clock_DIV_INT_MASK   (0xFFFFFF00u)
#define T_Clock_DIV_INT_SHIFT  (8u)

#else 

#define T_Clock_DIV_REG        (*(reg32 *)T_Clock__REGISTER)
#define T_Clock_ENABLE_REG     T_Clock_DIV_REG
#define T_Clock_DIV_FRAC_MASK  T_Clock__FRAC_MASK
#define T_Clock_DIV_FRAC_SHIFT (16u)
#define T_Clock_DIV_INT_MASK   T_Clock__DIVIDER_MASK
#define T_Clock_DIV_INT_SHIFT  (0u)

#endif/* CYREG_PERI_DIV_CMD */

#endif /* !defined(CY_CLOCK_T_Clock_H) */

/* [] END OF FILE */
