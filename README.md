# ECommerce API 🛒

Sistema de Comercio Electrónico distribuido desarrollado en **.NET 8** aplicando **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS** con **MediatR**, autenticación basada en **JWT** y comunicación REST mediante `HttpClient` entre microservicios.

---

## 🏗️ Arquitectura del Proyecto

El proyecto está estructurado en capas independientes dentro de la carpeta `src/` y una arquitectura de microservicios para garantizar mantenibilidad, escalabilidad y desacoplamiento:


# ECommerce API 🛒

Sistema de Comercio Electrónico distribuido desarrollado en **.NET 8** aplicando **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS** con **MediatR**, autenticación basada en **JWT** y comunicación REST mediante `HttpClient` entre microservicios.

---

## 🏗️ Arquitectura del Proyecto

El proyecto está estructurado en capas independientes dentro de la carpeta `src/` y una arquitectura de microservicios para garantizar mantenibilidad, escalabilidad y desacoplamiento:

```text
Ecommerce-Backend/
├── src/
│   ├── ECommerce.Domain          # Entidades (User, Product, Order), Enums y Reglas de Negocio
│   ├── ECommerce.Application     # Casos de Uso (Commands/Queries), DTOs, Interfaces e IPaymentClient
│   ├── ECommerce.Infrastructure  # EF Core (InMemory DB), Repositorios, Servicio JWT y PaymentClient (HttpClient)
│   └── ECommerce.WebApi          # Controladores (Auth, Products, Orders), Middlewares y Swagger
└── services/
    └── PaymentService/       # Microservicio distribuido autónomo para procesamiento de pagos (Puerto 7200)


🛠️ Tecnologías y Librerías
Framework: .NET 8 SDK

Comunicación Distribuida: IHttpClientFactory con Typed Client (PaymentClient)

Persistencia: Entity Framework Core 8 (In-Memory Database)

Patrón CQRS: MediatR

Validaciones: FluentValidation

Seguridad & Autenticación: JWT (JSON Web Tokens) & Bearer Scheme

Documentación API: Swagger / OpenAPI con soporte JWT

Manejo de Errores: Global Exception Handler con estándar ProblemDetails (RFC 7807)

🔐 Autenticación y Control de Acceso (RBAC)
La API implementa autorización basada en roles (Role-Based Access Control):

Admin: Acceso total a la creación, edición y eliminación de catálogo (POST/PUT/DELETE /api/products).

Customer: Acceso de lectura al catálogo (GET /api/products) y creación de órdenes (POST /api/orders).


💳 Integración del PaymentService (Microservicio de Pagos)
El sistema integra un servicio independiente en el puerto 7200 para evaluar transacciones:

Montos < $100.000: El pago es aprobado (Approved) y la orden pasa a estado Paid.

Montos >= $100.000: El pago es rechazado (Rejected) y la orden pasa a estado PaymentRejected.

Servicio Caído / Inaccesible: El E-Commerce captura la excepción y asigna el estado PaymentServiceUnavailable.

🚀 Requisitos Previos e Instalación
Requisito: .NET 8 SDK instalado.

Clonar el repositorio:

Bash
git clone [https://github.com/zznacho/Ecommerce-Backend.git](https://github.com/zznacho/Ecommerce-Backend.git)
cd Ecommerce-Backend
Restaurar dependencias:

Bash
dotnet restore


🎮 Guía de Ejecución Distribuida
Para ejecutar el flujo completo, debes iniciar ambos servicios en terminales independientes:

1. Iniciar PaymentService (Terminal 1)
PowerShell
dotnet run --project services/PaymentService/PaymentService.Api/PaymentService.Api.csproj
URL: http://localhost:7200

Swagger: http://localhost:7200/swagger

2. Iniciar ECommerce API (Terminal 2)
PowerShell
dotnet run --project src/ECommerce.WebApi/ECommerce.WebApi.csproj
URL: http://localhost:7100 (o el puerto asignado en la consola)

Swagger: http://localhost:7100/swagger


🔑 Flujo de Prueba End-to-End en Swagger
Obtener Token: Registra un usuario en POST /api/auth/register e inicia sesión en POST /api/auth/login para copiar el JWT.

Autenticar: Haz clic en Authorize 🔒 e ingresa tu token.

Probar Orden Aprobada: Envía POST /api/orders con {"totalAmount": 45000} $\rightarrow$ Estado: Paid.

Probar Orden Rechazada: Envía POST /api/orders con {"totalAmount": 150000} $\rightarrow$ Estado: PaymentRejected.

Probar Resiliencia: Detén la terminal del PaymentService y crea una orden $\rightarrow$ Estado: PaymentServiceUnavailable.📄 LicenciaEste proyecto es de uso libre y educativo bajo la licencia MIT.