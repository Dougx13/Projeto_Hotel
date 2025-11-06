using System;
using System.Linq;
using System.Windows;
using HotelAdminWpf8.Data;
using HotelAdminWpf8.Models;

namespace HotelAdminWpf8
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var ctx = new HotelContext();
            ctx.Database.EnsureCreated();

            if (!ctx.Funcionarios.Any())
            {
                var admin = new Funcionario { Nome = "Admin", Usuario = "admin@hotel.com", Senha = "1234" };
                var tipo = new TipoQuarto { Nome = "Simples", ValorMensal = 1000m };
                var quarto = new Quarto { Numero = "101", PrecoDiaria = 100m, Status = "Disponível", TipoQuarto = tipo };
                var cliente = new Cliente
                {
                    Nome = "Carlos Silva",
                    CPF = "11122233344",
                    Email = "carlos@ex.com",
                    Senha = "1234",
                    Telefone = "199999999"
                };

                ctx.Funcionarios.Add(admin);
                ctx.TiposQuarto.Add(tipo);
                ctx.Quartos.Add(quarto);
                ctx.Clientes.Add(cliente);
                ctx.SaveChanges();
            }
        }
    }
}
