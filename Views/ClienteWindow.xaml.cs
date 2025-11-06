using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using HotelAdminWpf8.Data;
using HotelAdminWpf8.Views;
using HotelAdminWpf8.Models;

namespace HotelAdminWpf8.Views
{
    public partial class ClienteWindow : Window
    {
        private readonly Cliente _cliente;

        public ClienteWindow(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));

            txtNome.Text = $"Bem-vindo, {cliente.Nome ?? "Cliente"}";
            CarregarReservas();
        }

        private void CarregarReservas()
        {
            using var ctx = new HotelContext();
            var reservas = ctx.Reservas
                .Include(r => r.Quarto)
                .ThenInclude(q => q.TipoQuarto)
                .Where(r => r.ClienteId == _cliente.ClienteId)
                .ToList();

            dgReservas.ItemsSource = reservas;
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnFazerReserva_Click(object sender, RoutedEventArgs e)
        {
            var win = new ReservaClienteWindow(_cliente);
            win.ShowDialog();
            CarregarReservas();
        }
    }
}
