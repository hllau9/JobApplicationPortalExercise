using JobApplication.DAL;
using JobApplication.Entities;
using JobApplication.Services;
using System.Collections.Generic;

namespace JobApplication.Services
{
    public class JobPostingService : IJobPostingService
    {
        private readonly IJobPostingData _jobPostingData;

        public JobPostingService(IJobPostingData jobPostingData)
        {
            _jobPostingData = jobPostingData;
        }
        public JobPostingDTO GetJobPostingById(int id)
        {
            return _jobPostingData.GetJobPostingById(id);
        }

        public IEnumerable<JobPostingDTO> GetJobPostings()
        {
            return _jobPostingData.GetJobPostings();
        }

        public IEnumerable<string> GetSkillsByJobPostingId(int id)
        {
            return _jobPostingData.GetSkillsByJobPostingId(id);
        }
    }
}
