﻿using Entities;
using JobApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IJobApplicationData
    {
        IEnumerable<SkillDTO> GetSkills();
        bool Add(ApplicantDTO applicationDTO);
        ApplicantDTO GetApplicantByEmail(string email);

        IEnumerable<ApplicantSKillDTO> GetApplicantsAndSkills();
    }
}
