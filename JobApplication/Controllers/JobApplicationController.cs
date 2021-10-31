using JobApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Services;
using Entities;
using JobApplication.Entities;

namespace JobApplication.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IJobApplicationService _jobApplicationService;
        private static JobApplicationFormVM editModel { get; set; } = new JobApplicationFormVM();

        public JobApplicationController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IJobApplicationService jobApplicationService)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _jobApplicationService = jobApplicationService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ApplicantRegistration");
        }

        public IActionResult ApplicantRegistration()
        {
            JobApplicationFormVM model = new JobApplicationFormVM();
            model.HeardFromWhereOptions = PopulateHeardFromOptions();
            model.NoticePeriodOptions = PopulateNoticePeriodOptions();
            model.SkillOptions = PopulateSkillOptions();

            return View(model);
        }

        [HttpPost]
        public IActionResult ApplicantRegistration(JobApplicationFormVM model)
        {
            if (_jobApplicationService.GetApplicantByEmail(model.Email) != null)
            {
                ModelState.AddModelError("", "Email already exists.");
            }

            var supportedTypes = new[] { "txt", "doc", "docx", "pdf" };
            var fileExt = Path.GetExtension(model.ResumeFile.FileName).Substring(1).ToLower();
            if (!supportedTypes.Contains(fileExt))
            {
                ModelState.AddModelError("", "Resume file extension is invalid - Only upload doc/docx/pdf/txt file.");
            }

            if (!ModelState.IsValid)
            {
                model.HeardFromWhereOptions = PopulateHeardFromOptions(model.HeardFromWhere);
                model.NoticePeriodOptions = PopulateNoticePeriodOptions(model.NoticePeriod);
                model.SkillOptions = PopulateSkillOptions();
                return View(model);
            }
        
            model.ResumeFilePath = "/resumes/" + UploadFile(model);

            return RedirectToAction("Review", model);
        }

        [HttpGet]
        public IActionResult Review(JobApplicationFormVM model)
        {
            StringValues header;
            Request.Headers.TryGetValue("Referer", out header);
            if (header.Count == 0)
            {
                return RedirectToAction("ApplicantRegistration");
            }

            var skills = _jobApplicationService.GetSkills();

            model.SelectedSkills = skills.Where(s => model.Skills.Contains(s.SkillId)).ToList();

            editModel = model;

            return View(model);
        }

        [HttpPost]
        public IActionResult Submit()
        {
            var result = _jobApplicationService.Add(new ApplicantDTO { 
                                FirstName = editModel.FirstName,
                                LastName = editModel.LastName,
                                JobTitle = editModel.JobTitle,
                                PreferredLocation = editModel.PreferredLocation,
                                YearsOfExperience = editModel.YearsOfExperience,
                                HeardFromWhere = editModel.HeardFromWhere,
                                Email = editModel.Email,
                                Address = editModel.Address,
                                Phone = editModel.Phone,
                                NoticePeriod = editModel.NoticePeriod,
                                ResumeFilePath = editModel.ResumeFilePath,
                                Skills = editModel.Skills
            });

            if (!result)
                return View("Error");

            TempData["SubmissionSuccessful"] = true;

            return RedirectToAction("Index", "Home");
        }
        private string UploadFile(JobApplicationFormVM model)
        {
            string uniqueFileName = null;

            if (model.ResumeFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "resumes");
                uniqueFileName = Guid.NewGuid() + "_" + model.ResumeFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ResumeFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Edit()
        {
            editModel.HeardFromWhereOptions = PopulateHeardFromOptions(editModel.HeardFromWhere);
            editModel.NoticePeriodOptions = PopulateNoticePeriodOptions(editModel.NoticePeriod);
            editModel.SkillOptions = PopulateSkillOptions();

            return View(editModel);
        }

        public IActionResult ViewApplicants()
        {
            var applicants = _jobApplicationService.GetApplicantsAndSKills();

            var results = from a in applicants
                          group a.SkillName by new { a.Id, a.FirstName, a.LastName, a.JobTitle, a.YearsOfExperience, a.PreferredLocation, a.HeardFromWhere, a.NoticePeriod, a.Phone, a.Email, a.Address, a.ResumeFilePath } into g
                          select new JobApplicationFormVM { 
                              Id = g.Key.Id,
                              FirstName = g.Key.FirstName,
                              LastName = g.Key.LastName,
                              JobTitle = g.Key.JobTitle,
                              YearsOfExperience = g.Key.YearsOfExperience,
                              PreferredLocation = g.Key.PreferredLocation,
                              HeardFromWhere = g.Key.HeardFromWhere,
                              NoticePeriod = g.Key.NoticePeriod,
                              Phone = g.Key.Phone,
                              Email = g.Key.Email,
                              Address = g.Key.Address,
                              ResumeFilePath = g.Key.ResumeFilePath,
                              SkillList = g.ToList()
                          };

            var model = results.ToList();

            return View(model);
        }

        private List<SelectListItem> PopulateHeardFromOptions(string selected = "")
        {
            List<string> options = new List<string>()
            {
                "Search",
                "Randstad Staff",
                "Advertisement",
                "Friends"
            };

            List<SelectListItem> selectListItems = options.Select(o => new SelectListItem() { Text = o, Value = o, Selected = o == selected }).ToList();

            return selectListItems;
        }

        private List<SelectListItem> PopulateNoticePeriodOptions(string selected = "")
        {
            List<string> options = new List<string>()
            {
                "0 day",
                "1 day",
                "2 days",
                "3 days",
                "4 days",
                "5 days",
                "1 week",
                "2 weeks",
                "3 weeks",
                "4 weeks",
                "1 month",
                "2 months",
                "3 months"
            };

            List<SelectListItem> selectListItems = options.Select(o => new SelectListItem() { Text = o, Value = o, Selected = o == selected }).ToList();

            return selectListItems;
        }

        private List<SelectListItem> PopulateSkillOptions(int selected = 0)
        {
            IEnumerable<SkillDTO> skillGroups = _jobApplicationService.GetSkills();

            List<SelectListGroup> selectListGroups = skillGroups.GroupBy(sg => sg.SkillGroupName).Select(s => new SelectListGroup { Name = s.First().SkillGroupName }).ToList();
            List<SelectListItem> selectListItems = skillGroups.Select(o => new SelectListItem() { Text = o.SkillName, Value = o.SkillId.ToString(), Selected = o.SkillId == selected, Group = selectListGroups.Where(slg => slg.Name == o.SkillGroupName).First() }).ToList();

            return selectListItems;
        }
    }
}
