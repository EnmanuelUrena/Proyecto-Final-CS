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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        private Lists ThisList = new Lists();
        private int productquantity;
 

        public Products()
        {
            InitializeComponent();
            ThisList = MainWindow.db.Lists.Find(AppWindow.selectedList.ListID);
            Label_Name.Content = MainWindow.logedUser.Name;
            Label_WindowTitle.Content = ThisList.Name;
            CargarCantidad();
            CargarProductos();
        }

        private void CargarCantidad()
        {
            var list = new List<int>();
            for (int i = 1; i < 100 ; i++)
            {
                list.Add(i);
            }
            ComboBox_Quantity.ItemsSource = list;
        }

        private void Btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var name = TextBox_NewProduct.Text;
            AñadirProducto(name);
            TextBox_NewProduct.Text = "";
            CargarProductos();
        }

        private void AñadirProducto(string name)
        {
            var newProduct = new Product();
            newProduct.Name = name;
            newProduct.Quantity = productquantity;
            newProduct.Lists = ThisList;
            MainWindow.db.Product.Add(newProduct);
            MainWindow.db.SaveChanges();
        }

        private void CargarProductos()
        {
            ListBox_MyProduct.ItemsSource = null;
            ListBox_MyProduct.ItemsSource = ThisList.Product;
        }

        private void Btn_UpdateList_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            bool updated = false;
            try
            {
                var updateProduct = (Product)cmd.DataContext;
                var product = MainWindow.db.Product.Single(a => a.ProductID == updateProduct.ProductID);
                if (TextBox_NewProduct.Text != "")
                {
                    product.Name = TextBox_NewProduct.Text;
                    product.Quantity = productquantity;
                    MainWindow.db.SaveChanges();
                    updated = true;
                }
                else
                {
                    product.Quantity = productquantity;
                    MainWindow.db.SaveChanges();
                    updated = true;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha podido eliminar el producto", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (updated)
            {
                CargarProductos();
            }
        }

        private void Btn_DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            var product = cmd.DataContext;
            bool erased = false;
            try
            {
                MainWindow.db.Product.Remove((Product)product);
                MainWindow.db.SaveChanges();
                erased = true;
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha podido modificar el producto", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (erased)
            {
                CargarProductos();
            }
        }

        private void ComboBox_Quantity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmd = (ComboBox)sender;
            var quantity = cmd.SelectedValue;
            try
            {
               productquantity = Convert.ToInt32(quantity);
               if (cmd.Text != null)
               {
                    cmd.Text = (string)quantity;
               }
            }
            catch 
            {

            }
        }
    }
}
