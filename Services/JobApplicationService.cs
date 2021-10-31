using JobApplication.DAL;
using JobApplication.Entities;
using System.Collections.Generic;

namespace JobApplication.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationData _jobApplicationData;

        public JobApplicationService(IJobApplicationData jobApplicationData)
        {
            _jobApplicationData = jobApplicationData;
        }
        public IEnumerable<SkillDTO> GetSkills()
        {
            return _jobApplicationData.GetSkills();
        }

        public bool Add(ApplicationDTO applicationDTO)
        {
            return _jobApplicationData.Add(applicationDTO);
        }

        public IEnumerable<ApplicationDTO> GetApplicationByEmail(string email)
        {
            return _jobApplicationData.GetApplicationByEmail(email);
        }

        public IEnumerable<ApplicationSKillDTO> GetApplicationAndSKills()
        {
            var applications = _jobApplicationData.GetApplicationAndSkills();

            return applications;
        }
    }
}
