using Dapper;
using JobApplication.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace JobApplication.DAL
{
    public class JobPostingData : IJobPostingData
    {
        private readonly string _connString;
        public JobPostingData(string connString)
        {
            _connString = connString;
        }
        public JobPostingDTO GetJobPostingById(int id)
        {
            string sql = @"select * from JobPostings where Id = @Id";

            JobPostingDTO jobPosting = new JobPostingDTO();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                jobPosting = conn.Query<JobPostingDTO>(sql, new { Id = id }).FirstOrDefault();
            }
            return jobPosting;
        }

        public IEnumerable<JobPostingDTO> GetJobPostings()
        {
            string sql = @"select a.Id, a.JobTitle, a.JobDescription, a.Employer, c.SkillName from JobPostings a
                            join JobPostingSkillMap b on b.JobPostingId = a.Id
                            join Skills c on c.Id = b.SkillId";

            IEnumerable<JobPostingDTO> jobPostings = new List<JobPostingDTO>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                jobPostings = conn.Query<JobPostingDTO>(sql);
            }
            return jobPostings;
        }

        public IEnumerable<string> GetSkillsByJobPostingId(int id)
        {
            string sql = @"select c.SkillName from JobPostings a
                            join JobPostingSkillMap b on b.JobPostingId = a.Id
                            join Skills c on c.Id = b.SkillId
                            where a.Id = @Id
                            ";

            IEnumerable<string> skills = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                skills = conn.Query<string>(sql, new { Id = id });
            }
            return skills;
        }
    }
}
