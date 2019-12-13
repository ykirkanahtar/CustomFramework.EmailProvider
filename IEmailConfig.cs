namespace CustomFramework.EmailProvider
{
    public interface IEmailConfig
    {
        string MailServer { get; set; }
        int MailServerPort { get; set; }
        bool EnableSsl { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}