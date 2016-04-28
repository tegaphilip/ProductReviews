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

        const string DB_PATH = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\reviews.mdf;Integrated Security=True";

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

        public Dictionary<String, String> getProductInfoByID(int id)
        {
            Dictionary<String, String> productInfo;

            try
            {
                this.OpenDatabaseConnection();
                string sql = "SELECT * FROM Products WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, this.connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    productInfo = new Dictionary<String, String>();
                    while (reader.Read())
                    {
                        productInfo.Add("id", reader.GetInt32(0).ToString());
                        productInfo.Add("name", reader.GetString(1));
                        productInfo.Add("image_url", reader.GetString(2));
                        productInfo.Add("date_created", reader.GetDateTime(3).ToString());
                        return productInfo;
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

        public List<String[]> getProductReviews(int productId)
        {
            List<String[]> list;

            try
            {
                this.OpenDatabaseConnection();
                string sql = "SELECT r.*,u.FirstName,u.LastName FROM Reviews r LEFT JOIN Users u " + 
                " ON (u.Id = r.UserId) WHERE r.ProductId = @id";
                SqlCommand cmd = new SqlCommand(sql, this.connection);
                cmd.Parameters.AddWithValue("@id", productId);
                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    list = new List<string[]>();

                    String[] row;
                    while (reader.Read())
                    {
                        row = new String[8];
                        row[0] = reader.GetInt32(0).ToString();
                        row[1] = reader.GetInt32(1).ToString();
                        row[2] = reader.GetInt32(2).ToString();
                        row[3] = reader.GetString(3);
                        row[4] = reader.GetInt32(4).ToString();
                        row[5] = reader.GetDateTime(5).ToString();
                        row[6] = reader.GetString(6);
                        row[7] = reader.GetString(7);
                        list.Add(row);
                    }

                    return list;
                }
                //throw new Exception("What the hell is your problem");
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

        public Boolean AddReview(int productId, int userId, String comment, int point)
        {

            string sql = "INSERT INTO Reviews(ProductId,UserId,Comment,Point,DateAdded) VALUES" +
                "(@pid,@uid,@comment,@point,@date)";
            try
            {
                if (this.checkIfReviewExists(userId, productId))
                {
                    throw new Exception("You have previously reviewed this product");
                }

                this.OpenDatabaseConnection();
                SqlCommand cmd = new SqlCommand(sql, this.connection);
                cmd.Parameters.AddWithValue("@pid", productId);
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.Parameters.AddWithValue("@point", point);
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

        public bool checkIfReviewExists(int userId, int productId)
        {
            try
            {
                this.OpenDatabaseConnection();
                string sql = "SELECT * FROM Reviews WHERE UserId = @uid AND ProductId = @pid";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@pid", productId);
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
    }
}