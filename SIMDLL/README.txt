El dll generado por el archivo SIMDLL.cpp se encuentra en la raiz en la carpeta DEBUG.

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