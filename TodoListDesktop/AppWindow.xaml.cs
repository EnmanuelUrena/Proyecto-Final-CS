using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
        {
            InitializeComponent();
            Label_Name.Content = MainWindow.logedUser.Name;
            CargarLista();
        }

        private void CargarLista()
        {
            var UserList = MainWindow.logedUser.Lists.ToList();
            List<string> list = new List<string>();
            foreach (var item in UserList)
            {
                list.Add(item.Name);
            }
            list.Reverse();
            ListBox_MyList.ItemsSource = list;
        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Home";
            Label_WindowSection.Content = "My Lists";
        }

        private void Btn_Products_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Products";
            Label_WindowSection.Content = "All Products";
            
        }

        private void Btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Profile";
            Label_WindowSection.Content = "My Profile";
        }

        private void Btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Settings";
            Label_WindowSection.Content = "My Settings";
        }
        private void Btn_AddList_Click(object sender, RoutedEventArgs e)
        {
            var name = TextBox_NewList.Text;
            AñadirLista(name);
            TextBox_NewList.Text = "";
            CargarLista();
        }

        private void AñadirLista(string name)
        {
            var newList = new Lists();
            newList.Name = name;
            newList.Users = MainWindow.logedUser;
            MainWindow.db.Lists.Add(newList);
            MainWindow.db.SaveChanges();
        }
    }
}
