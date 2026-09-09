namespace WhoIAm.Domain.Entities;

public class JournalEntry : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid VirtualIdentityId { get; set; }
    public string Content { get; set; } = string.Empty;
    public JournalEntryType Type { get; set; } = JournalEntryType.Text;
    public bool IsPrivate { get; set; } = true;
    public string? Mood { get; set; }
    
    public User? User { get; set; }
    public VirtualIdentity? VirtualIdentity { get; set; }
    public ICollection<JournalTag> Tags { get; set; } = new List<JournalTag>();
}

public class JournalTag : BaseEntity
{
    public Guid JournalEntryId { get; set; }
    public string Tag { get; set; } = string.Empty;
    
    public JournalEntry? JournalEntry { get; set; }
}

public enum JournalEntryType
{
    Text,
    Voice,
    Mixed
}
