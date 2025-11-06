using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using HotelAdminWpf8.Data;

namespace HotelAdminWpf8.Views
{
    public partial class AdminWindow : Window
    {
        private readonly string _nomeAdmin;

        public AdminWindow(string nome)
        {
            InitializeComponent();
            _nomeAdmin = nome;
            Title = $"Painel do Administrador - {_nomeAdmin}";
            CarregarReservas();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement)?.Tag?.ToString();

            switch (tag)
            {
                case "Reservas":
                    CarregarReservas();
                    break;

                case "Clientes":
                    CarregarClientes();
                    break;

                case "Tipos":
                    CarregarTipos();
                    break;

                case "Checkin":
                    var checkInWindow = new CheckInWindow();
                    checkInWindow.ShowDialog();
                    break;

                case "Quartos":
                    new QuartoWindow().ShowDialog();
                    break;


                case "Sair":
                    new LoginWindow().Show();
                    this.Close();
                    break;



            }
        }

        private void CarregarReservas()
        {

            btnExcluirCliente.IsEnabled = false; // Não excluir reservas

            using var ctx = new HotelContext();
            var reservas = ctx.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Quarto)
                .Select(r => new
                {
                    r.ReservaId,
                    Cliente = r.Cliente!.Nome,
                    Quarto = r.Quarto!.Numero,
                    r.DataEntrada,
                    r.DataSaida,
                    r.Valor,
                    r.Status
                })
                .ToList();

            dgDados.ItemsSource = reservas;
        }
        private void btnExcluirCliente_Click(object sender, RoutedEventArgs e)
        {
            if (dgDados.SelectedValue == null)
            {
                MessageBox.Show("Selecione um cliente na aba de clientes.");
                return;
            }

            int id = Convert.ToInt32(dgDados.SelectedValue);

            using var ctx = new HotelContext();
            var cliente = ctx.Clientes.Find(id);

            if (cliente != null)
            {
                ctx.Clientes.Remove(cliente);
                ctx.SaveChanges();
                CarregarClientes();
                MessageBox.Show("Cliente excluído!");
            }
        }

        private void CarregarClientes()
        {

            using var ctx = new HotelContext();
            var clientes = ctx.Clientes
                .Select(c => new
                {
                    c.ClienteId,
                    c.Nome,
                    c.Telefone,
                    c.Email,
                    c.CPF
                })
                .ToList();

            dgDados.ItemsSource = clientes;
            dgDados.SelectedValuePath = "ClienteId";

            btnExcluirCliente.IsEnabled = true;
        }

        private void CarregarTipos()
        {
            using var ctx = new HotelContext();
            var tipos = ctx.TiposQuarto
                .Select(t => new
                {
                    t.TipoQuartoId,
                    Nome = t.Nome,
                    ValorMensal = t.ValorMensal
                })
                .ToList();

            dgDados.ItemsSource = tipos;
            btnExcluirCliente.IsEnabled = false;
        }
    }
}