namespace HotelAdminWpf8.Models
{
    public class Quarto
    {
        public int QuartoId { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal PrecoDiaria { get; set; }

        public int TipoQuartoId { get; set; }
        public TipoQuarto? TipoQuarto { get; set; }
    }
}
