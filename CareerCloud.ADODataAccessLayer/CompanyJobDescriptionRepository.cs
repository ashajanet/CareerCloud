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
   public class CompanyJobDescriptionRepository : BaseADO, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {

                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Company_Jobs_Descriptions
                     (Id,Job,Job_Name,Job_Descriptions)
                Values  
                     (@Id,@Job,@Job_Name,@Job_Descriptions)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Job", poco.Job);
                    Cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    Cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[2000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Company_Jobs_Descriptions";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.JobName = reader.GetString(2);
                    poco.JobDescriptions = reader.GetString(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Company_Jobs_Descriptions
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(CompanyJobDescriptionPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Company_Jobs_Descriptions
                       SET Job=@Job,
                           Job_Name=@Job_Name,
                           Job_Descriptions=@Job_Descriptions
                       WHERE Id=@Id";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Job", poco.Job);
                    Cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    Cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);


                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();

                }
            }
        }
    }
}
