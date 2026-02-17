using OrderSystem.Domain.Entities;

namespace OrderSystem.Application.DTOs.Order;

public record class PaymentInfoDto
{
    public PaymentMethod Method { get; set; } // "CreditCard", "Pix", "Boleto"
    public decimal PaidAmount { get; set; }
    public string TransactionReference { get; set; } = string.Empty;
    public string LastFourDigits { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
}
