using JobApplication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Web.Models
{
    public class JobApplicationFormVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Current Job Title")]
        public string CurrentJobTitle { get; set; }
        [Required(ErrorMessage = "Please enter the number of years of experience.")]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        [Display(Name = "Number of Years of Experience")]
        public int? YearsOfExperience { get; set; }
        [Required]
        [Display(Name = "Preferred Location")]
        public string PreferredLocation { get; set; }
        [Required(ErrorMessage = "Please tell us where you heard about the vacancy.")]
        [Display(Name = "Where Did You Hear About the Vacancy")]
        public string HeardFromWhere { get; set; }
        [Required(ErrorMessage = "The Notice Period field is required.")]
        [Display(Name = "Notice Period in Your Current Role")]
        public string NoticePeriod { get; set; }
        [Required]
        [Display(Name = "Contact No")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please upload your resume.")]
        [Display(Name = "Resume")]
        public IFormFile ResumeFile { get; set; }
        [Display(Name = "Resume")]
        public string ResumeFilePath { get; set; }
        [Required]
        [Display(Name = "Your Skills")]
        public int[] Skills { get; set; }

        public int JobPostingId { get; set; }
        [Display(Name = "Job Title")]
        public string JobPostingTitle { get; set; }

        public List<string> SkillList { get; set; }
        public List<SkillDTO> SelectedSkills { get; set; }
        public List<SelectListItem> HeardFromWhereOptions { get; set; }
        public List<SelectListItem> SkillOptions { get; set; }
        public List<SelectListItem> NoticePeriodOptions { get; set; }
    }
}
