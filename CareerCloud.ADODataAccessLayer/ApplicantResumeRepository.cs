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
    public class ApplicantResumeRepository :BaseADO,IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;

                foreach (ApplicantResumePoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Applicant_Resumes
                    (Id, Applicant, Resume, Last_Updated )
                     Values
                     (@Id, @Applicant, @Resume, @Last_Updated)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Applicant ", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    Cmd.Parameters.AddWithValue("@Last_Updated ", poco.LastUpdated);


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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            ApplicantResumePoco[] pocos = new ApplicantResumePoco[1000];
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Applicant_Resumes";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Resume = reader.GetString(2);
                    //poco.LastUpdated = (DateTime?)reader[3];
                    //poco.LastUpdated = Convert.DateTime(reader)[3];

                    if (reader.IsDBNull(3))
                         poco.LastUpdated = null;
                    else
                        poco.LastUpdated = reader.GetDateTime(3);

                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }
        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (ApplicantResumePoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Applicant_Resumes
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            SqlConnection Conn = new SqlConnection(ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(ApplicantResumePoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Applicant_Resumes
                          SET Applicant=@Applicant,
                              Resume=@Resume, 
                              Last_Updated=@Last_Updated
                          WHERE Id=@Id";    


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Applicant ", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    Cmd.Parameters.AddWithValue("@Last_Updated ", poco.LastUpdated);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }
    }
}
