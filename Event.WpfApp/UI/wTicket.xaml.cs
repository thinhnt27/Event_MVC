using Event.Business.Category;
using Event.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
    /// Interaction logic for wTicket.xaml
    /// </summary>
    public partial class wTicket : Window
    {
        private readonly TicketBusiness _business;

        public wTicket()
        {
            InitializeComponent();
            this._business ??= new TicketBusiness();
            this.LoadGrdTickets();
        }


        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTicketId.Text))
                {
                    txtTicketId.Text = "0";
                }
                var item = await _business.GetById(int.Parse(txtTicketId.Text));

                if (item.Data == null)
                {
                    var ticket = new Ticket()
                    {
                        EventId = int.Parse(txtEventId.Text),
                        TicketType = txtTicketType.Text,
                        Price = decimal.Parse(txtPrice.Text),
                        AvailableQuantity = int.Parse(txtAvailableQuantity.Text)
                    };

                    var result = await _business.Save(ticket);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var ticket = item.Data as Ticket;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    ticket.EventId = int.Parse(txtEventId.Text);
                    ticket.TicketType = txtTicketType.Text;
                    ticket.Price = decimal.Parse(txtPrice.Text);
                    ticket.AvailableQuantity = int.Parse(txtAvailableQuantity.Text);

                    var result = await _business.Update(ticket);
                    MessageBox.Show(result.Message, "Update");
                }

                txtTicketId.Text = string.Empty;
                txtEventId.Text = string.Empty;
                txtTicketType.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtAvailableQuantity.Text = string.Empty;
                this.LoadGrdTickets();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtTicketId.Text = "";
            txtEventId.Text = "";
            txtTicketType.Text = "";
            txtPrice.Text = "";
            txtAvailableQuantity.Text = "";
        }

        private async void grdTicket_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            var ticket = grdTicket.SelectedItem as Ticket;

            if (ticket != null)
            {
                txtTicketId .Text = ticket.TicketId.ToString();
                txtEventId.Text = ticket.EventId.ToString();
                txtTicketType.Text = ticket.TicketType;
                txtPrice.Text = ticket.Price.ToString();
                txtAvailableQuantity.Text = ticket.AvailableQuantity.ToString();
            }
        }

        private async void grdTicket_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var ticket = grdTicket.SelectedItem as Ticket;

            if (ticket != null)
            {
                var result = _business.DeleteById(ticket.TicketId);

                if (result.Status > 0)
                {
                    MessageBox.Show("Ticket deleted successfully");
                    this.LoadGrdTickets();
                }
                else
                {
                    MessageBox.Show("Failed to delete ticket");
                }
            }
        }
        private async void LoadGrdTickets()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdTicket.ItemsSource = result.Data as List<Ticket>;
            }
            else
            {
                grdTicket.ItemsSource = new List<Ticket>();
            }
        }

    }
}
