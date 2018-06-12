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
   public class SecurityLoginsLogRepository : BaseADO, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (SecurityLoginsLogPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Security_Logins_Log
                       (Id,Login,Source_IP,Logon_Date,Is_Succesful)
                Values
                       (@Id,@Login,@Source_IP,@Logon_Date,@Is_Succesful)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Login", poco.Login);
                    Cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    Cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    Cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);

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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[2000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Security_Logins_Log";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.SourceIP = reader.GetString(2);
                    poco.LogonDate = reader.GetDateTime(3);
                    poco.IsSuccesful = reader.GetBoolean(4);
                    

                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Security_Logins_Log
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Security_Logins_Log
                          SET Login=@Login,
                              Source_IP=@Source_IP,
                              Logon_Date=@Logon_Date, 
                              Is_Succesful=@Is_Succesful
                          WHERE Id=@Id"; 


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Login", poco.Login);
                    Cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    Cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    Cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }
    }
}
