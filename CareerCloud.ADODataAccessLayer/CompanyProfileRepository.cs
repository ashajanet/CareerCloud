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
   public class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (CompanyProfilePoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Company_Profiles
                     (Id,Registration_Date,Company_Website,Contact_Phone,Contact_Name,Company_Logo)
                Values
                     (@Id,@Registration_Date,@Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    Cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    Cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    Cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    Cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1000];
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Company_Profiles";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    //poco.CompanyWebsite = reader.GetString(2);
                    if (reader.IsDBNull(2))
                        poco.CompanyWebsite = null;
                    else
                        poco.CompanyWebsite= reader.GetString(2);

                    poco.ContactPhone = reader.GetString(3);
                    //poco.ContactName = reader.GetString(4);
                    if (reader.IsDBNull(4))
                        poco.ContactName = null;
                    else
                    poco.ContactName= reader.GetString(4);
                    //poco.CompanyLogo =(byte[])reader[5];
                    if (reader.IsDBNull(5))
                        poco.CompanyLogo = null;
                    else
                        poco.CompanyLogo = (byte[])reader[5];


                    poco.TimeStamp = (byte[])reader[6];

                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Company_Profiles
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(CompanyProfilePoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Company_Profiles 
                       SET Registration_Date=@Registration_Date,
                           Company_Website=@Company_Website,
                           Contact_Phone=@Contact_Phone,
                           Contact_Name=@Contact_Name,
                           Company_Logo=@Company_Logo
                       WHERE Id=@Id";


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    Cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    Cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    Cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    Cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }
    }
}
