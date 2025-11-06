using System;

namespace HotelAdminWpf8.Models
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }
        public int ReservaId { get; set; }
        public Reserva? Reserva { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public DateTime DataPagamento { get; set; }
    }
}
