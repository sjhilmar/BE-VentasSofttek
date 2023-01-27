namespace BE_VentasSofttek.Models.DTO
{
    public class JwtDTO
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
