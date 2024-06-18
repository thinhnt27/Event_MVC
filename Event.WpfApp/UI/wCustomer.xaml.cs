using Event.Business.Category;
using Event.Data.Models;
using Microsoft.IdentityModel.Tokens;
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

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerId.Text.IsNullOrEmpty())
                {
                    var customer = new Customer()
                    {
                        CustomerName = CustomerName.Text,
                        Password = Password.Text,
                        Email = Email.Text,
                        Address = Address.Text,
                        Phone = Phone.Text,
                    };

                    var result = await _bussiness.Save(customer);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var item = await _bussiness.GetById(int.Parse(CustomerId.Text));

                    var customer = item.Data as Customer;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    customer.CustomerName = CustomerName.Text;
                    customer.Password = Password.Text;
                    customer.Email = Email.Text;
                    customer.Address = Address.Text;
                    customer.Phone = Phone.Text;

                    var result = await _bussiness.Update(customer);
                    MessageBox.Show(result.Message, "Update");

                }
                CustomerId.Text = string.Empty;
                CustomerName.Text = string.Empty;
                Password.Text = string.Empty;
                Email.Text = string.Empty;
                Address.Text = string.Empty;
                Phone.Text = string.Empty;
                this.LoadGrdCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string customerId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(customerId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _bussiness.DeleteById(int.Parse(customerId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCustomer();
                }
            }
        }

        private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var customerResult = await _bussiness.GetById(item.CustomerId);

                        if (customerResult.Status > 0 && customerResult.Data != null)
                        {
                            item = customerResult.Data as Customer;
                            CustomerId.Text = item.CustomerId.ToString();
                            CustomerName.Text = item.CustomerName;
                            Password.Text = item.Password;
                            Email.Text = item.Email;
                            Address.Text = item.Address;
                            Phone.Text = item.Phone;
                        }
                    }
                }
            }
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
