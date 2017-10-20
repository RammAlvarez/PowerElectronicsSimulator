/*******************************************************************************
* File Name: Digital_Output_Pin_U2.h  
* Version 2.10
*
* Description:
*  This file containts Control Register function prototypes and register defines
*
* Note:
*
********************************************************************************
* Copyright 2008-2014, Cypress Semiconductor Corporation.  All rights reserved.
* You may use this file only in accordance with the license, terms, conditions, 
* disclaimers, and limitations in the end user license agreement accompanying 
* the software package with which this file was provided.
*******************************************************************************/

#if !defined(CY_PINS_Digital_Output_Pin_U2_H) /* Pins Digital_Output_Pin_U2_H */
#define CY_PINS_Digital_Output_Pin_U2_H

#include "cytypes.h"
#include "cyfitter.h"
#include "Digital_Output_Pin_U2_aliases.h"


/***************************************
*        Function Prototypes             
***************************************/    

void    Digital_Output_Pin_U2_Write(uint8 value) ;
void    Digital_Output_Pin_U2_SetDriveMode(uint8 mode) ;
uint8   Digital_Output_Pin_U2_ReadDataReg(void) ;
uint8   Digital_Output_Pin_U2_Read(void) ;
uint8   Digital_Output_Pin_U2_ClearInterrupt(void) ;


/***************************************
*           API Constants        
***************************************/

/* Drive Modes */
#define Digital_Output_Pin_U2_DRIVE_MODE_BITS        (3)
#define Digital_Output_Pin_U2_DRIVE_MODE_IND_MASK    (0xFFFFFFFFu >> (32 - Digital_Output_Pin_U2_DRIVE_MODE_BITS))

#define Digital_Output_Pin_U2_DM_ALG_HIZ         (0x00u)
#define Digital_Output_Pin_U2_DM_DIG_HIZ         (0x01u)
#define Digital_Output_Pin_U2_DM_RES_UP          (0x02u)
#define Digital_Output_Pin_U2_DM_RES_DWN         (0x03u)
#define Digital_Output_Pin_U2_DM_OD_LO           (0x04u)
#define Digital_Output_Pin_U2_DM_OD_HI           (0x05u)
#define Digital_Output_Pin_U2_DM_STRONG          (0x06u)
#define Digital_Output_Pin_U2_DM_RES_UPDWN       (0x07u)

/* Digital Port Constants */
#define Digital_Output_Pin_U2_MASK               Digital_Output_Pin_U2__MASK
#define Digital_Output_Pin_U2_SHIFT              Digital_Output_Pin_U2__SHIFT
#define Digital_Output_Pin_U2_WIDTH              1u


/***************************************
*             Registers        
***************************************/

/* Main Port Registers */
/* Pin State */
#define Digital_Output_Pin_U2_PS                     (* (reg32 *) Digital_Output_Pin_U2__PS)
/* Port Configuration */
#define Digital_Output_Pin_U2_PC                     (* (reg32 *) Digital_Output_Pin_U2__PC)
/* Data Register */
#define Digital_Output_Pin_U2_DR                     (* (reg32 *) Digital_Output_Pin_U2__DR)
/* Input Buffer Disable Override */
#define Digital_Output_Pin_U2_INP_DIS                (* (reg32 *) Digital_Output_Pin_U2__PC2)


#if defined(Digital_Output_Pin_U2__INTSTAT)  /* Interrupt Registers */

    #define Digital_Output_Pin_U2_INTSTAT                (* (reg32 *) Digital_Output_Pin_U2__INTSTAT)

#endif /* Interrupt Registers */


/***************************************
* The following code is DEPRECATED and 
* must not be used.
***************************************/

#define Digital_Output_Pin_U2_DRIVE_MODE_SHIFT       (0x00u)
#define Digital_Output_Pin_U2_DRIVE_MODE_MASK        (0x07u << Digital_Output_Pin_U2_DRIVE_MODE_SHIFT)


#endif /* End Pins Digital_Output_Pin_U2_H */


/* [] END OF FILE */
