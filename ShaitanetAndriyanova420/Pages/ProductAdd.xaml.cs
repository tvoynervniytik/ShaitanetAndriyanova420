using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Page
    {
        public Product Product { get; set; }
        public Product contextProduct;
        public ProductAdd(Product product)
        {
            InitializeComponent();
            DataContext = contextProduct = product;
            if (contextProduct.ID != 0)
            {
                NameTb.Text = contextProduct.Name;

                costTb.Text = contextProduct.Cost.ToString();
                
                descTb.Text = contextProduct.Description.ToString();

            }
            this.DataContext = this;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductPage());
        }
        private void photoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = " .png; .jpg; .jpeg | *.png; *.jpg; *.jpeg" };
                if (openFileDialog.ShowDialog().GetValueOrDefault())
                {
                    contextProduct.Image = File.ReadAllBytes(openFileDialog.FileName);
                    img.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (NameTb.Text != "" && descTb.Text != "" && costTb.Text != "")
            {
                contextProduct.Name = NameTb.Text;
                contextProduct.Description = descTb.Text;
                contextProduct.Cost = int.Parse(costTb.Text);
                if (contextProduct.ID == 0)
                    DBConnection.Shaitanet.Product.Add(contextProduct);
                DBConnection.Shaitanet.SaveChanges();

                MessageBox.Show("Вы внесли изменения в список продукции", "", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ProductPage());
            }
        }
    }
}
