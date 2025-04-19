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
using Microsoft.Win32;
using ShaitanetAndriyanova420.DB;

namespace ShaitanetAndriyanova420.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public List<Product> products {  get; set; }

        public ProductPage()
        {
            InitializeComponent();
            Refresh();
            this.DataContext = products;
        }

        public static Window GetParentWindow(DependencyObject child)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            if (parentObject is Window parentWindow)
            {
                return parentWindow;
            }

            return GetParentWindow(parentObject);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = GetParentWindow((DependencyObject)sender);

            if (parentWindow != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    parentWindow.Close();
                }
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = GetParentWindow((DependencyObject)sender);

            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new AuthorizationPage());
            }
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();

        }
        public void Refresh()
        {
            products = new List<Product>(DBConnection.Shaitanet.Product);
            string text = searchTb.Text.ToLower().Trim();
            products = products.Where(i => i.Name.ToLower().Contains(text)).ToList();
            productsLv.ItemsSource = products;
        }

        private void productsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = productsLv.SelectedItem as Product;
            NavigationService.Navigate(new ProductAdd(product));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductAdd(new Product()));
        }
      
    }
}
