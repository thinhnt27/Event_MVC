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
    /// Interaction logic for wEvent.xaml
    /// </summary>
    public partial class wEvent : Window
    {
        private readonly EventBusiness _business;
        public wEvent()
        {
            InitializeComponent();
            this._business ??= new EventBusiness();
            this.LoadGrdEvents();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEventId.Text))
                {
                    txtEventId.Text = "0";
                }
                var item = await _business.GetById(int.Parse(txtEventId.Text));

                if (item.Data == null)
                {
                    var Event = new Events()
                    {
                        EventId = int.Parse(txtEventId.Text),
                        //EventType = txtEventType.Text,
                        //Price = decimal.Parse(txtPrice.Text),
                        //AvailableQuantity = int.Parse(txtAvailableQuantity.Text)
                    };

                    var result = await _business.Save(Event);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var Event = item.Data as Events;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    //Event.EventId = int.Parse(txtEventId.Text);
                    //Event.EventType = txtEventType.Text;
                    //Event.Price = decimal.Parse(txtPrice.Text);
                    //Event.AvailableQuantity = int.Parse(txtAvailableQuantity.Text);

                    var result = await _business.Update(Event);
                    MessageBox.Show(result.Message, "Update");
                }

                txtEventId.Text = string.Empty;
                txtEventId.Text = string.Empty;
                txtEventType.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtAvailableQuantity.Text = string.Empty;
                this.LoadGrdEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtEventId.Text = "";
            txtEventId.Text = "";
            txtEventType.Text = "";
            txtPrice.Text = "";
            txtAvailableQuantity.Text = "";
        }

        private async void grdEvent_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            var Event = grdEvent.SelectedItem as Events;

            //if (Event != null)
            //{
            //    txtEventId.Text = Event.EventId.ToString();
            //    txtEventId.Text = Event.EventId.ToString();
            //    txtEventType.Text = Event.EventType;
            //    txtPrice.Text = Event.Price.ToString();
            //    txtAvailableQuantity.Text = Event.AvailableQuantity.ToString();
            //}
        }

        private async void grdEvent_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var Event = grdEvent.SelectedItem as Events;

            if (Event != null)
            {
                var result = _business.DeleteById(Event.EventId);

                if (result.Status > 0)
                {
                    MessageBox.Show("Event deleted successfully");
                    this.LoadGrdEvents();
                }
                else
                {
                    MessageBox.Show("Failed to delete Event");
                }
            }
        }
        private async void LoadGrdEvents()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdEvent.ItemsSource = result.Data as List<Events>;
            }
            else
            {
                grdEvent.ItemsSource = new List<Events>();
            }
        }
    }
}
