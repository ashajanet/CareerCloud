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
   public class ApplicantProfileRepository : BaseADO,IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {

                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (ApplicantProfilePoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Applicant_Profiles
                    (Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_code )
                     Values
                     (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_code)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Login", poco.Login);
                    Cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    Cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    Cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    Cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    Cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    Cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    Cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    Cmd.Parameters.AddWithValue("@Zip_Postal_code", poco.PostalCode);

                    Conn.Open();
                    rowsEffected += Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn) 
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Applicant_Profiles";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while(reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary =(decimal?)reader[2];
                    poco.CurrentRate = (decimal?)reader[3];
                    poco.Currency = reader.GetString(4);
                    poco.Country = reader.GetString(5);
                    poco.Province = reader.GetString(6);
                    poco.Street = reader.GetString(7);
                    poco.City = reader.GetString(8);
                    poco.PostalCode = reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];

                    pocos[position] = poco;
                    position++;
                    }
                Conn.Close();

                   }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable< ApplicantProfilePoco>pocos=GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Applicant_Profiles
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(ApplicantProfilePoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Applicant_Profiles
                         SET Login=@Login,
                             Current_Salary=@Current_Salary, 
                             Current_Rate= @Current_Rate,
                             Currency=@Currency,
                             Country_Code=@Country_Code,
                             State_Province_Code=@State_Province_Code,
                             Street_Address=@Street_Address,
                             City_Town=@City_Town,
                             Zip_Postal_code=@Zip_Postal_code
                         WHERE Id=@Id "; 

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Login", poco.Login);
                    Cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    Cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    Cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    Cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    Cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    Cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    Cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    Cmd.Parameters.AddWithValue("@Zip_Postal_code", poco.PostalCode);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }
    }
}
