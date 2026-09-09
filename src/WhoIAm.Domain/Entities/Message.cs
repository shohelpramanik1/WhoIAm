namespace WhoIAm.Domain.Entities;

public class Conversation : BaseEntity
{
    public Guid InitiatorUserId { get; set; }
    public Guid? RecipientUserId { get; set; }
    public bool IsAnonymous { get; set; }
    public DateTime? LastMessageAt { get; set; }
    
    public User? InitiatorUser { get; set; }
    public User? RecipientUser { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

public class Message : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    
    public Conversation? Conversation { get; set; }
    public User? Sender { get; set; }
    public ICollection<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
}

public class MessageAttachment : BaseEntity
{
    public Guid MessageId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    
    public Message? Message { get; set; }
}
