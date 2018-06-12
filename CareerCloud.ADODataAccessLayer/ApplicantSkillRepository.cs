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
    public class ApplicantSkillRepository :BaseADO,IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {

                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                int rowsEffected = 0;
                foreach (ApplicantSkillPoco poco in items)
                {
                    Cmd.CommandText = @"INSERT INTO Applicant_Skills
                     (Id,Applicant,Skill,Skill_Level,Start_Month,Start_Year,End_Month,End_Year)
                Values
                     (@Id,@Applicant,@Skill,@Skill_Level,@Start_Month,@Start_Year,@End_Month,@End_Year)";

                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    Cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1000];
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from Applicant_Skills";

                Conn.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                int position = 0;

                while (reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.StartMonth = reader.GetByte(4);
                    poco.StartYear = reader.GetInt32(5);
                    poco.EndMonth = reader.GetByte(6);
                    poco.EndYear = reader.GetInt32(7);
                    poco.TimeStamp = (byte[])reader[8];


                    pocos[position] = poco;
                    position++;
                }
                Conn.Close();
            }
            return pocos.Where(p => p != null).ToList();
            }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach (ApplicantSkillPoco poco in items)
                {
                    Cmd.CommandText = @"DELETE FROM Applicant_Skills
                            WHERE Id=@Id";
                    Cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            SqlConnection Conn = new SqlConnection(_ConnectionStr);
            using (Conn)
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                foreach(ApplicantSkillPoco poco in items)
                {
                    Cmd.CommandText = @"UPDATE Applicant_Skills
                         SET Applicant=@Applicant,
                             Skill=@Skill,
                             Skill_Level=@Skill_Level, 
                             Start_Month=@Start_Month,
                             Start_Year=@Start_Year,
                             End_Month=@End_Month,  
                             End_Year=@End_Year
                        WHERE Id=@Id";   


                    Cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    Cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    Cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
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
