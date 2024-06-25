using Castle.Core.Resource;
using Event.Business.Category;
using Event.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
                    var _event = new Events()
                    {
                        EventName = txtEventName.Text,
                        EventDate = dpEventDate.SelectedDate,
                        EventDescription = txtEventDescription.Text,
                        EventTypeId = int.TryParse(txtEventTypeId.Text, out var eventTypeId) ? eventTypeId : (int?)null,
                        NumberTickets = int.TryParse(txtNumberTickets.Text, out var numberTickets) ? numberTickets : (int?)null,
                        Sponsor = txtSponsor.Text,
                        SubjectCode = txtSubjectCode.Text,
                        Location = txtLocation.Text
                    };

                    var result = await _business.Save(_event);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var item = await _business.GetById(int.Parse(txtEventId.Text));
                    var _event = item.Data as Events;

                    _event.EventName = txtEventName.Text;
                    _event.EventDate = dpEventDate.SelectedDate;
                    _event.EventDescription = txtEventDescription.Text;
                    _event.EventTypeId = int.TryParse(txtEventTypeId.Text, out var eventTypeId) ? eventTypeId : (int?)null;
                    _event.NumberTickets = int.TryParse(txtNumberTickets.Text, out var numberTickets) ? numberTickets : (int?)null;
                    _event.Sponsor = txtSponsor.Text;
                    _event.SubjectCode = txtSubjectCode.Text;
                    _event.Location = txtLocation.Text;

                    var result = await _business.Update(_event);
                    MessageBox.Show(result.Message, "Update");
                }
                ClearFields();
                this.LoadGrdEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtEventId.Text = string.Empty;
            txtEventName.Text = string.Empty;
            dpEventDate.SelectedDate = null;
            txtEventDescription.Text = string.Empty;
            txtEventTypeId.Text = string.Empty;
            txtNumberTickets.Text = string.Empty;
            txtSponsor.Text = string.Empty;
            txtSubjectCode.Text = string.Empty;
            txtLocation.Text = string.Empty;
        }


        private void grdEvent_MouseDouble_Click(object sender, MouseButtonEventArgs e)
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

        private void grdEvent_ButtonDelete_Click(object sender, RoutedEventArgs e)
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
