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
    public class SystemLanguageCodeRepository : BaseADO, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (SystemLanguageCodePoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO System_Language_Codes
                       (LanguageID,Name,Native_Name)
                Values
                       (@LanguageID,@Name,@Native_Name)";

                    Cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    Cmd.Parameters.AddWithValue("@Name", poco.Name);
                    Cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[1000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from System_Language_Codes";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();

                    poco.LanguageID = reader.GetString(0);
                    poco.Name = reader.GetString(1);
                    poco.NativeName = reader.GetString(2);
                  


                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM System_Language_Codes
                            WHERE LanguageID=@LanguageID";
                    Cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(SystemLanguageCodePoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE System_Language_Codes 
                        SET Name=@Name,
                            Native_Name=@Native_Name
                        WHERE LanguageID=@LanguageID";
                        

            Cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
            Cmd.Parameters.AddWithValue("@Name", poco.Name);
            Cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
        }
    }
}
}
}
