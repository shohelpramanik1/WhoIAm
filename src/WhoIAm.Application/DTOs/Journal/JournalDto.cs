namespace WhoIAm.Application.DTOs.Journal;

public class CreateJournalEntryRequest
{
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = "Text"; // Text, Voice, Mixed
    public bool IsPrivate { get; set; } = true;
    public string? Mood { get; set; }
    public List<string>? Tags { get; set; }
}

public class JournalEntryResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsPrivate { get; set; }
    public string? Mood { get; set; }
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
