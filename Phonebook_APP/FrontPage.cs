
using Phonebook_APP.CRUDService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Phonebook_APP
{
    public partial class FrontPage : MetroFramework.Forms.MetroForm
    {
        EntityState objState = EntityState.Unchanged;
        private readonly PersonService _client;
        private List<Person> persons;

        public FrontPage()
        {
            InitializeComponent();
            try
            {
                _client = new PersonService();
                LoadData();
            }
            catch (Exception ex)
            {
                HandleException(ex, "Error initializing client");
            }
        }
        private void HandleException(Exception ex, string message)
        {
            MetroFramework.MetroMessageBox.Show(this, $"{message}: {ex.Message}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadData()
        {
            try
            {
                Person[] personsArray = _client.GetAll();
                persons = personsArray.ToList(); // Convert Person[] to List<Person>
                personBindingSource.DataSource = persons;
            }
            catch (Exception ex)
            {
                HandleException(ex, "Error loading data");
            }
        }

        private void DeletePerson()
        {
            objState = EntityState.Deleted;
            if (MetroFramework.MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Person person = personBindingSource.Current as Person;
                    if (person != null)
                    {
                        bool result = _client.Delete(person.Id);
                        if (result)
                        {
                            objState = EntityState.Unchanged;
                            LoadData(); // Refresh data after deletion
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Error deleting person");
                }
            }
            objState = EntityState.Unchanged;
        }
        private void UpdatePerson()
        {
            Person selectedPerson = personBindingSource.Current as Person;
       
            if (selectedPerson != null)
            {
                EditPersonForm updating = new EditPersonForm(EntityState.Changed, selectedPerson);
                updating.ShowDialog();
                LoadData(); // Refresh data after update
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "No person is selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            objState = EntityState.Unchanged;
        }
        private void NewPerson()
        {
            Person newPerson = new Person();
            EditPersonForm newPresonForm = new EditPersonForm(EntityState.Added, newPerson);
            newPresonForm.ShowDialog();
            LoadData(); // Refresh data after adding a new person
            objState = EntityState.Unchanged;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeletePerson();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdatePerson();
        }


        private void newButton_Click(object sender, EventArgs e)
        {
            NewPerson();
        }

        private void FrontPage_Load(object sender, EventArgs e)
        {
            try
            {
                personBindingSource.DataSource = _client.GetAll();
            }
            catch(Exception ex) 
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = SearchField.Text.Trim().ToLower();

            // Filter persons based on the search keyword across multiple columns
            var filteredPersons = persons
                .Where(person =>
                    person.FirstName.ToLower().Contains(searchKeyword) ||
                    person.LastName.ToLower().Contains(searchKeyword) ||
                    person.Id.ToString().Contains(searchKeyword) ||
                    (person.DateOfBirth.HasValue && person.DateOfBirth.Value.ToString("MM/dd/yyyy").Contains(searchKeyword)) ||
                    person.Address.ToLower().Contains(searchKeyword) ||
                    person.City.ToLower().Contains(searchKeyword)
                )
                .ToList();

            // Update the data source with the filtered list
            personBindingSource.DataSource = filteredPersons;
        }

    }
}
