using Contacts.MAUI.Models;
using System.Collections.ObjectModel;
using Contact = Contacts.MAUI.Models.Contact;


namespace Contacts.MAUI.Views;

public partial class ContactsPage : ContentPage
{
    public ContactsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Clear the SearchBar text and load contacts
        SearchBar.Text = string.Empty;
        await LoadContactsAsync();
    }

    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listContacts.SelectedItem != null)
        {
            var selectedContact = (Contact)listContacts.SelectedItem;
            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={selectedContact.ContactId}");
        }
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private async void DeleteContact_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;

        if (contact != null)
        {
            // Call delete method from ContactService
            await ContactService.DeleteContactAsync(contact.ContactId);

            // Refresh the contact list
            await LoadContactsAsync();
        }
    }

    private async Task LoadContactsAsync()
    {
        // Fetch contacts from the API
        var contacts = await ContactService.GetAllContactsAsync();

        // Bind contacts to the ListView
        listContacts.ItemsSource = new ObservableCollection<Contact>(contacts);
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filterText = ((SearchBar)sender).Text;

        // Fetch filtered contacts from the API
        var contacts = await ContactService.SearchContactsAsync(filterText);

        // Bind filtered contacts to the ListView
        listContacts.ItemsSource = new ObservableCollection<Contact>(contacts);
    }
}
