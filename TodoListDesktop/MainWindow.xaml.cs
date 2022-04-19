using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObject;

namespace TodoListDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TodoListDBEntities db = new TodoListDBEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario registrar = new RegistrarUsuario();
            registrar.Show();
            Close();
        }

        private void Btn_IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            var username = TextBox_Username.Text;
            var password = PasswordBox_Password.Password;
            bool authenticated = IniciarSesion(username, password);
            if (authenticated)
            {

                AppWindow appWindow = new AppWindow();
                Close();
                appWindow.Show();
            }
            else
            {
                Label_Error.Content = "Usuario o Contraseña incorrectos";
            }
        }

        private bool IniciarSesion(string username, string password)
        {
            bool result = false;
            try
            {
                var user = db.Users.Single(b => b.Username == username && b.Password == password);
                if (user != null)
                    result = true;
            }
            catch
            {
                return false;
            }
            return result;

        }
    }
}
