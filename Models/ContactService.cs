using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.MAUI.Models
{
    public static class ContactService
    {
        private static readonly string apiUrl = "https://66cc6073a4dd3c8a71b7664b.mockapi.io/Contacts";

        // Method to get all contacts from the API
        public static async Task<List<Contact>> GetAllContactsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Contact>>(responseData);
                }
                return new List<Contact>();
            }
        }

        // Method to get a contact by ID from the API
        public static async Task<Contact> GetContactByIdAsync(int contactId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{contactId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Contact>(responseData);
                }
                return null;
            }
        }

        // Method to add a new contact via the API
        public static async Task AddContactAsync(Contact contact)
        {
            using (HttpClient client = new HttpClient())
            {
                // Convert the contact object to JSON
                string json = JsonConvert.SerializeObject(contact);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a POST request to the API
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Optionally, handle the response (e.g., throw an exception if not successful)
                response.EnsureSuccessStatusCode();
            }
        }

        // Method to delete a contact via the API
        public static async Task DeleteContactAsync(int contactId)
        {
            using (HttpClient client = new HttpClient())
            {
                // Send a DELETE request to the API
                HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}/{contactId}");

                // Optionally, handle the response (e.g., throw an exception if not successful)
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task UpdateContactAsync(int contactId, Contact contact)
        {
            using (HttpClient client = new HttpClient())
            {
                // Convert the contact object to JSON
                string json = JsonConvert.SerializeObject(contact);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a PUT request to the API to update the contact
                HttpResponseMessage response = await client.PutAsync($"{apiUrl}/{contactId}", content);

                // Optionally, handle the response (e.g., throw an exception if not successful)
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task<List<Contact>> SearchContactsAsync(string filterText)
        {
            // Fetch all contacts from the API
            var contacts = await ContactService.GetAllContactsAsync();

            // Filter by Name
            var filteredContacts = contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) &&
                                                       x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();

            // If no results, filter by Email
            if (filteredContacts == null || filteredContacts.Count <= 0)
            {
                filteredContacts = contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) &&
                                                       x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return filteredContacts;
            }

            // If no results, filter by Phone
            if (filteredContacts == null || filteredContacts.Count <= 0)
            {
                filteredContacts = contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) &&
                                                       x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return filteredContacts;
            }

            // If no results, filter by Address
            if (filteredContacts == null || filteredContacts.Count <= 0)
            {
                filteredContacts = contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) &&
                                                       x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }

            return filteredContacts;
        }

    }


}
