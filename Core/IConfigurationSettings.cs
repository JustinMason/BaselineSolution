namespace Core
{
    /// <summary>
    /// </summary>
    public interface IConfigurationSettings
    {
        string SmtpCredentialsUserName { get; }
        string SmtpCredentialsPassword { get; }
        string SmtpAddressTo { get; }
        string SmtpAddressFrom { get; }
        string SmtpHostName { get; }
        string SmtpHostPort { get; }
        string SmtpEnableSsl { get; }
    }
    
}
