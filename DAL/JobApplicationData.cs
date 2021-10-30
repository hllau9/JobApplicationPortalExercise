using Dapper;
using Entities;
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
            

            //List<SkillDTO> skills = new List<SkillDTO>();
            //skills.Add(new SkillDTO { SkillGroupId = 1, SkillGroupName = "Group One", SkillId = 1, SkillName = "Skill One" });
            //skills.Add(new SkillDTO { SkillGroupId = 1, SkillGroupName = "Group One", SkillId = 2, SkillName = "Skill Two" });
            //skills.Add(new SkillDTO { SkillGroupId = 2, SkillGroupName = "Group Two", SkillId = 6, SkillName = "Skill Six" });
            //skills.Add(new SkillDTO { SkillGroupId = 3, SkillGroupName = "Group Three", SkillId = 5, SkillName = "Skill Five" });
            //skills.Add(new SkillDTO { SkillGroupId = 4, SkillGroupName = "Group Four", SkillId = 4, SkillName = "Skill Four" });
            //skills.Add(new SkillDTO { SkillGroupId = 4, SkillGroupName = "Group Four", SkillId = 3, SkillName = "Skill Three" });

            //return skills;
        }
    }
}
