using Event.Business.Category;
using Event.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace Event.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wPayment.xaml
    /// </summary>
    public partial class wPayment : Window
    {   
        public List<Payment> payments { get; set; }

        public Payment payment { get; set; }
        public IPaymentBusiness _paymentBusiness { get; set; }

        public  wPayment()
        {
            InitializeComponent();
            payments = new List<Payment>();
            _paymentBusiness ??= new PaymentBusiness();
            this.Load();
     
        }

  /*      private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(dvg_Payments.SelectedItem is Payment selectedPayment)
            {
                txt_PaymentId.Text = selectedPayment.PaymentId.ToString();
                txt_PaymentDate.Text = selectedPayment.PaymentDate.ToString();
                txt_PaymentType.Text = selectedPayment.PaymentType;
                txt_RegistrationId.Text = selectedPayment.RegistrationId.ToString();
                txt_TicketId.Text = selectedPayment.TicketId.ToString();
                txt_TicketQuantity.Text = selectedPayment.TicketQuantity.ToString();
                txt_Status.Text = selectedPayment.Status.ToString();
            }
        }*/



        private async void btn_Save_Click(object sender, RoutedEventArgs e)
        {   
            var id = int.Parse(txt_PaymentId.Text);
            var item = await _paymentBusiness.GetById(id);
            try
            {
                if ((item.Data as Payment) is null)
                {
                    await _paymentBusiness.Save(new Payment()
                    {
                        AmountPaid = decimal.Parse(txt_AmountPaid.Text),
                        PaymentDate = DateTime.Parse(txt_PaymentDate.Text),
                        PaymentType = txt_PaymentType.Text,
                        RegistrationId = int.Parse(txt_RegistrationId.Text),
                        TicketId = int.Parse(txt_TicketId.Text),
                        Status = true,
                        TicketQuantity = int.Parse(txt_TicketQuantity.Text),
                    });
                    System.Windows.MessageBox.Show("Save successfully!");
                }
                else
                {
                    var updatePayment = item.Data as Payment;
                    updatePayment.Status = bool.Parse(txt_Status.Text);
                    updatePayment.TicketQuantity = int.Parse(txt_TicketQuantity.Text);
                    updatePayment.TicketId = int.Parse(txt_TicketId.Text);
                    updatePayment.PaymentDate = DateTime.Parse(txt_PaymentDate.Text);
                    updatePayment.PaymentType = txt_PaymentType.Text;
                    updatePayment.RegistrationId = int.Parse(txt_RegistrationId.Text);
                    updatePayment.AmountPaid = int.Parse(txt_AmountPaid.Text);
                    await _paymentBusiness.Update(updatePayment);
                    System.Windows.MessageBox.Show("Update successfully!");
                }
            } catch(Exception a)
            {
                System.Windows.MessageBox.Show(a.Message.ToString());
            }
            txt_AmountPaid.Clear();
            txt_PaymentType.Clear();
            txt_RegistrationId.Clear();
            txt_Status.Clear();
            txt_PaymentId.Clear();
            txt_PaymentDate.Clear();
            txt_TicketId.Clear();
            txt_TicketQuantity.Clear();
            this.Load();
            
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private async void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
          
            
        }
        private async void Load()
        {
            var objects = await _paymentBusiness.GetAll();
            if (objects.Data is null)
            {
                dtaGridPayments.ItemsSource = new List<Payment>();
            }
            else
            {
                dtaGridPayments.ItemsSource = (objects.Data as List<Payment>);
           
            }
        }


        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        
        

        private bool IsTextNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+"); 
            return !regex.IsMatch(text);
        }

        private async void dtaGridPayments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Payment;
                    if (item != null)
                    {
                        var paymentResult = await _paymentBusiness.GetById(item.PaymentId);

                        if (paymentResult.Status > 0 && paymentResult.Data != null)
                        {
                            var payment = paymentResult.Data as Payment;
                            txt_AmountPaid.Text = payment.AmountPaid.ToString();
                            txt_PaymentDate.Text = payment.PaymentDate.ToString();
                            txt_PaymentId.Text = payment.PaymentId.ToString();
                            txt_PaymentType.Text = payment.PaymentType.ToString();
                            txt_RegistrationId.Text = payment.RegistrationId.ToString();
                            txt_Status.Text = payment.Status.ToString();
                            txt_TicketId.Text = payment.TicketId.ToString();
                            txt_TicketQuantity.Text = payment.TicketQuantity.ToString();
                        }
                    }
                }
            }
        }

        private async void bttn_Delete_Click_1(object sender, RoutedEventArgs e)
        {
            if (!txt_PaymentId.Text.IsNullOrEmpty())
            {

                MessageBoxResult result = System.Windows.MessageBox.Show(
                    "Are you sure you want to delete this item?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );


                if (result == MessageBoxResult.Yes)
                {

                    await _paymentBusiness.DeleteById(int.Parse(txt_PaymentId.Text));
                    System.Windows.MessageBox.Show("Payment deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    txt_AmountPaid.Clear();
                    txt_PaymentType.Clear();
                    txt_RegistrationId.Clear();
                    txt_Status.Clear();
                    txt_PaymentId.Clear();
                    txt_PaymentDate.Clear();
                    txt_TicketId.Clear();
                    txt_TicketQuantity.Clear();
                    this.Load();
                }
            }
        }
    }
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        public int? RegistrationId { get; set; }

        public int? TicketId { get; set; }

        public int? TicketQuantity { get; set; }

        public DateOnly? PaymentDate { get; set; }

        public decimal? AmountPaid { get; set; }

        public bool? Status { get; set; }

        public string PaymentType { get; set; }
    }
}
