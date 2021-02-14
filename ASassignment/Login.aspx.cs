using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ASassignment
{
    public partial class Login : System.Web.UI.Page
    {
        string Database1ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database1"].ConnectionString;
        
        string Locker;
        DateTime Current = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string getDBHash(string email)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(Database1ConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }
        protected string getDBSalt(string email)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(Database1ConnectionString);
            string sql = "select PasswordSalt FROM ACCOUNT WHERE Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                s = reader["PasswordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }
        public string getDBLock(string email)
        {
            SqlConnection connection = new SqlConnection(Database1ConnectionString);
            string sql = "SELECT * FROM Account WHERE Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Lock"] != DBNull.Value)
                        {
                            Locker = reader["Lock"].ToString();

                        }
                    }

                }
            }//try
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return "";
           
        }
        public int getDBCountit(string email)
        {
            int CountiT=0 ;
            SqlConnection connection = new SqlConnection(Database1ConnectionString);
            string sql = "SELECT * FROM Account WHERE Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Countit"] != DBNull.Value)
                        {
                            CountiT =  Convert.ToInt32(reader["Countit"]);

                        }
                    }

                }
            }//try
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return CountiT;

        }


        protected void LoginBTN_Click(object sender, EventArgs e)
        {
            string PW = TBPassword.Text.ToString().Trim();
            string Email = TBEmail.Text.ToString().Trim();

            SHA512Managed hashing = new SHA512Managed();
            string dbhash = getDBHash(Email);
            string dbSalt = getDBSalt(Email);
            //Grab to see if account lock
            getDBLock(Email);
            var count = getDBCountit(Email);
            if (count == 3)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(Database1ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Account SET Lock = @lock WHERE Email = @email"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.Text;

                                cmd.Parameters.AddWithValue("@Email", Email);
                                cmd.Parameters.AddWithValue("@lock", 0);






                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

            }
            if (Locker != "0")
            {
                try
                {
                    if (dbSalt != null && dbSalt.Length > 0 && dbhash != null && dbhash.Length > 0)
                    {
                        string pwdWithSalt = PW + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);
                        if (userHash.Equals(dbhash))
                        {
                            Session["Email"] = Email;
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                            Response.Redirect("MainPage.aspx", false);

                        }
                        else
                        {
                            count++;
                            try
                            {
                                using (SqlConnection con = new SqlConnection(Database1ConnectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("UPDATE Account SET Countit = @count WHERE Email = @email"))
                                    {
                                        using (SqlDataAdapter sda = new SqlDataAdapter())
                                        {
                                            cmd.CommandType = CommandType.Text;

                                            cmd.Parameters.AddWithValue("@Email", Email);
                                            cmd.Parameters.AddWithValue("@count", count);






                                            cmd.Connection = con;
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.ToString());
                            }
                            EmailVeri.Text = "Wrong password or Email"+"  " +count;
                        }

                    }
                    else
                    {
                        

                        EmailVeri.Text = "Account losdadagin error";

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { }

            }
            else if (Locker == "0")
            {
                LBLock.Text = "Account lock, please try again after 20 seconds";


            }
            
            
            
        }
    }
}