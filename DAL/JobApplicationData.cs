using Dapper;
using JobApplication.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace JobApplication.DAL
{
    public class JobApplicationData : IJobApplicationData
    {
        private readonly string _connString;
        public JobApplicationData(string connString)
        {
            _connString = connString;
        }
        public IEnumerable<SkillDTO> GetSkills()
        {
            string sql = @"select a.Id SkillId, a.SkillName, b.Id SkillGroupId, b.SkillGroupName from Skills a
                            join SkillGroups b on b.Id = a.SkillGroupId";

            IEnumerable<SkillDTO> skills = new List<SkillDTO>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                skills = conn.Query<SkillDTO>(sql);
            }
            return skills;
        }

        public bool Add(ApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
                return false;

            string sqlApplication = @"insert into Applications (firstname, lastname, currentjobtitle, preferredlocation, yearsofexperience, heardfromwhere, noticeperiod, phone, email, address, resumefilepath, jobpostingid)
                                    values (@firstname, @lastname, @currentjobtitle, @preferredlocation, @yearsofexperience, @heardfromwhere, @noticeperiod, @phone, @email, @address, @resumefilepath, @jobpostingid);
                                    select scope_identity();
                            ";

            string sqlSkillMap = @"insert into SkillMap (ApplicationId, SkillId)
                                        values (@ApplicationId, @SkillId);
                            ";

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    var insertedRowId = conn.ExecuteScalar<int>(sqlApplication, applicationDTO, transaction);

                    foreach (var skillId in applicationDTO.Skills)
                    {
                        conn.Execute(sqlSkillMap, new { ApplicationId = insertedRowId, SkillId = skillId }, transaction);
                    }
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<ApplicationDTO> GetApplicationByEmail(string email)
        {
            string sql = @"select * from Applications where Email = @email";

            IEnumerable<ApplicationDTO> applications = null;

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                applications = conn.Query<ApplicationDTO>(sql, new { email = email });
            }
            return applications;
        }

        public IEnumerable<ApplicationSKillDTO> GetApplicationAndSkills()
        {
            string sql = @"select a.*, c.Id SkillId, c.SkillName from Applications a
                            join SkillMap b on b.ApplicationId = a.Id
                            join Skills c on c.Id = b.SkillId";

            IEnumerable<ApplicationSKillDTO> applications = new List<ApplicationSKillDTO>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                applications = conn.Query<ApplicationSKillDTO>(sql);
            }
            return applications;
        }
    }
}
