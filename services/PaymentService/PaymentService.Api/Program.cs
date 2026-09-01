using PaymentService.Application.Features.Payments.Commands.ProcessPayment;
using PaymentService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(ProcessPaymentCommand).Assembly));

// Infraestructura
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();