using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.MAUI.Models
{
    public static class ContactRepository
    {
        public static async Task<List<Contact>> GetContactsAsync()
        {
            // API URL
            string apiUrl = "https://66cc6073a4dd3c8a71b7664b.mockapi.io/Contacts";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Send a GET request to the API and wait for the response
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // If the response is successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseData = await response.Content.ReadAsStringAsync();

                    // Convert the JSON response into a list of Contact objects
                    List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(responseData);

                    return contacts;
                }
                else
                {
                    // Handle the error (e.g., return an empty list or throw an exception)
                    return new List<Contact>();
                }
            }
        }


        public static async Task<Contact> GetContactByIdAsync(int contactId)
        {
            // Call the service method to get the contact from the API
            var contact = await ContactService.GetContactByIdAsync(contactId);

            // Return the contact if found, otherwise return null
            return contact;
        }

        public static async Task UpdateContact(int contactId, Contact contact) 
        {
            if (contactId != contact.ContactId) return;

            // Call the service method to update the contact via the API
            await ContactService.UpdateContactAsync(contactId, contact);
        }

        public static async Task AddContact(Contact contact)
        {
            // Call the service method to add the contact via the API
            await ContactService.AddContactAsync(contact);
        }

        public static async Task DeleteContact(int contactId)
        {
            // Call the service method to delete the contact via the API
            await ContactService.DeleteContactAsync(contactId);
        }







    }
}
