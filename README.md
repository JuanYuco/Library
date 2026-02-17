# Administrador de autores y libros.
El presente proyecto es un administrador de libros y autores el cual cuenta con un backned construido con una arquitectura hexagonal para el desacoplamiento de sus componentes y un proyecto web MVC.

## Arquitectura del proyecto.
### Backend.
Esta divido en varios proyectos:
- Library.Domain: Aquí se encuentran las entidades que se mapean de la base de datos y los puertos que contienen las interfaces de los repositorios.
- Library.Infrastructure: Configuración y conexión con la base de datos.
- Library.Application: interfaces, servicios y DTO's con la logica del negocio.
- Library.Api: Controladores, es el punto de entrada del proyecto.
- Library.Test: Pruebas unitarias de los servicios.

### Proyecto WEB.
Esta construido con una arquitectura MVC, cada agrupación de vistas tiene su controlador, a pesar de estar en la misma solución del backend, se encuentra totalmente desacoplado del mismo.

## Tecnologías utilizadas.
- Base de datos: SQL Server.
- Backend: .NET 9
- Web: Asp NET CORE MVC.
- Pruebas: xUnit y Moq.
- Documentación: Swagger.

## Iniciar el proyecto.
- Ejecutar el archivo `script.sql` ubicado en la carpeta `Database` del proyecto Library.Infrastructure en una isntancia de SQL Server.
- Configurar la cadena de conexión en los secretos de usuario, puede tomar de referencia la estructura agregada en el `appsettings.json` del proyecto Library.Api.
- Configurar el máximo de libros permitidos en el `appsettings.json` del proyecto Library.Api, la propiedad se encuentra dentro de la sección `LibrarySettings` con la llave `MaxBooks`
- Configurar la url base del backend en el `appsettings.json` del proyecto Library.Web, la llave de la propiedad es `ApiBaseUrl`.
- Desde visual estudio configurar como proyecto de inicio múltiple y seleccionar los proyectos Library.Api y Library.Web.

Una vez iniciado el proyecto Backend podra visualizar una pequeña documentación de los endpoints creados.
