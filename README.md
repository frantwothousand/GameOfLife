# GameOfLife

Proyecto final de Estructura de Datos, segundo semestre del 2022; según los lineamientos indicados por el profesor Ing. Luis Espinoza.


## Integrantes:

> López\
> Rodríguez

## Importante:

El juego se conecta automáticamente a un Hub de SignalR en el Launcher. El puerto deberá ser ajustado de manera manual en ``WpfGameOfLifeClient/Hubs/SessionHub.cs``. Para environment de desarrollo (debug), el puerto es ``7777``, de lo contrario el puerto ``5000``.

## Breves indicaciones:

Iniciar el servidor Signal R, luego, ejecutar el Launcher. Hacer click en `Nueva partida` y cargar un archivo ***JSON*** que es automáticamente generado al iniciar el Launcher. Por lo general, este archivo se encuentra o en la carpeta Debug, o en Release. Según sea el caso.


### TODO:

Automatizar e implementar en un solo void las funciones necesarias relacionadas al funcionamiento del juego como tal, y por ende, obtener la asincronidad.



