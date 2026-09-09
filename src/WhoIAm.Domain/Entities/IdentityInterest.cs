namespace WhoIAm.Domain.Entities;

public class IdentityInterest : BaseEntity
{
    public Guid VirtualIdentityId { get; set; }
    public string Interest { get; set; } = string.Empty;
    
    public VirtualIdentity? VirtualIdentity { get; set; }
}
