# Sistema Distribuido de Control de Combustible - Empresa XYZ

## Descripción
Sistema distribuido basado en microservicios utilizando **.NET 8** y **gRPC**, diseñado para la gestión del consumo de combustible de maquinaria liviana y pesada en la empresa XYZ.

## Objetivo General
Desarrollar un sistema distribuido que permita controlar de forma eficiente el consumo de combustible de la flota de vehículos.

## Objetivos Específicos
- Implementar microservicios independientes para el control de choferes, vehículos, rutas y consumo de combustible.
- Utilizar **gRPC** como mecanismo de comunicación entre microservicios.
- Asegurar alta disponibilidad y escalabilidad del sistema.
- Separar la administración de maquinaria liviana y pesada.
- Garantizar la interoperabilidad entre componentes distribuidos.

## Arquitectura General
- **Estilo arquitectónico:** Microservicios
- **Comunicación:** gRPC (Protocol Buffers)
- **Persistencia:** Bases de datos separadas o esquema compartido (SQL Server / MongoDB)
- **Contenedores:** Docker
- **Orquestación:** Kubernetes (futuro)

## Componentes Principales
- **DriversService:** Gestión de choferes
- **VehiclesService:** Gestión de vehículos (livianos y pesados)
- **RoutesService:** Gestión de rutas y distancias
- **FuelService:** Registro de consumo de combustible
- **AuthService:** Autenticación y autorización con JWT

## Diseño de Microservicios
Cada microservicio está estructurado en las siguientes capas:
- `Controllers` (gRPC)
- `Application` (Lógica de negocio)
- `Domain` (Entidades, interfaces)
- `Infrastructure` (Acceso a datos, clientes gRPC)
- `Persistence` (Manejo de bases de datos)

## Separación de Maquinaria
El sistema diferencia entre maquinaria **liviana** y **pesada** mediante el siguiente enum:
```csharp
public enum TipoMaquinaria {
    Liviana,
    Pesada
}
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
Pasos
Clonar el repositorio:
git clone https://github.com/empresaXYZ/sistema-combustible.git
cd sistema-combustible


---

¿Te gustaría que también te genere una versión con algunos *badges* (por ejemplo: Docker ready, .NET 8, Kubernetes ready)? 🚀  
¡Podría hacer que tu `README.md` se vea aún más profesional si quieres!
