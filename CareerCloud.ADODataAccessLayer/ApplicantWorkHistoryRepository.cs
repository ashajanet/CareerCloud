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
    public class ApplicantWorkHistoryRepository :BaseADO,IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {

                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Applicant_Work_History
                      (Id,Applicant,Company_Name,Country_Code,Location,Job_Title,Job_Description,Start_Month,Start_Year,End_Month,End_Year)
                Values
                      (@Id,@Applicant,@Company_Name,@Country_Code,@Location,@Job_Title,@Job_Description,@Start_Month,@Start_Year,@End_Month,@End_Year)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    Cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    Cmd.Parameters.AddWithValue("@Location", poco.Location);
                    Cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    Cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    Cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    Cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    Cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    Cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1000];
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Applicant_Work_History";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.CompanyName = reader.GetString(2);
                    poco.CountryCode = reader.GetString(3);
                    poco.Location = reader.GetString(4);
                    poco.JobTitle = reader.GetString(5);
                    poco.JobDescription = reader.GetString(6);
                    poco.StartMonth = reader.GetInt16(7);
                    poco.StartYear = reader.GetInt32(8);
                    poco.EndMonth = reader.GetInt16(9);
                    poco.EndYear = reader.GetInt32(10);
                    poco.TimeStamp = (byte[])reader[11];


                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Applicant_Work_History
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(ApplicantWorkHistoryPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Applicant_Work_History
                        SET Applicant=@Applicant,
                            Company_Name=@Company_Name,
                            Country_Code=@Country_Code,
                            Location=@Location,
                            Job_Title=@Job_Title,  
                            Job_Description=@Job_Description,
                            Start_Month=@Start_Month,
                            Start_Year=@Start_Year,
                            End_Month=@End_Month,
                            End_Year=@End_Year
                        WHERE Id=@Id";


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    Cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    Cmd.Parameters.AddWithValue("@Location", poco.Location);
                    Cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    Cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    Cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    Cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    Cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    Cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }
    }
}
