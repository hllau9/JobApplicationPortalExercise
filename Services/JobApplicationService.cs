using DAL;
using Entities;
using JobApplication.Entities;
using System;
using System.Collections.Generic;

namespace Services
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

        public bool Add(ApplicantDTO applicationDTO)
        {
            return _jobApplicationData.Add(applicationDTO);
        }

        public ApplicantDTO GetApplicantByEmail(string email)
        {
            return _jobApplicationData.GetApplicantByEmail(email);
        }
    }
}
