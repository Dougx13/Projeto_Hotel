using System;
using System.Linq;
using System.Windows;
using HotelAdminWpf8.Data;
using HotelAdminWpf8.Models;

namespace HotelAdminWpf8.Views
{
    public partial class CheckInWindow : Window
    {
        private readonly HotelContext _context;

        // ✅ Construtor sem parâmetros — Agora existe!
        public CheckInWindow()
        {
            InitializeComponent();
            _context = new HotelContext();
        }

        // ✅ Construtor opcional para abrir com cliente pré-carregado
        public CheckInWindow(Cliente cliente) : this()
        {
            if (cliente != null)
            {
                txtEmail.Text = cliente.Email;
                txtCPF.Text = cliente.CPF;
            }
        }

        private void BtnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string cpf = txtCPF.Text.Trim();

            var cliente = _context.Clientes.FirstOrDefault(c => c.Email == email && c.CPF == cpf);

            if (cliente == null)
            {
                txtStatus.Text = "Cliente não encontrado!";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            var reserva = _context.Reservas.FirstOrDefault(r => r.ClienteId == cliente.ClienteId && r.Status == "Reservado");

            if (reserva == null)
            {
                txtStatus.Text = "Nenhuma reserva pendente de check-in encontrada.";
                txtStatus.Foreground = System.Windows.Media.Brushes.OrangeRed;
                return;
            }

            reserva.Status = "Check-in";
            _context.SaveChanges();

            txtStatus.Text = $"Check-in realizado com sucesso para {cliente.Nome}!";
            txtStatus.Foreground = System.Windows.Media.Brushes.Green;
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow("Administrador").Show();
            this.Close();
        }
    }
}
