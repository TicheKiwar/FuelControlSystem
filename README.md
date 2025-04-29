# Distributed Fuel Control System - Company XYZ

## Description
A distributed system based on a microservices architecture using **.NET 8** and **gRPC**, designed to manage the fuel consumption of light and heavy machinery at Company XYZ.

## General Objective
Develop a distributed system that efficiently controls the fuel consumption of the company's vehicle fleet.

## Specific Objectives
- Implement independent microservices for managing drivers, vehicles, routes, and fuel consumption.
- Use **gRPC** as the communication mechanism between microservices.
- Ensure high availability and scalability of the system.
- Separate the management of light and heavy machinery.
- Guarantee interoperability between distributed components.

## General Architecture
- **Architectural Style:** Microservices
- **Communication:** gRPC (Protocol Buffers)
- **Persistence:** Independent databases or shared schemas (SQL Server / MongoDB)
- **Containers:** Docker (each microservice includes a Dockerfile)
- **Orchestration:** Kubernetes (future integration)

## Main Components
- **DriversService:** Driver management
- **VehiclesService:** Vehicle management (light and heavy)
- **RoutesService:** Route and distance management
- **FuelService:** Fuel consumption tracking
- **AuthService:** Authentication and authorization with JWT

## Microservices Design
Each microservice is structured into the following layers:
- `Controllers` (gRPC)
- `Application` (Business logic)
- `Domain` (Entities, interfaces)
- `Infrastructure` (Data access, gRPC clients)
- `Persistence` (Database management)

## Project Structure
```plaintext
/src
  /Services
    /Common
    /DriversService
    /VehiclesService
    /RoutesService
    /FuelService
    /AuthService
  /Protos
  /Shared
  /FuelControlSystem (Gateway)
```

## Installation and Setup

### Prerequisites
- .NET 8 SDK
- Docker
- Docker Compose

### Steps

1. Clone the repository:
```bash
git clone https://github.com/TicheKiwar/FuelControlSystem.git
cd FuelControlSystem
```

2. Build and run all services using Docker Compose:
```bash
docker-compose up --build
```
This will build and start all microservices, databases, and the API Gateway automatically using the provided Dockerfiles.

3. Access the services:
   - Use a gRPC client (e.g., Postman, BloomRPC, or a custom frontend).
   - If you have enabled an API Gateway (REST or gRPC-Web), access it via the configured domain or localhost.

4. Stop all services:
```bash
docker-compose down
```

## Development Workflow

### Adding a New Microservice
1. Create a new directory under `/src/Services`
2. Define the service's Protocol Buffers (`.proto` files) in the `/Protos` directory
3. Implement the service following the layered architecture
4. Add the service to the Docker Compose configuration

### Updating Existing Services
1. Modify the required `.proto` files if the contract changes
2. Update the service implementation
3. Rebuild and restart the affected services with Docker Compose

## Testing

### Running Unit Tests
```bash
dotnet test
```

### Integration Testing
Each microservice includes integration tests that can be run independently or as part of the CI/CD pipeline.

## Deployment

### Local Environment
Use Docker Compose as described in the Installation section.

### Production Environment
1. Build container images
2. Push to container registry
3. Deploy using Kubernetes manifests or Docker Compose

## Monitoring and Logging
- Each service logs to a centralized logging system
- Prometheus metrics are exposed for monitoring
- Grafana dashboards are available for visualization

## License
[MIT](LICENSE)

## Contributors
- [Kiwar Tiche](https://github.com/tichekiwar)
