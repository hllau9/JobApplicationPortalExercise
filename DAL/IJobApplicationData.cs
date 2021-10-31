using JobApplication.Entities;
using System.Collections.Generic;

namespace JobApplication.DAL
{
    public interface IJobApplicationData
    {
        IEnumerable<SkillDTO> GetSkills();
        bool Add(ApplicationDTO applicationDTO);
        IEnumerable<ApplicationDTO> GetApplicationByEmail(string email);

        IEnumerable<ApplicationSKillDTO> GetApplicationAndSkills();
    }
}
