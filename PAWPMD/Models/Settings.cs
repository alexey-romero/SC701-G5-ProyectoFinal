namespace PAWPMD.Mvc.Models
{
    public class AppSettings
    {
        public LogginSettings Loggin { get; set; }
        public string AllowedHosts { get; set; }

        public string LoginApi {  get; set; }

        public string SignUpApi { get; set; }

        public string UserApi { get; set; }
    }

    public class LogginSettings
    {
       public LogLevelSettings LogLevel { get; set; }
    }

    public class LogLevelSettings
    {
        public string Default { get; set; }

        public string MicrosoftAspNetCore { get; set; }
    }
}
