using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using HotelAdminWpf8.Data;
using HotelAdminWpf8.Models;

namespace HotelAdminWpf8.Views
{
    public partial class CadastroWindow : Window
    {
        private readonly AppDbContext _context;

        public CadastroWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string cpf = txtCPF.Text.Trim();
            string senha = txtSenha.Password.Trim();

            // 🔸 Verifica se há campos vazios
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(cpf) ||
                string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campos obrigatórios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 🔸 Validação básica de e-mail
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Informe um e-mail válido.", "Erro de validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 🔸 Validação básica de CPF (11 dígitos numéricos)
            if (!Regex.IsMatch(cpf, @"^\d{11}$"))
            {
                MessageBox.Show("Informe um CPF válido (somente números, 11 dígitos).", "Erro de validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 🔸 Verifica se já existe um cliente com esse e-mail
                if (_context.Clientes.Any(c => c.Email == email))
                {
                    MessageBox.Show("Este e-mail já está cadastrado.", "Erro de cadastro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 🔸 Cria o novo cliente
                var novoCliente = new Cliente
                {
                    Nome = nome,
                    Email = email,
                    CPF = cpf,
                    Senha = senha
                };

                _context.Clientes.Add(novoCliente);
                _context.SaveChanges();

                MessageBox.Show($"Bem-vindo(a), {nome}! Seu cadastro foi realizado com sucesso.",
                    "Cadastro Concluído", MessageBoxButton.OK, MessageBoxImage.Information);

                // 🔸 Retorna ao login
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar cliente: {ex.Message}",
                    "Erro no sistema", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
