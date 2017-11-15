namespace UserService.Mail
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public EmailConfiguration()
        {
            SmtpServer = "aspmx.l.google.com/";//"ssl://smtp.gmail.com";
            SmtpPort = 25;
            SmtpUsername = "danielbahedev@gmail.com";
            SmtpPassword = "sajada18";
            
            PopServer = "popserver";
            PopPort = 25;
            PopUsername = "danielbahedev@gmail.com";
            PopPassword = "";
        }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public string PopServer { get; set; }
        public int PopPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }
    }
}