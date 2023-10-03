
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Configuration;
using Dapper;

namespace WebService
{
    /// <summary>
    /// Summary description for PersonService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PersonService : System.Web.Services.WebService
    {
        [WebMethod]
        public int Insert(Person person)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Phonebook_APP.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                int result = db.Execute("dbo.sp_PersonGridData_Insert",
            new
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Id = person.Id,
                DateOfBirth = person.DateOfBirth,
                Address = person.Address,
                City = person.City,
                Picture = person.Picture
            },
            commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        [WebMethod]
        public bool Update(Person person)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Phonebook_APP.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    var parameters = new
                    {
                        P_Id = person.P_Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Id = person.Id,
                        DateOfBirth = person.DateOfBirth,
                        Address = person.Address,
                        City = person.City,
                        Picture = person.Picture
                    };

                    // Call the stored procedure to update the PersonGridData table
                    db.Execute("UpdatePersonData", parameters, commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error, show a message to the user)
                Console.WriteLine("Error updating data: " + ex.Message);
                return false;
            }
        }



        //Works
        [WebMethod]
        public List<Person> GetAll()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Phonebook_APP.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return db.Query<Person>("select * from dbo.PersonGridData", commandType: CommandType.Text).ToList();
            }
        }

        //Works
        [WebMethod]
        public bool Delete(int personId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Phonebook_APP.Properties.Settings.Phonebook_DatabaseConnectionString"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                int result = db.Execute("delete from PersonGridData where Id = @Id", new
                {
                    Id = personId
                }, commandType: CommandType.Text);
                return result != 0;

            }
        }
    }
}
