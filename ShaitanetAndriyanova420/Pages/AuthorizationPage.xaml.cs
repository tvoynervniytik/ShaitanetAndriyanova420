﻿using System;
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
using ShaitanetAndriyanova420.DB;

namespace ShaitanetAndriyanova420.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public List<User> Users { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
            Users = new List<User>(DBConnection.Shaitanet.User.ToList());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTb.Text.Trim();
            string password = passwordTb.Password.Trim();
            if (login == "" || password == "")
                MessageBox.Show("Вы не заполнили данные", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (Users.FirstOrDefault(i => i.Login == login && i.Password == password) != null)
                {
                    User user = Users.FirstOrDefault(i => i.Login == login && i.Password == password);
                    MessageBox.Show($"Здравствуйте, {user.Name} {user.Patronymic} {user.Surname}. Ваша роль: {user.UserType.Name}", "",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    if (user.UserType.Name == "Модератор")
                        NavigationService.Navigate(new ProductPage());
                }
                else
                    MessageBox.Show("Польозователь не найден", "", MessageBoxButton.OK, MessageBoxImage.Error);

            }
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
       
    }
}
