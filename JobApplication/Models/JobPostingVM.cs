using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Web.Models
{
    public class JobPostingVM
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription{ get; set; }
        public string Employer { get; set; }
        public List<string> SkillList { get; set; }
    }
}
