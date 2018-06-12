using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyDescriptionRepository :BaseADO,IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {

                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Company_Descriptions
                       (Id,Company,LanguageID,Company_Name,Company_Description)
                 values
                       (@Id,@Company,@LanguageID,@Company_Name,@Company_Description)";


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Company", poco.Company);
                    Cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    Cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    Cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);


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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Company_Descriptions";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.LanguageId = reader.GetString(2);
                    poco.CompanyName = reader.GetString(3);
                    poco.CompanyDescription = reader.GetString(4);
                    poco.TimeStamp = (byte[])reader[5];
                    
                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Company_Descriptions
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(CompanyDescriptionPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Company_Descriptions
                        SET Company=@Company,
                            LanguageID=@LanguageID,
                            Company_Name=@Company_Name,
                            Company_Description=@Company_Description
                       WHERE Id=@Id";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Company", poco.Company);
                    Cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    Cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    Cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();


                }
            }
                        
            
        }
    }
}
