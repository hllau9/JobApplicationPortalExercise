﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Entities
{
    public class ApplicantDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public int? YearsOfExperience { get; set; }
        public string PreferredLocation { get; set; }
        public string HeardFromWhere { get; set; }
        public string NoticePeriod { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ResumeFilePath { get; set; }
        public int[] Skills { get; set; }
    }
}
