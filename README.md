# ECommerce API 🛒

Una Web API RESTful desarrollada con **.NET 8** siguiendo los principios de **Clean Architecture**, **Domain-Driven Design (DDD)** y el patrón **CQRS** mediante **MediatR**.

---

## 🏗️ Arquitectura del Proyecto

El proyecto está estructurado en cuatro capas independientes para garantizar mantenibilidad, escalabilidad y desacoplamiento:
ECommerce/
│    ├── ECommerce.Domain          # Entidades, Objetos de Valor, Excepciones del Dominio e Interfaces
│    ├── ECommerce.Application     # Casos de Uso (Commands/Queries), DTOs, Validaciones (FluentValidation)
│    ├── ECommerce.Infrastructure  # EF Core, Persistencia de Datos, Repositorios e Inyección de Dependencias
│    └── ECommerce.WebApi          # Controladores / Endpoints, Middlewares (GlobalExceptionHandler) y Swagger
└── ECommerce.sln

### Tecnologías y Librerías Utilizadas

* **Framework:** .NET 8 SDK
* **Persistencia:** Entity Framework Core 8 (Proveedor InMemory / SQL)
* **Patrón CQRS & Eventos:** MediatR
* **Validaciones:** FluentValidation
* **Documentación API:** Swagger / OpenAPI
* **Manejo de Errores:** Middleware personalizado (`IExceptionHandler`) con estándar **ProblemDetails** (RFC 7807)

---

## 🚀 Requisitos Previos

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior.
* IDE recomendado: Visual Studio 2022, Visual Studio Code o JetBrains Rider.

---

## ⚙️ Instalación y Configuración

1. **Clonar el repositorio:**

   ```bash
   git clone [https://github.com/zznacho/Ecommerce-Backend.git](https://github.com/zznacho/Ecommerce-Backend.git)
   cd Ecommerce-Backend

   Restaurar las dependencias:

Bash
dotnet restore
Compilar la solución:

Bash
dotnet build
🎮 Ejecución de la API
Para iniciar el servidor de desarrollo en la capa WebAPI, ejecuta:

Bash
dotnet run --project ECommerce.WebApi/ECommerce.WebApi.csproj
Una vez iniciada la aplicación, accede a Swagger UI en la URL correspondiente (ejemplo por defecto):

Swagger UI: https://localhost:7001/swagger o http://localhost:5000/swagger

🧪 Pruebas y Validación
Para ejecutar las pruebas unitarias o de integración en caso de tener proyectos de test asociados:

Bash
dotnet test
📁 Estructura del Middleware de Excepciones
El proyecto incluye un GlobalExceptionHandler centralizado que intercepta y formatea respuestas de error uniformes:

ValidationException: Retorna estado 400 Bad Request con el listado detallado de campos con errores.

DomainException: Retorna estado 400 Bad Request cuando se viola una regla de negocio del dominio.

Excepciones no controladas: Retorna estado 500 Internal Server Error.

📄 Licencia
Este proyecto es de uso libre y educativo bajo la licencia MIT.


---

Puedes guardar este contenido en la raíz de tu proyecto con el nombre **`README.md`**.
