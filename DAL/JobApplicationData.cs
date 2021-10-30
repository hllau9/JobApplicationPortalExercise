﻿using Dapper;
using Entities;
using JobApplication.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
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

        public bool Add(ApplicantDTO applicantDTO)
        {
            string sqlApplicant = @"insert into Applicants (firstname, lastname, jobtitle, preferredlocation, yearsofexperience, heardfromwhere, noticeperiod, phone, email, address, resumefilepath)
                                    values (@firstname, @lastname, @jobtitle, @preferredlocation, @yearsofexperience, @heardfromwhere, @noticeperiod, @phone, @email, @address, @resumefilepath);
                                    select scope_identity();
                            ";

            string sqlSkillMap = @"insert into SkillMap (ApplicantId, SkillId)
                                        values (@ApplicantId, @SkillId);
                            ";

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    var insertedRowId = conn.ExecuteScalar<int>(sqlApplicant, applicantDTO, transaction);

                    foreach (var skillId in applicantDTO.Skills)
                    {
                        conn.Execute(sqlSkillMap, new { ApplicantId = insertedRowId, SkillId = skillId }, transaction);
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
    }
}
