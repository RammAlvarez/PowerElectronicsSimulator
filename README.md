# Power Electronics Simulator
El objetivo de este proyecto es la prueba de un modelo matemático de un controlador para un rectificador multinivel monofásico híbrido en código C 
dentro de un dll con una función expuesta de hasta 75 parametros flotantes de paso por referencia permitiendo cargarlo y cambiar los 
valores de estos en tiempo de ejecución, para lograr un prototipado más rápido y agíl sin necesidad de un microcontrolador.

-----
# ¿Cómo utilizar el programa?

## Prerequisitos
1. [.NET Framework 4.5 o superior](https://www.microsoft.com/es-mx/download/details.aspx?id=30653)

## Guía de uso rápido
1. Ubicarse en la carpeta **Proyecto de prueba**.
2. Abrir la aplicación **PowerElectronicSimulator** en la carpeta.
3. Tienes dos opciones: utilizar el proyecto de prueba **Test** o crear un nuevo proyecto
4. **Abrir el proyecto de prueba**
> 1. Abre el proyecto en el menú *Archivo/Abrir*
> 2. Escoge el archivo Test.xml y dale abrir
> 3. En la parte de en medio del programa en el combobox *"Señal de salida a monitorear"* escoge la señal *v_t* 
(Esta señal, que significa *tensión total de salida*, es el objetivo de la tesis que se encuentra en la carpeta *Documentos de apoyo*)
> 4. Darle **Iniciar** y la gráfica empezará a formarse
> 5. [OPCIONAL] Si quieres cambiar el valor de salida del sistema y cambia el valor de vt_des (*Tension total deseada*) 
a un valor cualquiera por ejemplo 90 (significa 90 V) y presiona 90. El valor por defecto de salida deseado del proyecto de prueba es 
70 V.
5. **Crear un nuevo proyecto**
> 1. Crea un nuevo proyecto en el menú *Archivo/Nuevo*, 
> 2. Ingresa un nombre de proyecto que desees
> 3. Da click en "*Buscar*" para encontrar la dirección del archivo dll en C, escoge *"SIMDLL.dll"* y dale abrir
> 4. Escribe el nombre de la función a invocar. Por defecto *SteppedSimulation*
> 5.  click en "*Buscar*" para encontrar la dirección del archivo CSV con la variables de inicio, 
escoge *"Variables-Nombres y Valores-01.csv"* y dale abrir
> 6. Usa los pasos **3, 4 y 5** de **Abrir proyecto de prueba** 
6. Puedes utilizar los controles de la derecha para ajustar la frecuencia de actualización de la gráfica y el tamaño de la ventana de 
la gráfica, el botón **AUTOSET** ajusta la escala automaticamente como si fuera un osciloscopio y el botón **RESET** regresa la
escala de la gráfica a su estado original.
7. [OPCIONAL] En la parte de en medio puedes ver dos pestañas **Simulador** y **Interface a emulador**, si seleccionas la ultima te 
permitirá
conectarte por UART con un microcontrolador a través de un formato de paquetes propetario(el cuál se encuentra en este proyecto).


-----
# ¿Cómo utlilizar este código?

## Prerequisitos
1. [.NET Framework 4.5 o superior](https://www.microsoft.com/es-mx/download/details.aspx?id=30653)
2. [Visual Studio 2017](https://www.visualstudio.com/es/?rr=https%3A%2F%2Fwww.google.com.mx%2F)
3. [PSoC Creator](http://www.cypress.com/products/psoc-creator-integrated-design-environment-ide) 
*(Para los archivos en la carpeta emulador que sirven para programar un microcontrolador PSoC 4)*

## Stack
1. **Programa de Escritorio**, lenguaje C# con Windows Forms, XML y CSV.
2. **DLL**, lenguaje C
3. **Microcontrolador**, lenguaje C.

## Guía de inicio rápido
1. Clona este proyecto y abrelo en Visual Studio.
2. Presiona **Ctrl+F5** para probarlo(Si intentas correrlo con F5 te encotraras con errores por la llamada dinámica al dll), 
puedes seguir los pasos de arriba para utilizarlo.

Esta solución contiene el proyecto de la aplicación de escritorio en C# y el proyecto del dll en C.


