namespace e_commerce_project.Modles
{
    public class MailSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public bool UseSsl { get; set; } = false;
        public bool UseStartTls { get; set; } = false;

    }
}
