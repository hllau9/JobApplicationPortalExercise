using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class JobApplicationFormVM
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Please enter the number of years of experience.")]
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
        public int[] Skills { get; set; }
        
        public List<SkillDTO> SelectedSkills { get; set; }
        public List<SelectListItem> HeardFromWhereOptions { get; set; }
        public List<SelectListItem> SkillOptions { get; set; }
        public List<SelectListItem> NoticePeriodOptions { get; set; }
    }

    public class TestVM
    {
        public List<string> Skills { get; set; }
        public List<SelectListItem> SkillOptions { get; set; }
    }
}
