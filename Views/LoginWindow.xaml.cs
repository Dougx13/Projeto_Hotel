using System.Linq;
using System.Windows;
using HotelAdminWpf8.Data;

namespace HotelAdminWpf8.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Login de ADMIN
        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            using var ctx = new HotelContext();
            var func = ctx.Funcionarios
                .FirstOrDefault(f => f.Usuario == txtUsuario.Text && f.Senha == txtSenha.Password);

            if (func != null)
            {
                MessageBox.Show("Login de administrador efetuado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                new AdminWindow(func.Nome).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos para o administrador.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Login de CLIENTE
        private void BtnCliente_Click(object sender, RoutedEventArgs e)
        {
            using var ctx = new HotelContext();
            // Aqui txtUsuario é usado como campo de e-mail do cliente
            var cliente = ctx.Clientes
                .FirstOrDefault(c => c.Email == txtUsuario.Text && c.Senha == txtSenha.Password);

            if (cliente != null)
            {
                MessageBox.Show($"Bem-vindo, {cliente.Nome}!", "Login realizado", MessageBoxButton.OK, MessageBoxImage.Information);
                var janelaCliente = new ReservaClienteWindow(cliente);
                janelaCliente.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Email ou senha incorretos.", "Erro de login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Botão de cadastro
        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var cadastroWindow = new CadastroWindow();
            cadastroWindow.Show();
            Close();
        }
    }
}
