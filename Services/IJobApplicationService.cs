using JobApplication.Entities;
using System.Collections.Generic;

namespace JobApplication.Services
{
    public interface IJobApplicationService
    {
        IEnumerable<SkillDTO> GetSkills();
        bool Add(ApplicationDTO applicationDTO);

        IEnumerable<ApplicationDTO> GetApplicationByEmail(string email);

        IEnumerable<ApplicationSKillDTO> GetApplicationAndSKills();
    }
}
