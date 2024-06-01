using Event.Business.Category;
using Event.Data.Models;
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

namespace Event.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        public readonly ICustomerBussiness _bussiness;

        public wCustomer()
        {
            InitializeComponent();
            this._bussiness ??= new CustomerBussiness();
            this.LoadGrdCustomer();
        }

        private void grdCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LoadGrdCustomer()
        {
            var result = await _bussiness.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }

    }
}
