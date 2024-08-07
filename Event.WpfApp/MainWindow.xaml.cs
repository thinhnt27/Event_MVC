﻿using Event.WpfApp.UI;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Event.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_wPayment_Click(object sender, RoutedEventArgs e)
        {
            var p = new wRegistration();
            p.Owner = this;
            p.Show();
        }
        private async void Open_wRegistration_Click(object sender, RoutedEventArgs e)
        {
            var p = new wPayment();
            p.Owner = this;
            p.Show();
        }
        private async void Open_wCustomer_Click(object sender, RoutedEventArgs e)
        {
            var c = new wCustomer();
            c.Owner = this;
            c.Show();
        }

        private void Open_wTicket_Click(object sender, RoutedEventArgs e)
        {
            var p = new wTicket();
            p.Owner = this;
            p.Show();
        }
    }
}