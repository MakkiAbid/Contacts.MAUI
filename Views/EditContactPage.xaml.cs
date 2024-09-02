namespace Contacts.MAUI.Views;

using Contacts.MAUI.Models;
using Contact = Contacts.MAUI.Models.Contact;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    private Contact contact;
	public EditContactPage()
	{
		InitializeComponent();
	}

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    public string ContactId
    {
        set
        {
            LoadContactAsync(value);
        }
    }

    // Method to load contact details from API
    private async Task LoadContactAsync(string contactId)
    {
        try
        {
            contact = await ContactService.GetContactByIdAsync(int.Parse(contactId));
            if (contact != null)
            {
                contactCtrl.Name = contact.Name;
                contactCtrl.Email = contact.Email;
                contactCtrl.Phone = contact.Phone;
                contactCtrl.Address = contact.Address;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (contact != null)
            {
                contact.Name = contactCtrl.Name;
                contact.Email = contactCtrl.Email;
                contact.Phone = contactCtrl.Phone;
                contact.Address = contactCtrl.Address;

                await ContactService.UpdateContactAsync(contact.ContactId, contact);
                await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void contactCtrl_OnError(object sender, string e)
    {
        await DisplayAlert("Error", e, "OK");
    }
}