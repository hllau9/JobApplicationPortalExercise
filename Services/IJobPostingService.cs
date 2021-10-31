using JobApplication.Entities;
using System.Collections.Generic;

namespace JobApplication.Services
{
    public interface IJobPostingService
    {
        IEnumerable<JobPostingDTO> GetJobPostings();
        JobPostingDTO GetJobPostingById(int id);
        IEnumerable<string> GetSkillsByJobPostingId(int id);
    }
}
