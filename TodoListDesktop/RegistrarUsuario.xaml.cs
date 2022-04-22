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
using System.Windows.Shapes;
using BusinessObject;

namespace TodoListDesktop
{
    /// <summary>
    /// Interaction logic for RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : Window
    {
        TodoListDBEntities db = new TodoListDBEntities();

        public RegistrarUsuario()
        {
            InitializeComponent();
        }

        private void Btn_RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            MainWindow inicio = new MainWindow();
            try
            {
                if(TextBox_Nombre.Text.Length < 3 || TextBox_Apellido.Text.Length < 3 || TextBox_Email.Text.Length < 6 || TextBox_Username.Text.Length < 5 || PasswordBox_Password.Password.Length < 5)
                {
                    throw new Exception();
                }
                else
                {
                    var name = TextBox_Nombre.Text;
                    var lastname = TextBox_Apellido.Text;
                    var email = TextBox_Email.Text;
                    var username = TextBox_Username.Text;
                    var password = PasswordBox_Password.Password;
                    Registrar(name, lastname, email, username, password);
                    Close();
                    inicio.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede registrar un usuario sin haber llenado los campos requeridos", "Register Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            

        }

        private void Registrar(string name, string lastname, string email, string username, string password)
        {
            var newUser = new Users();
            newUser.Name = name;
            newUser.Lastname = lastname;
            newUser.Email = email;
            newUser.Username = username;
            newUser.Password = password;
            db.Users.Add(newUser);
            db.SaveChanges();

        }
    }
}
