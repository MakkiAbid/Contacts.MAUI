using Contacts.MAUI.Models;
using System;
using System.Threading.Tasks;
using Contact = Contacts.MAUI.Models.Contact;

namespace Contacts.MAUI.Views
{
    public partial class AddContactPage : ContentPage
    {
        public AddContactPage()
        {
            InitializeComponent();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void contactCtrl_OnSave(object sender, EventArgs e)
        {
            try
            {
                var newContact = new Contact
                {
                    Name = contactCtrl.Name,
                    Email = contactCtrl.Email,
                    Phone = contactCtrl.Phone,
                    Address = contactCtrl.Address
                };

                await ContactService.AddContactAsync(newContact);
                await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void contactCtrl_OnCancel(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
        }

        private async void contactCtrl_OnError(object sender, string e)
        {
            await DisplayAlert("Error", e, "OK");
        }
    }
}
