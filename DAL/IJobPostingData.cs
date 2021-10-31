using JobApplication.Entities;
using System.Collections.Generic;

namespace JobApplication.DAL
{
    public interface IJobPostingData
    {
        IEnumerable<JobPostingDTO> GetJobPostings();
        JobPostingDTO GetJobPostingById(int id);
        IEnumerable<string> GetSkillsByJobPostingId(int id);
    }
}
