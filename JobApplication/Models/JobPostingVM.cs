using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Web.Models
{
    public class JobPostingVM
    {
        public int Id { get; set; }
        [Display(Name = "Job Title")] 
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription{ get; set; }
        [Display(Name = "Employer")]
        public string Employer { get; set; }
        public List<string> SkillList { get; set; }
    }
}
