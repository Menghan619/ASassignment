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
    public partial class WebForm1 : System.Web.UI.Page
    {
        string Database1ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database1"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        int someNum = 0;
        string str;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string CheckEmail(string email)
        {
            string h = "";
            SqlConnection connection = new SqlConnection(Database1ConnectionString);
            string sql = "select Email FROM Account WHERE Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["Email"] != null)
                        {
                            if (reader["Email"] != DBNull.Value)
                            {
                                string emails = reader["Email"].ToString();
                                if (emails == email)
                                {
                                    EmailCheck.Text = "Email already exist";
                                    
                                    EmailCheck.ForeColor = Color.Red;
                                    someNum += 1;
                                }
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

        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Database1ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@Fname,@Lname, @NameOnCard, @PasswordHash, @PasswordSalt, @DoB, @CreditNo, @CVV,@Email,@IV,@Key,@Lock,@Countit)"))
                {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            
                            cmd.Parameters.AddWithValue("@Fname", TBLN.Text.ToString().Trim());
                            cmd.Parameters.AddWithValue("@NameOnCard", (TBCardName.Text.ToString().Trim()));
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@DoB", TBDOB.Text.ToString());
                            cmd.Parameters.AddWithValue("@Lname", TBLN.Text.ToString().Trim());
                            cmd.Parameters.AddWithValue("@CreditNo", Convert.ToBase64String(encryptData(TBCCN.Text.ToString().Trim())));
                            cmd.Parameters.AddWithValue("@CVV", Convert.ToBase64String(encryptData(TBCVV.Text.ToString().Trim())));
                            cmd.Parameters.AddWithValue("@Email", (TBEMail.Text.ToString().Trim()));
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Parameters.AddWithValue("@Lock", "1");
                            cmd.Parameters.AddWithValue("@Countit", 0);




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
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

        protected void REGBTN_Click(object sender, EventArgs e)
        {
            
            int passnum = 0;
            if (TBFname.Text.ToString() == "" && TBLN.Text.ToString() == "")
            {
                someNum += 1;
                FNCheck.Text = "First name cannot be empty";
                LNCheck.Text = "Last name cannot be empty";
                FNCheck.ForeColor = Color.Red;
                LNCheck.ForeColor = Color.Red;
            }
            else if (TBFname.Text.ToString() == "")
            {
                someNum += 1;
                FNCheck.Text = "First name cannot be empty";
                FNCheck.ForeColor = Color.Red;
            }
            else if(TBLN.Text.ToString() == "")
            {
                someNum += 1;

                LNCheck.Text = "Last name cannot be empty";
                LNCheck.ForeColor = Color.Red;
            }
            if (TBCCN.Text.ToString().Length != 16)
            {
                CreditnoCheck.Text = "Credit card number must be 16";
                CreditnoCheck.ForeColor = Color.Red;
            } 
            if (Regex.IsMatch(TBCCN.Text.ToString(), "[^0-9]"))
            {
                someNum += 1;
                CreditnoCheck.Text = "Error, characters not accepted. Your text:  " + HttpUtility.HtmlEncode(TBCardName.Text);
            }
            else
            {
                CreditnoCheck.Text = "";

            }
            if (TBEMail.Text.ToString() == "")
            {
                someNum += 1;
                EmailCheck.Text = "Credit card number must be 16";
                EmailCheck.ForeColor = Color.Red;
            }
            if (Regex.IsMatch(TBCardName.Text.ToString(), "[^A-Za-z]"))
            {
                someNum += 1;

                CardNameCheck.Text = "Error, characters not accepted. Your text:  " + HttpUtility.HtmlEncode(TBCardName.Text);
            }
            else
            {
                CardNameCheck.Text = "";

            }
            str = TBPW.Text.ToString().Trim();
            if (Regex.IsMatch(str, "[A-Z]"))
            {
                passnum += 1;
            }
            else
            {
                someNum += 1;
                PWcheck.Text = "Password must have at least one Uppercase, one Lowercase, one Digit and one Special Character!1";
                PWcheck.ForeColor = Color.Red;
            }
            if (Regex.IsMatch(str, "[a-z]"))
            {
                passnum += 1;
            }
            else
            {
                someNum += 1;
                PWcheck.Text = "Password must have at least one Uppercase, one Lowercase, one Digit and one Special Character!2";
                PWcheck.ForeColor = Color.Red;
            }
            if (Regex.IsMatch(str, "[0-9]"))
            {
                passnum += 1;

            }
            else
            {
                someNum += 1;
                PWcheck.Text = "Password must have at least one Uppercase, one Lowercase, one Digit and one Special Character!3";
                PWcheck.ForeColor = Color.Red;

            }
            if (Regex.IsMatch(str, "[^a-zA-Z0-9]"))
            {
                
            }
            else
            {
                someNum += 1;
                PWcheck.Text = "Password must have at least one Uppercase, one Lowercase, one Digit and one Special Character!4";
                PWcheck.ForeColor = Color.Red;
            }
            
           
           
            var cvv = TBCVV.Text.ToString();
            if (cvv.Length==3)
            {
                if (Regex.IsMatch(cvv, "[0-9]"))
                {

                }
                else
                {
                    CVVCheck.Text = "Error, CVV invalid";
                    someNum += 1;
                }

            }
            else
            {
                CVVCheck.Text = "Error, CVV invalid";
                someNum += 1;
            }
            CheckEmail(TBEMail.Text.ToString().Trim());
            if (Regex.IsMatch(TBFname.Text.ToString(), "[^A-Za-z0-9]"))
            {
                someNum += 1;
                FNCheck.Text = "Error, characters not accepted. Your text:  " + HttpUtility.HtmlEncode(TBFname.Text);
            }
            else
            {

                FNCheck.Text = "";

            }
            CheckEmail(TBEMail.Text.ToString().Trim());
            if (Regex.IsMatch(TBLN.Text.ToString(), "[^A-Za-z0-9]"))
            {
                someNum += 1;
                LNCheck.Text = "Error, characters not accepted. Your text:  " + HttpUtility.HtmlEncode(TBLN.Text);
            }
            else
            {

                LNCheck.Text = "";

            }

            if (someNum == 0)
            {
                PWcheck.Text = "FUCK YEAH";
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];
                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();

                string PassWithSalt = str + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(str));
                byte[] hashwithsalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(PassWithSalt));

                finalHash = Convert.ToBase64String(hashwithsalt);

                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;

                createAccount();
                Response.Redirect("Login.aspx", false);

            }
        }
    }
}