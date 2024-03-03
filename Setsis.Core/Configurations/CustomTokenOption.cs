namespace Setsis.Core.Configurations
{
    public class CustomTokenOption
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
        public string Secret { get; set; }
    }
}
