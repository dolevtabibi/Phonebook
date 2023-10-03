using Phonebook_APP.CRUDService;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Phonebook_APP
{
    public partial class EditPersonForm : Form
    {
        private EntityState entityState;
        private readonly string connectionString;
        private readonly PersonService client;
        private readonly int p_id;

        public EditPersonForm(EntityState state, Person personData)
        {
            InitializeComponent();
            try
            {
                client = new PersonService();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, $"Error initializing client:{ ex.Message}");
                // Log the exception if necessary
            }

            this.entityState = state;
            InitializeFormData(personData);
            this.p_id = personData.P_Id;
            connectionString = ConfigurationManager.ConnectionStrings["Phonebook_APP.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString;
        }

        private void InitializeFormData(Person personData)
        {
            firstNameTextBox.Text = personData.FirstName;
            lastNameTextBox.Text = personData.LastName;
            addressTextBox.Text = personData.Address;

            if (personData.DateOfBirth.HasValue)
            {
                dateTextBox.Text = personData.DateOfBirth.Value.ToString("MM/dd/yyyy");
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Invalid date format. Please enter the date in MM/dd/yyyy format.");
            }

            idTextBox.Text = personData.Id.ToString();
            cityTextBox.Text = personData.City;

            if (personData.Picture != null && personData.Picture.Length > 0)
            {
                personPictureBox.Image = ByteToImage(personData.Picture);
            }
            else
            {
                personPictureBox.Image = Properties.Resources.defaultPicture;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            personGridDataBindingSource.EndEdit();

            if (entityState == EntityState.Changed) // Update person
            {
                UpdatePerson();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            else if (entityState == EntityState.Added) // New person
            {
                if (AddNewPerson()) // Check if the new person was added successfully
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Show a message indicating the error, if necessary
                }
            }
        }


        private void UpdatePerson()
        {
            if (ValidateInputs())
            {
                if (TryParseDate(dateTextBox.Text.Trim(), out DateTime parsedDate))
                {
                    int.TryParse(idTextBox.Text, out int personId);
                    string firstName = firstNameTextBox.Text;
                    string lastName = lastNameTextBox.Text;
                    string address = addressTextBox.Text;
                    string city = cityTextBox.Text;

                    byte[] picture = ImageToByteArray(personPictureBox.Image);

                    // Pass P_Id as a parameter to your Update method
                    Person updatedPerson = new Person
                    {
                        P_Id = this.p_id, // P_Id identifies the person in the database
                        FirstName = firstName,
                        LastName = lastName,
                        Id = personId,
                        DateOfBirth = parsedDate,
                        Address = address,
                        City = city,
                        Picture = picture
                    };

                    try
                    {
                        if (client.Update(updatedPerson))
                        {
                            MetroFramework.MetroMessageBox.Show(this, "Person information updated successfully");
                            return; // Indicate success and exit method
                        }
                        else
                        {
                            MetroFramework.MetroMessageBox.Show(this, "Failed to update person information. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions specific to the update operation
                        MetroFramework.MetroMessageBox.Show(this, $"Error updating person information: {ex.Message}");
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Invalid input. Please enter valid values for Person Id and Date of Birth.");
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Invalid input. Please check your input values and try again.");
            }
        }



        private bool AddNewPerson()
        {
            if (ValidateInputs())
            {
                if (TryParseDate(dateTextBox.Text.Trim(), out DateTime parsedDate))
                {
                    byte[] picture = ImageToByteArray(personPictureBox.Image);

                    Person personToAdd = new Person
                    {
                        FirstName = firstNameTextBox.Text,
                        LastName = lastNameTextBox.Text,
                        Address = addressTextBox.Text,
                        City = cityTextBox.Text,
                        Id = int.TryParse(idTextBox.Text, out int id) ? id : 0,
                        DateOfBirth = parsedDate,
                        Picture = picture
                    };

                    try
                    {
                        client.Insert(personToAdd);
                        MetroFramework.MetroMessageBox.Show(this, "New Person information Added successfully");
                        return true; // Indicate success
                    }
                    catch (Exception ex)
                    {
                        MetroFramework.MetroMessageBox.Show(this, $"Error adding new person: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // Indicate failure
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Invalid date format. Please enter the date in MM/dd/yyyy format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Indicate failure
                }
            }

            return false; // Indicate failure if inputs are not valid
        }



        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(firstNameTextBox.Text) || string.IsNullOrEmpty(lastNameTextBox.Text) ||
                string.IsNullOrEmpty(addressTextBox.Text) || string.IsNullOrEmpty(cityTextBox.Text))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Handle delete operation if needed
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg|PNG|*.png", ValidateNames = true })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    personPictureBox.Image=Image.FromFile(ofd.FileName);
                    Person obj = personBindingSource.Current as Person;
                    if(obj != null)
                    {
                        obj.Picture = ImageToByteArray(personPictureBox.Image);
                    }
                }
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private Image ByteToImage(byte[] byteArray)
        {
            if (byteArray == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private bool TryParseDate(string input, out DateTime parsedDate)
        {
            string[] dateFormats = { "MM/dd/yyyy", "ddMMyyyy", "MMddyyyy", "ddMMyyyy", "MMddyy", "MMyyyydd", "MMddyyyy" };
            return DateTime.TryParseExact(input, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
        }


    }
}
