# ECommerce API 🛒

Una Web API RESTful desarrollada con **.NET 8** siguiendo los principios de **Clean Architecture**, **Domain-Driven Design (DDD)**, el patrón **CQRS** mediante **MediatR** y autenticación basada en **JWT (JSON Web Tokens)** con control de acceso por roles.

---

## 🏗️ Arquitectura del Proyecto

El proyecto está estructurado en cuatro capas independientes para garantizar mantenibilidad, escalabilidad y desacoplamiento:

ECommerce/
├── 
│    ├── ECommerce.Domain          # Entidades (User, Product), Constantes de Roles, Reglas de Negocio
│    ├── ECommerce.Application     # Casos de Uso (Commands/Queries), Interfaces (IUserRepository, IJwtTokenGenerator), Validaciones
│    ├── ECommerce.Infrastructure  # EF Core (InMemory DB), Repositorios, Servicio JWT e Inyección de Dependencias
│    └── ECommerce.WebApi          # Controladores (Auth, Products), Middlewares y Configuración de Swagger JWT
└── ECommerce.sln

## 🛠️ Tecnologías y Librerías

* **Framework:** .NET 8 SDK
* **Persistencia:** Entity Framework Core 8 (In-Memory Database)
* **Patrón CQRS & Eventos:** MediatR
* **Validaciones:** FluentValidation
* **Seguridad & Autenticación:** JWT (JSON Web Tokens) & Bearer Scheme
* **Documentación API:** Swagger / OpenAPI con soporte para tokens
* **Manejo de Errores:** Middleware personalizado (`IExceptionHandler`) con estándar **ProblemDetails** (RFC 7807)

---

## 🔐 Autenticación y Control de Accesos (RBAC)

La API implementa un modelo de autorización basado en roles (**Role-Based Access Control**):

* **`Admin`:** Acceso total a la creación, actualización y eliminación de recursos (`POST /api/products`, `DELETE /api/products/{id}`).
* **`Customer`:** Acceso de lectura a catálogos y operaciones del cliente (`GET /api/products`).

---

## 🚀 Requisitos Previos e Instalación

1. **Requisitos:** [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior.
2. **Clonar el repositorio:**
   ```bash
   git clone [https://github.com/zznacho/Ecommerce-Backend.git](https://github.com/zznacho/Ecommerce-Backend.git)
   cd Ecommerce-Backend
Restaurar dependencias y compilar:

Bash
dotnet restore
dotnet build
🎮 Ejecución del Proyecto
Para iniciar el servidor de desarrollo:

Bash
dotnet run --project ECommerce.WebApi/ECommerce.WebApi.csproj
Abre tu navegador e ingresa a la interfaz interactiva de Swagger:
👉 https://localhost:7001/swagger (o la URL asignada en tu terminal).

🔑 Flujo de Autenticación en Swagger UI
Registrar un usuario:
Accede al endpoint POST /api/auth/register indicando el rol (Admin o Customer):

JSON
{
  "email": "admin@ecommerce.com",
  "password": "Password123!",
  "role": "Admin"
}
Obtener el Token:
Accede al endpoint POST /api/auth/login con tus credenciales para recibir el token JWT.

Autenticar la sesión en Swagger:

Haz clic en el botón Authorize 🔒 (arriba a la derecha).

Pega únicamente el string del token generado (sin anteponer la palabra Bearer).

Haz clic en Authorize y prueba consumir los endpoints protegidos.

📁 Estructura del Middleware de Excepciones
El proyecto incluye un GlobalExceptionHandler centralizado que formatea respuestas uniformes bajo el estándar RFC 7807:

401 Unauthorized: Intentar consumir endpoints protegidos sin token válido.

403 Forbidden: Intentar realizar acciones de Admin autenticado como Customer.

400 Bad Request: Violaciones de validación de entradas o reglas de negocio del dominio.

📄 Licencia
Este proyecto es de uso libre y educativo bajo la licencia MIT.