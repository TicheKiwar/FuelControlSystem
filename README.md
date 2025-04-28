Sistema Distribuido de Control de Combustible - Empresa XYZ
Descripción
Sistema distribuido basado en microservicios utilizando .NET 8 y gRPC para gestionar el consumo de combustible de maquinaria liviana y pesada en la empresa XYZ. El proyecto busca mejorar el control, optimizar recursos y facilitar la administración de rutas, vehículos y choferes.

Objetivo General
Desarrollar un sistema distribuido de gestión de consumo de combustible, asegurando alta disponibilidad, escalabilidad y separación entre maquinaria liviana y pesada.

Objetivos Específicos
Implementar microservicios independientes para choferes, vehículos, rutas y consumo de combustible.

Usar gRPC como mecanismo de comunicación eficiente.

Asegurar disponibilidad y escalabilidad mediante arquitectura distribuida.

Separar la administración entre maquinaria liviana y pesada.

Garantizar interoperabilidad entre componentes distribuidos en diferentes entornos de red.

Arquitectura General
Estilo arquitectónico: Microservicios

Comunicación entre servicios: gRPC

Persistencia: Base de datos por microservicio (SQL Server / MongoDB)

Contenedores: Docker

Orquestación futura: Kubernetes

Exposición externa: API Gateway (opcional)

Componentes del Sistema
Servicio de Choferes (DriversService)

Servicio de Vehículos (VehiclesService)

Servicio de Rutas (RoutesService)

Servicio de Combustible (FuelService)

Servicio de Autenticación y Autorización

Diseño Modular por Dominio
Cada microservicio se estructura en capas:

Controllers (gRPC)

Application (Lógica de negocio)

Domain (Entidades, interfaces)

Infrastructure (Acceso a datos, clientes gRPC)

Persistence (Persistencia en bases de datos)

Separación por Maquinaria
Los servicios de vehículos y combustible distinguen entre:

csharp
Copy
Edit
public enum TipoMaquinaria {
    Liviana,
    Pesada
}
Esto permite adaptar la lógica del sistema dinámicamente según el tipo de maquinaria.

Seguridad y Autenticación
Autenticación: JWT

Roles: Admin, Operador, Supervisor

Autorización: Basada en endpoints gRPC o API Gateway

Monitoreo y Escalabilidad
Logging: Serilog / Elastic Stack

Escalado: Horizontal por microservicio

Orquestación: Kubernetes (futuro)

Tecnologías Utilizadas
Backend: .NET 8

Comunicación: gRPC + Protocol Buffers

Bases de Datos: SQL Server / MongoDB

Contenedores: Docker

Orquestación: Kubernetes (opcional)

Estructura del Proyecto
plaintext
Copy
Edit
/src
  /Services
    /XYZ.DriversService
    /XYZ.VehiclesService
    /XYZ.RoutesService
    /XYZ.FuelService
  /Protos
  /Shared
  /Gateway (opcional)
  /Infrastructure (docker-compose, bases de datos)
Cómo Empezar
Clona el repositorio:

bash
Copy
Edit
git clone https://github.com/empresaXYZ/sistema-combustible.git
Construye los servicios con Docker:

bash
Copy
Edit
docker-compose up --build
Accede a los servicios gRPC o vía API Gateway (si está habilitado).
