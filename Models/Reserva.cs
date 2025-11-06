using System;

namespace HotelAdminWpf8.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int QuartoId { get; set; }
        public Quarto? Quarto { get; set; }
        public int? FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
