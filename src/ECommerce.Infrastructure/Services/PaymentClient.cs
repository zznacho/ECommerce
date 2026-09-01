using System.Net.Http.Json;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Services;

public class PaymentClient : IPaymentClient
{
    private readonly HttpClient _httpClient;

    public PaymentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaymentResponseDto?> ProcessPaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new PaymentRequestDto(orderId, amount);
            
            // Envía la petición POST al endpoint del PaymentService (puerto 7200)
            var response = await _httpClient.PostAsJsonAsync("/api/payments/process", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<PaymentResponseDto>(cancellationToken: cancellationToken);
        }
        catch (HttpRequestException)
        {
            // Ocurre si el servicio PaymentService (7200) está totalmente apagado o inaccesible
            return null;
        }
        catch (TaskCanceledException)
        {
            // Ocurre si la llamada supera el tiempo límite (Timeout)
            return null;
        }
    }
}