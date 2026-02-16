using System;

namespace OrderSystem.Domain.Entities;

public class PaymentInfo : Entity
{
    // Dados para o histórico (Snapshot)
    public string Method { get; private set; } // "CreditCard", "Pix", "Boleto"
    public decimal PaidAmount { get; private set; }
    // Rastreabilidade Externa
    public string TransactionReference { get; private set; } // O ID que vem do Gateway
    public string LastFourDigits { get; private set; }       // Apenas para o cliente identificar o cartão
    public string ProviderName { get; private set; }        // Ex: "Stripe", "Adyen"

    // Status do Pagamento (usando a dica da string que vimos antes)
    public string PaymentStatus { get; private set; } // "Authorized", "Captured", "Refunded", "Failed"

    public PaymentInfo(
        Guid Id,
        DateTimeOffset CreationDate,
        DateTimeOffset UpdateDate,
        bool Active,
        string Method,
        decimal PaidAmount,
        string TransactionReference,
        string LastFourDigits,
        string ProviderName,
        string PaymentStatus
        ) : base(Id, CreationDate, UpdateDate, Active)
    {
        this.Method = Method;
        this.PaidAmount = PaidAmount;
        this.TransactionReference = TransactionReference;
        this.LastFourDigits = LastFourDigits;
        this.ProviderName = ProviderName;
        this.PaymentStatus = PaymentStatus;
    }
}
