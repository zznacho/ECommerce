using ECommerce.Application;
using ECommerce.Application.Common.Behaviors;
using ECommerce.Infrastructure;
using ECommerce.WebApi.Middlewares;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Registrar Servicios de la capa Application (MediatR, FluentValidation y Pipeline Behavior)
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

// 3. Registrar Capa de Infraestructura
builder.Services.AddInfrastructure(builder.Configuration);

// 4. Registrar Manejo de Excepciones Global (ProblemDetails / RFC 7807)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(); // Habilita nuestro manejador global RFC 7807
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();