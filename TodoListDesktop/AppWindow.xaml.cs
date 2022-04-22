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
        public static Lists selectedList = new Lists();
        public AppWindow()
        {
            InitializeComponent();
            Label_Name.Content = MainWindow.logedUser.Name;
            CargarLista();
        }

        private void CargarLista()
        {
            ListBox_MyList.ItemsSource = null;
            var UserList = MainWindow.logedUser.Lists;
            ListBox_MyList.ItemsSource = UserList;
        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Home";
            Label_WindowSection.Content = "My Lists";
            TextBox_NewList.IsEnabled = true;
            Btn_AddList.IsEnabled = true;
            TextBox_NewList.Visibility = Visibility.Visible;
            Btn_AddList.Visibility = Visibility.Visible;
            CargarLista();
        }

        private void Btn_Products_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Products";
            Label_WindowSection.Content = "All Products";
            ListBox_MyList.ItemsSource = null;
            TextBox_NewList.IsEnabled = false;
            Btn_AddList.IsEnabled = false;
            TextBox_NewList.Visibility = Visibility.Collapsed;
            Btn_AddList.Visibility = Visibility.Collapsed;
            CargarProductos();
        }

        private void CargarProductos()
        {
            var products = MainWindow.db.Product.ToList();
            ListBox_MyList.ItemsSource = products;
        }

        private void Btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Profile";
            Label_WindowSection.Content = "My Profile";
            ListBox_MyList.ItemsSource = null;
            TextBox_NewList.IsEnabled = false;
            Btn_AddList.IsEnabled = false;
            TextBox_NewList.Visibility = Visibility.Collapsed;
            Btn_AddList.Visibility = Visibility.Collapsed;
        }

        private void Btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            Label_WindowTitle.Content = "Settings";
            Label_WindowSection.Content = "My Settings";
            ListBox_MyList.ItemsSource = null;
            TextBox_NewList.IsEnabled = false;
            Btn_AddList.IsEnabled = false;
            TextBox_NewList.Visibility = Visibility.Collapsed;
            Btn_AddList.Visibility = Visibility.Collapsed;
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

        private void ListBox_MyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            var curItemIndex = ListBox_MyList.SelectedIndex;
            var UserList = MainWindow.logedUser.Lists.ToList();
            try
            {
                selectedList = UserList[curItemIndex];
                Products products = new Products();
                products.Show();
            }
            catch
            {

            }
        }

        private void Btn_DeleteList_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            var list = cmd.DataContext;
            bool erased = false;
            try
            {
                MainWindow.db.Lists.Remove((Lists)list);
                MainWindow.db.SaveChanges();
                erased = true;
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar una lista con productos, primero elimine los productos dentro de la lista", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (erased)
            {
                CargarLista();
            }
        }

        private void Btn_UpdateList_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            bool updated = false;
            try
            {
                var updateList = (Lists)cmd.DataContext;
                var list = MainWindow.db.Lists.Single(a => a.ListID == updateList.ListID);
                list.Name = TextBox_NewList.Text;
                MainWindow.db.SaveChanges();
                updated = true;
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha podido modificar la lista", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (updated)
            {
                CargarLista();
            }
        }
    }
}
