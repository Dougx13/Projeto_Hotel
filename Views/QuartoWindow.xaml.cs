using System.Linq;
using System.Windows;
using HotelAdminWpf8.Data;
using HotelAdminWpf8.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAdminWpf8.Views
{
    public partial class QuartoWindow : Window
    {
        public QuartoWindow()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            using var ctx = new HotelContext();
            dgQuartos.ItemsSource = ctx.Quartos.Include(q => q.TipoQuarto).ToList();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using var ctx = new HotelContext();
            var quarto = new Quarto
            {
                Numero = "Novo Quarto",
                Status = "Disponível",
                TipoQuartoId = 1, // coloque um ID existente
                PrecoDiaria = 120
            };
            ctx.Quartos.Add(quarto);
            ctx.SaveChanges();
            Carregar();
        }
    }
}
