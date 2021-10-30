using Entities;
using JobApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobApplicationService
    {
        IEnumerable<SkillDTO> GetSkills();
        bool Add(ApplicantDTO applicationDTO);
    }
}
