using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phonebook_APP
{
    public partial class EditPersonForm : Form
    {
        private readonly DataRowView _personData;
        private readonly SqlDataAdapter _adapter;
        private readonly DataTable _dataTable;
        private byte[] _pictureData;
        private bool newPerson = false;
        private string _originalPersonId;
        private readonly string _connectionString;

        public EditPersonForm()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["Phonebook.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString;
            personPictureBox.Image = Properties.Resources.defaultPicture;
            newPerson = true;
        }
        public EditPersonForm(DataRowView personData)
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["Phonebook_APP.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString;
            _personData = personData;

            // Populate the form controls with personData
            firstNameTextBox.Text = _personData["FirstName"].ToString();
            lastNameTextBox.Text = _personData["LastName"].ToString();
            cityTextBox.Text = _personData["City"].ToString();
            _originalPersonId = idTextBox.Text = _personData["Id"].ToString();
            dateTextBox.Text = ((DateTime)_personData["DateOfBirth"]).ToString("yyyy-MM-dd");
            addressTextBox.Text = _personData["Address"].ToString();
            byte[] pictureData = (byte[])_personData["Picture"];
            this._pictureData = pictureData;
            using (MemoryStream ms = new MemoryStream(_pictureData))
            {
                personPictureBox.Image = Image.FromStream(ms);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (_pictureData == null)
            {
                Bitmap defaultBitmap = Properties.Resources.defaultPicture;
                using (MemoryStream stream = new MemoryStream())
                {
                    defaultBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    _pictureData = stream.ToArray();
                }
            }

            if (!int.TryParse(idTextBox.Text, out int newId))
            {
                MessageBox.Show("Invalid ID format. Please enter a valid numeric ID.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                if (newPerson)
                {
                    string checkIdQuery = "SELECT COUNT(*) FROM dbo.PersonGridData WHERE Id = @Id";
                    using (SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, connection))
                    {
                        checkIdCmd.Parameters.Add("@Id", SqlDbType.Int).Value = newId;
                        int existingIdCount = (int)checkIdCmd.ExecuteScalar();

                        if (existingIdCount > 0)
                        {
                            MessageBox.Show("ID already exists in the database. Please enter a different ID.");
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO dbo.PersonGridData (Picture, FirstName, LastName, Id, City, Address, DateOfBirth) VALUES (@Image, @FirstName, @LastName, @Id, @City, @Address, @DateOfBirth)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = _pictureData;
                        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstNameTextBox.Text;
                        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastNameTextBox.Text;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = newId;
                        cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = cityTextBox.Text;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = addressTextBox.Text;

                        string dateInput = dateTextBox.Text.Trim();
                        string[] dateFormats = { "ddMMyyyy", "MMddyyyy", "ddMMyyyy", "MMddyy", "MMyyyydd", "MMddyyyy" };
                        DateTime parsedDate;
                        if (DateTime.TryParseExact(dateInput, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        {
                            cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = parsedDate;
                        }
                        else
                        {
                            MessageBox.Show("Invalid date format. Please enter the date in a valid format.");
                            return;
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data inserted successfully.");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data.");
                        }
                    }
                }
                else // Existing user
                {
                    if (!int.TryParse(_originalPersonId, out int originalId))
                    {
                        MessageBox.Show("Invalid original ID format.");
                        return;
                    }
                    if (newId != originalId)
                    {
                        string checkIdQuery = "SELECT COUNT(*) FROM dbo.PersonGridData WHERE Id = @Id";
                        using (SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, connection))
                        {
                            checkIdCmd.Parameters.Add("@Id", SqlDbType.Int).Value = newId;
                            int existingIdCount = (int)checkIdCmd.ExecuteScalar();

                            if (existingIdCount > 0)
                            {
                                MessageBox.Show("ID already exists in the database. Please enter a different ID.");
                                return;
                            }
                        }
                    }

                    string updateQuery = "UPDATE dbo.PersonGridData SET " +
                                         "Id = @NewId, " +
                                         "Picture = @Image, " +
                                         "FirstName = @FirstName, " +
                                         "LastName = @LastName, " +
                                         "City = @City, " +
                                         "Address = @Address, " +
                                         "DateOfBirth = @DateOfBirth " +
                                         "WHERE Id = @OriginalId";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.Add("@NewId", SqlDbType.Int).Value = newId;
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = _pictureData;
                        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstNameTextBox.Text;
                        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastNameTextBox.Text;
                        cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = cityTextBox.Text;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = addressTextBox.Text;

                        string dateInput = dateTextBox.Text.Trim();
                        string[] dateFormats = {
    "ddMMyyyy", // 07131997
    "MMddyyyy", // 07131997
    "ddMMyyyy", // 13071997
    "MMddyy",   // 071397
    "MMyyyydd", // 07199713
    "MMddyyyy", // 19970713
    "dd-MM-yyyy", // 13-07-1997
    "dd.MM.yyyy", // 13.07.1997
    "yyyy.MM.dd", // 1997.07.13
    "yyyy-MM-dd"  // 1997-07-13
}; DateTime parsedDate;
                        if (DateTime.TryParseExact(dateInput, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        {
                            cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = parsedDate;
                        }
                        else
                        {
                            MessageBox.Show("Invalid date format. Please enter the date in a valid format.");
                            return;
                        }

                        cmd.Parameters.Add("@OriginalId", SqlDbType.Int).Value = originalId;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data updated successfully.");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data.");
                        }
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            _pictureData = null;
            personPictureBox.Image = Properties.Resources.defaultPicture;
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the selected image into the PictureBox
                    using (Stream stream = openFileDialog.OpenFile())
                    {
                        Image selectedImage = Image.FromStream(stream);
                        personPictureBox.Image = selectedImage;

                        // Convert the selected image to byte array and store it in _pictureData
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            selectedImage.Save(memoryStream, selectedImage.RawFormat);
                            _pictureData = memoryStream.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading the image: " + ex.Message);
                }
            }
        }
    }
}
