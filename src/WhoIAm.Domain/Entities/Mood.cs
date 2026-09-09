namespace WhoIAm.Domain.Entities;

public class MoodEntry : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid VirtualIdentityId { get; set; }
    public MoodType Mood { get; set; }
    public int Intensity { get; set; } // 1-10
    public string? Note { get; set; }
    
    public User? User { get; set; }
    public VirtualIdentity? VirtualIdentity { get; set; }
}

public enum MoodType
{
    Happy,
    Good,
    Neutral,
    Sad,
    Angry,
    Stressed,
    Lonely,
    Overwhelmed,
    Loved,
    Motivated,
    Confused,
    Other
}
