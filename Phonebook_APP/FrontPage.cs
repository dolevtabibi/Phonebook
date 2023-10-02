using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phonebook_APP
{
    public partial class FrontPage : Form
    {
        private readonly SqlDataAdapter adapter;
        private readonly DataTable dataTable;
        private readonly string _connectionString;
        public FrontPage()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["Phonebook_APP.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString;
            // Initialize the adapter and dataTable at the class level
            adapter = new SqlDataAdapter("SELECT * FROM dbo.PersonGridData", _connectionString);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
        }
        
        private void Front_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'phonebook_DatabaseDataSet.PersonGridData' table. You can move, or remove it, as needed.
            this.personGridDataTableAdapter.Fill(this.phonebook_DatabaseDataSet.PersonGridData);

        }
        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = SearchField.Text.Trim();

            // Filter the DataTable based on searchKeyword and specified columns
            DataRow[] filteredRows = dataTable.Select($"FirstName LIKE '%{searchKeyword}%' OR " +
                                                      $"LastName LIKE '%{searchKeyword}%' OR " +
                                                      $"CONVERT(Id, 'System.String') LIKE '%{searchKeyword}%' OR " +
                                                      $"CONVERT(DateOfBirth, 'System.String') LIKE '%{searchKeyword}%' OR " +
                                                      $"Address LIKE '%{searchKeyword}%' OR " +
                                                      $"City LIKE '%{searchKeyword}%'");

            // Create a new DataTable with the filtered rows
            DataTable filteredDataTable = dataTable.Clone();
            foreach (DataRow row in filteredRows)
            {
                filteredDataTable.ImportRow(row);
            }

            // Update the DataGridView with the filtered data
            personsTable.DataSource = filteredDataTable;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (personsTable.SelectedRows.Count > 0)
            {
                // Get the ID (primary key) of the selected row
                string selectedId = personsTable.SelectedRows[0].Cells["Id"].Value.ToString();

                // Call a method to delete the record from the database
                DeleteRecordFromDatabase(selectedId);

                // Remove the selected row from the DataGridView
                personsTable.Rows.RemoveAt(personsTable.SelectedRows[0].Index);

                MessageBox.Show("Record deleted successfully!");
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (personsTable.SelectedRows.Count == 1)
            {
                // Get the DataRowView for the selected row
                DataRowView selectedPerson = (DataRowView)personsTable.SelectedRows[0].DataBoundItem;

                // Open the edit form with the selected DataRowView, SqlDataAdapter, and DataTable
                EditPersonForm editForm = new EditPersonForm(selectedPerson);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Update the database
                    personGridDataTableAdapter.Update(phonebook_DatabaseDataSet.PersonGridData);

                    // Update the adapter
                    adapter.Update(phonebook_DatabaseDataSet.PersonGridData);

                    // Clear and refill the DataTable
                    dataTable.Clear();
                    adapter.Fill(dataTable);

                    // Update the DataGridView
                    personsTable.DataSource = dataTable;

                    MessageBox.Show("Data updated successfully!");
                }
            }
            else if (personsTable.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please select only one row to update.");
            }
            else
            {
                MessageBox.Show("Please select a person to update.");
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            EditPersonForm editForm = new EditPersonForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Update the database
                personGridDataTableAdapter.Update(phonebook_DatabaseDataSet.PersonGridData);

                // Update the adapter
                adapter.Update(phonebook_DatabaseDataSet.PersonGridData);

                // Clear and refill the DataTable
                dataTable.Clear();
                adapter.Fill(dataTable);

                // Update the DataGridView
                personsTable.DataSource = dataTable;

                MessageBox.Show("Data updated successfully!");
            }
        }

        private void DeleteRecordFromDatabase(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Construct the delete query with a parameter to prevent SQL injection
                string deleteQuery = "DELETE FROM dbo.PersonGridData WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    // Add parameter to the command
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Record deleted successfully
                        Console.WriteLine("Record deleted from the database. ID: " + id);
                    }
                    else
                    {
                        // No rows were affected, handle the case accordingly
                        Console.WriteLine("No records deleted. Verify the provided primary key value. ID: " + id);
                    }
                }
            }
        }
    }
}
