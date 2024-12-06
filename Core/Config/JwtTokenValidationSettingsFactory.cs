namespace Core.Config
{
    public class JwtTokenValidationSettings
    {
        public String ValidIssuer { get; set; }

        public String ValidAudience { get; set; }

        public String SecretKey { get; set; }

        public int Duration { get; set; } //Minutes
    }
}