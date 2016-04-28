using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProductReviews
{
    public class DBConn
    {
        private SqlConnection connection;

        private string lastErrorMessage = "An error occurred";

        const string DB_PATH = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"C:\\Users\\t.oghenekohwo.15\\Documents\\visual studio 2013\\Projects\\ProductReviews\\ProductReviews\\App_Data\\reviews.mdf\";Integrated Security=True";
        //const string DB_PATH = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"C:\\Users\\t.oghenekohwo.15\\Documents\\visual studio 2013\\Projects\\ProductReviews\\ProductReviews\\App_Data\\reviews.mdf\";Integrated Security=True"; 

        public DBConn()
        {
            this.connection = new SqlConnection(DB_PATH);
            this.OpenDatabaseConnection();
        }

        public void OpenDatabaseConnection()
        {
            if (this.connection != null)
            {
                if (this.connection.State != ConnectionState.Open)
                {
                    this.connection.Open();
                }
            }
        }

        public void CloseDatabaseConnection()
        {
            if (this.connection != null)
            {
                this.connection.Close();
            }
        }

        public Boolean RegisterUser(String email, String password, String firstName, String lastName)
        {
            string hashedPassword = Util.HashPassword(password);

            string sql = "INSERT INTO Users(Email,Password,FirstName,LastName,DateRegistered) VALUES(@email,@password,@firstname,@lastname,@date)";
            try
            {
                if (this.checkIfEmailExists(email))
                {
                    throw new Exception("The email provided already exists!");
                }

                this.OpenDatabaseConnection();
                SqlCommand cmd = new SqlCommand(sql, this.connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@lastname", lastName);
                cmd.Parameters.AddWithValue("@firstname", firstName);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                cmd.CommandType = CommandType.Text;
                int insert = cmd.ExecuteNonQuery();
                return insert == 1;
            }
            catch (Exception e)
            {
                this.lastErrorMessage = e.Message;
                return false;
            }
            finally
            {
                this.CloseDatabaseConnection();
            }
        }

        public string getErrorMessage()
        {
            return this.lastErrorMessage;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<String, String> getUserByEmail(String email)
        {
            Dictionary<String, String> userData;

            try
            {
                this.OpenDatabaseConnection();
                string sql = "SELECT * FROM Users WHERE Email = @email";
                SqlCommand cmd = new SqlCommand(sql, this.connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userData = new Dictionary<String, String>();
                        userData.Add("id", reader.GetInt32(0).ToString());
                        userData.Add("email", reader.GetString(1));
                        userData.Add("password", reader.GetString(2));
                        userData.Add("first_name", reader.GetString(3));
                        userData.Add("last_name", reader.GetString(4));
                        userData.Add("date_registered", reader.GetDateTime(5).ToString());
                        return userData;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                setlastErrorMessage(e);
                return null;
            }
            finally
            {
                this.CloseDatabaseConnection();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool checkIfEmailExists(String email)
        {
            try
            {
                this.OpenDatabaseConnection();
                string sql = "SELECT * FROM Users WHERE Email = @email";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                setlastErrorMessage(e);
                return false;
            }
            finally
            {
                this.CloseDatabaseConnection();
            }
        }

        private void setlastErrorMessage(Exception e)
        {
            this.lastErrorMessage = e.Message;
        }

        public List<String[]> getProductsAndReviews()
        {
            List<String[]> list;
            try
            {
                this.OpenDatabaseConnection();
                string sql = "SELECT p.Id,p.Name, p.ImageUrl,p.DateCreated, count(r.Id) AS ReviewsCount FROM Products p " +
" LEFT JOIN Reviews r ON (r.ProductId = p.Id) GROUP BY p.Id,p.Name, p.ImageUrl,p.DateCreated ";
                SqlCommand cmd = new SqlCommand(sql, this.connection);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    list = new List<string[]>();

                    String[] row;
                    while (reader.Read())
                    {
                        row = new String[5];
                        row[0] = reader.GetInt32(0).ToString();
                        row[1] = reader.GetString(1);
                        row[2] = reader.GetString(2);
                        row[3] = reader.GetDateTime(3).ToString();
                        row[4] = reader.GetInt32(4).ToString();
                        list.Add(row);
                    }
                    
                    return list;
                }
                return null;
            }
            catch (Exception e)
            {
                setlastErrorMessage(e);
                return null;
            }
            finally
            {
                this.CloseDatabaseConnection();
            }

        }
    }
}