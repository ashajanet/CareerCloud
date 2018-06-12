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
   public class CompanyJobRepository :BaseADO,IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (CompanyJobPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Company_Jobs
                     (Id,Company,Profile_Created,Is_Inactive,Is_Company_Hidden)
                Values
                     (@Id,@Company,@Profile_Created,@Is_Inactive,@Is_Company_Hidden)";


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Company", poco.Company);
                    Cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    Cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    Cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);


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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            CompanyJobPoco[] pocos = new CompanyJobPoco[2000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Company_Jobs";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.ProfileCreated = reader.GetDateTime(2);
                    poco.IsInactive = reader.GetBoolean(3);
                    poco.IsCompanyHidden = reader.GetBoolean(4);
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (CompanyJobPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Company_Jobs
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(CompanyJobPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Company_Jobs
                       SET Company=@Company,
                           Profile_Created=@Profile_Created,
                           Is_Inactive=@Is_Inactive,
                           Is_Company_Hidden=@Is_Company_Hidden
                       WHERE Id=@Id"; 

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Company", poco.Company);
                    Cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    Cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    Cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();

                }
            }
        }
    }
}
