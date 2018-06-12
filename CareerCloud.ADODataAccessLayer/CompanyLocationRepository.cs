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
    public class CompanyLocationRepository : BaseADO, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (CompanyLocationPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Company_Locations
                      (Id,Company,Country_Code,State_Province_Code,Street_Address,City_Town,Zip_Postal_Code)
                values
                      (@Id,@Company,@Country_Code,@State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)";


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Company", poco.Company);
                    Cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    Cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    Cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    Cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    Cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[2000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Company_Locations";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.CountryCode = reader.GetString(2);
                    poco.Province = reader.GetString(3);
                    poco.Street = reader.GetString(4);
                    //poco.City = reader.GetString(5);

                    if (reader.IsDBNull(5))
                    {
                        poco.City = "";
                    }
                    else
                    {
                        poco.City = reader.GetString(5);
                    }

                    //poco.PostalCode = reader.GetString(6);
                    //poco.PostalCode = (string)reader[6];

                    if (reader.IsDBNull(6))
                    {
                        poco.PostalCode = "";
                    }
                    else
                    {
                        poco.PostalCode = reader.GetString(6);
                    }

                    poco.TimeStamp = (byte[])reader[7];

                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
                //return pocos.Where(p => p != null).ToList();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Company_Locations
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(CompanyLocationPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Company_Locations
                       SET Company=@Company,
                           Country_Code=@Country_Code, 
                           State_Province_Code=@State_Province_Code,
                           Street_Address=@Street_Address,
                           City_Town=@City_Town, 
                           Zip_Postal_Code=@Zip_Postal_Code
                       WHERE Id=@Id";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Company", poco.Company);
                    Cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    Cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    Cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    Cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    Cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }
    }
}
