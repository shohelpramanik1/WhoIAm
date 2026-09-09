namespace WhoIAm.Domain.Entities;

public class Subscription : BaseEntity
{
    public Guid UserId { get; set; }
    public SubscriptionTier Tier { get; set; } = SubscriptionTier.Free;
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; }
    public int MaxIdentities { get; set; } = 1;
    public bool HasAdvancedCustomization { get; set; }
    public bool HasCreatorTools { get; set; }
    
    public User? User { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public class Payment : BaseEntity
{
    public Guid SubscriptionId { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime? ProcessedAt { get; set; }
    public string? FailureReason { get; set; }
    
    public Subscription? Subscription { get; set; }
}

public enum SubscriptionTier
{
    Free,
    Premium,
    Creator
}

public enum PaymentStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
    Refunded
}
