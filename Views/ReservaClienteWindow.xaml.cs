using System.Windows;
using HotelAdminWpf8.Data;
using HotelAdminWpf8.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HotelAdminWpf8.Views
{
    public partial class ReservaClienteWindow : Window
    {
        private readonly HotelContext _context;
        private readonly Cliente _cliente;

        public ReservaClienteWindow(Cliente cliente)
        {
            InitializeComponent();
            _context = new HotelContext();
            _cliente = cliente;
            cbQuartos.ItemsSource = _context.Quartos.Include(q => q.TipoQuarto).ToList();
        }

        private void btnReservar_Click(object sender, RoutedEventArgs e)
        {

            if (cbQuartos.SelectedItem is Quarto quarto && dpEntrada.SelectedDate != null && dpSaida.SelectedDate != null)
            {
                var reserva = new Reserva
                {
                    ClienteId = _cliente.ClienteId,
                    QuartoId = quarto.QuartoId,
                    DataEntrada = dpEntrada.SelectedDate.Value,
                    DataSaida = dpSaida.SelectedDate.Value,
                    Valor = quarto.PrecoDiaria, // ou quarto.TipoQuarto.ValorMensal se for mensal
                    Status = "Reservado"
                };

                _context.Reservas.Add(reserva);

                // muda status do quarto
                quarto.Status = "Reservado";

                _context.SaveChanges();

                MessageBox.Show("Reserva realizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // ✅ Voltar para ClienteWindow
                var clienteWindow = new ClienteWindow(_cliente);
                clienteWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
