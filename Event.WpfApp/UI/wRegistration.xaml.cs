using Event.Business.Category;
using Event.Data.DAO;
using Event.Data.Models;
using Microsoft.Extensions.Logging;
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
    public partial class wRegistration : Window
    {
        public readonly IRegistrationBusiness _business;


        public wRegistration()
        {
            InitializeComponent();
            this._business ??= new RegistrationBusiness();
            this.LoadGrdRegistration();
        }

        private void grdRegistration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var registration = await _business.GetById(int.Parse(txtRegistrationId.Text));
                int registrationIdTmp = -1;
                int.TryParse(txtRegistrationId.Text, out registrationIdTmp);
            if (registration.Data == null) {
                var result = new Registration()
                {
                    RegistrationId = registrationIdTmp,
                    EventId = int.Parse(txtEventId.Text),
                    ParticipantName = txtName.Text,
                    ParticipantType = txtType.Text,
                    AttendeeEmail = txtMail.Text,
                    RegistrationPhone = txtPhone.Text,
                    RegistrationDate = DateTime.Now,
                    Checkin = false,
                };
                await _business.Save(result);
                MessageBox.Show("Save success");
             }else
                {
                    var updateRegistration = registration.Data as Registration;
                    updateRegistration.EventId = int.Parse(txtEventId.Text);
                    updateRegistration.ParticipantName = txtName.Text;
                    updateRegistration.ParticipantType = txtType.Text;
                    updateRegistration.AttendeeEmail = txtMail.Text;
                    updateRegistration.RegistrationPhone = txtPhone.Text;
                    updateRegistration.RegistrationDate = DateTime.Now;
                    updateRegistration.Checkin = false;

                await _business.Update(updateRegistration);
                    MessageBox.Show("Update success");
                }
                this.LoadGrdRegistration();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtRegistrationId.Clear();
            txtEventId.Clear();
            txtName.Clear();
            txtType.Clear();
            txtMail.Clear();
            txtPhone.Clear();
        }

        private async void grdRegistration_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var registration = grdRegistration.SelectedItem as Registration;
            if (registration != null)
            {
               var result = MessageBox.Show("Do you want to delete this Registration","Delete",
                   MessageBoxButton.YesNo, MessageBoxImage.Warning);
             if(result == MessageBoxResult.Yes)
            {
                    var deleteResult = await _business.DeleteById(registration.RegistrationId);
                    if(deleteResult.Status > 0)
                    {
                    MessageBox.Show("Delete success");
                    this.LoadGrdRegistration();
                    }
            }else
            {
                MessageBox.Show("Delete fail");
            }

            }
        }

    private async void grdRegistration_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            //DataGrid grd = sender as DataGrid;
            //if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            //{
            //    var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
            //    if (row != null)
            //    {
            //        var item = row.Item as Registration;
            //        if (item != null)
            //        {
            //            var registrationResult = await _business.GetById(item.RegistrationId);

            //            if (registrationResult.Status > 0 && registrationResult.Data != null)
            //            {
            //                item = registrationResult.Data as Registration;
            //                txtRegistrationId.Text = item.RegistrationId.ToString();
            //                txtEventId.Text = item.EventId.ToString();
            //                txtName.Text = item.ParticipantName;
            //                txtType.Text = item.ParticipantType;
            //                txtMail.Text = item.AttendeeEmail;
            //                txtPhone.Text = item.RegistrationPhone;
            //            }
            //        }
            //    }
            //}
            var result = grdRegistration.SelectedItem as Registration; // selectedItem = doubbleClick
        if(result != null)
            {
            txtRegistrationId.Text = result.RegistrationId.ToString();
            txtEventId.Text = result.EventId.ToString();
            txtName.Text = result.ParticipantName;
            txtType.Text = result.ParticipantType;
            txtMail.Text = result.AttendeeEmail;
            txtPhone.Text = result.RegistrationPhone;
            }
        }

        private async void LoadGrdRegistration()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdRegistration.ItemsSource = result.Data as List<Registration>;
            }
            else
            {
                grdRegistration.ItemsSource = new List<Registration>();
            }
        }

    }
}