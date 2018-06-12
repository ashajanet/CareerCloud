using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
   public class SecurityLoginRepository : BaseADO, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (SecurityLoginPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Security_Logins
                       (Id,Login,Password,Created_Date,Password_Update_Date,Agreement_Accepted_Date,Is_Locked,Is_Inactive,Email_Address,Phone_Number,Full_Name,Force_Change_Password,Prefferred_Language)
                Values
                       (@Id,@Login,@Password,@Created_Date,@Password_Update_Date,@Agreement_Accepted_Date,@Is_Locked,@Is_Inactive,@Email_Address,@Phone_Number,@Full_Name,@Force_Change_Password,@Prefferred_Language)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Login", poco.Login);
                    Cmd.Parameters.AddWithValue("@Password", poco.Password);
                    Cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    Cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    Cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    Cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    Cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    Cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    Cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    Cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    Cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    Cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

                    Conn.Open();
                    rowsEffected += Cmd.ExecuteNonQuery();
                    Conn.Close();

                }
            }

        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[1000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Security_Logins";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetString(1);
                    poco.Password = reader.GetString(2);
                    poco.Created = reader.GetDateTime(3);
                    //poco.PasswordUpdate = (DateTime?)reader[4];
                    if (reader.IsDBNull(4))
                        poco.PasswordUpdate = null;
                    else
                        poco.PasswordUpdate= (DateTime?)reader[4];

                    //poco.AgreementAccepted = (DateTime?)reader[5];
                    if (reader.IsDBNull(5))
                        poco.AgreementAccepted = null;
                    else
                        poco.AgreementAccepted = (DateTime?)reader[5];

                    poco.IsLocked = reader.GetBoolean(6);
                    poco.IsInactive = reader.GetBoolean(7);
                    poco.EmailAddress = reader.GetString(8);
                    //poco.PhoneNumber = reader.GetString(9);
                    if (reader.IsDBNull(9))
                        poco.PhoneNumber = null;
                    else
                    poco.PhoneNumber= reader.GetString(9);

                    poco.FullName = reader.GetString(10);
                    poco.ForceChangePassword = reader.GetBoolean(11);
                    //poco.PrefferredLanguage = reader.GetString(12);
                    if (reader.IsDBNull(12))
                        poco.PrefferredLanguage = null;
                    else
                        poco.PrefferredLanguage = reader.GetString(12);

                    poco.TimeStamp = (byte[])reader[13];

                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (SecurityLoginPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Security_Logins
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (SecurityLoginPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Security_Logins
                          SET Login=@Login,
                              Password=@Password,
                              Created_Date=@Created_Date, 
                              Password_Update_Date=@Password_Update_Date,
                              Agreement_Accepted_Date=@Agreement_Accepted_Date,
                              Is_Locked=@Is_Locked,
                              Is_Inactive=@Is_Inactive,
                              Email_Address=@Email_Address, 
                              Phone_Number=@Phone_Number,
                              Full_Name=@Full_Name,
                              Force_Change_Password=@Force_Change_Password,
                              Prefferred_Language=@Prefferred_Language
                         WHERE Id=@Id";


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Login", poco.Login);
                    Cmd.Parameters.AddWithValue("@Password", poco.Password);
                    Cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    Cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    Cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    Cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    Cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    Cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    Cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    Cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    Cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    Cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);


                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();

                }
            }
        }
    }
}
