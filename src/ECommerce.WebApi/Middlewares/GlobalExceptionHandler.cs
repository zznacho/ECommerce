using ECommerce.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            ValidationException valEx => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Error de Validación",
                Detail = "Una o más validaciones de la petición fallaron.",
                Extensions = { ["errors"] = valEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) }
            },
            DomainException domEx => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Regla de Negocio Violada",
                Detail = domEx.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error Interno del Servidor",
                Detail = "Ocurrió un error inesperado al procesar la solicitud."
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}