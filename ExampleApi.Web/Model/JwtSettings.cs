namespace ExampleApi.Web.Model
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string SigningAlgorithm { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}