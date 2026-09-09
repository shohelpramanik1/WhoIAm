namespace WhoIAm.Domain.Exceptions;

public class DomainException : Exception
{
    public string? Code { get; set; }

    public DomainException(string message, string? code = null) : base(message)
    {
        Code = code;
    }
}
