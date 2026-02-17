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


