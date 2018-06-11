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
    public class ApplicantEducationRepository : BaseADO, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (ApplicantEducationPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Applicant_Educations
                    (Id, Applicant, Major, Certificate_Diploma, Start_Date, Completion_Date, Completion_Percent)
                     Values
                     (@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date, @Completion_Date, @Completion_Percent)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Major", poco.Major);
                    Cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    Cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    Cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    Cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Applicant_Educations";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

                int Position = 0;
                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.GetString(3);
                    poco.StartDate = (DateTime?)reader[4];
                    poco.CompletionDate = (DateTime?)reader[5];
                    poco.CompletionPercent = (byte?)reader[6];
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[Position] = poco;
                    Position++;
                }
                Conn.Close();
             

            }
            //return Pocos;
             return pocos.Where(p => p != null).ToList();



        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(ApplicantEducationPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Applicant_Educations
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;

                foreach(ApplicantEducationPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Applicant_Educations
                        SET  Applicant=@Applicant,
                             Major=@Major,
                             Certificate_Diploma=@Certificate_Diploma,
                             Start_Date=@Start_Date,
                             Completion_Date=@Completion_Date,
                             Completion_Percent=@Completion_Percent
                             WHERE Id=@Id";

                    Cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Major", poco.Major);
                    Cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    Cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    Cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    Cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);


                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();

                }
            }
        }
    }

}   

