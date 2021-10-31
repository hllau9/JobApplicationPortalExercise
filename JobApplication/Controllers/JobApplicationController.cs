using JobApplication.Entities;
using JobApplication.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using JobApplication.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JobApplication.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IJobPostingService _jobPostingService;
        private static JobApplicationFormVM editModel { get; set; } = new JobApplicationFormVM();

        public JobApplicationController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IJobApplicationService jobApplicationService, IJobPostingService jobPostingService)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _jobApplicationService = jobApplicationService;
            _jobPostingService = jobPostingService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Application");
        }

        public IActionResult Application(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index", "JobPosting");

            JobApplicationFormVM model = new JobApplicationFormVM();
            model.HeardFromWhereOptions = PopulateHeardFromOptions();
            model.NoticePeriodOptions = PopulateNoticePeriodOptions();

            var skillOptions = PopulateSkillOptions();
            if (skillOptions == null)
                return View("Error");
            model.SkillOptions = skillOptions;
            model.JobPostingId = id;

            try
            {
                model.JobPostingTitle = _jobPostingService.GetJobPostingById(id).JobTitle;
            }
            catch
            {
                return View("Error");
            }
            

            return View(model);
        }

        [HttpPost]
        public IActionResult Application(JobApplicationFormVM model)
        {
            try
            {
                model.JobPostingTitle = _jobPostingService.GetJobPostingById(model.JobPostingId).JobTitle;

                var applications = _jobApplicationService.GetApplicationByEmail(model.Email);
                if (applications != null)
                {
                    if (applications.Where(a => a.JobPostingId == model.JobPostingId).Count() > 0)
                        ModelState.AddModelError("", "Somebody with the same email has already applied for this role.");
                }
            }
            catch
            {
                return View("Error");
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
                var skillOptions = PopulateSkillOptions();
                if (skillOptions == null)
                    return View("Error");
                model.SkillOptions = skillOptions;

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
                return RedirectToAction("Application");
            }

            IEnumerable<SkillDTO> skills = null;
            string jobPostingTitle = null;
            try 
            { 
                skills = _jobApplicationService.GetSkills();
                jobPostingTitle = _jobPostingService.GetJobPostingById(model.JobPostingId).JobTitle;
            }
            catch
            {
                return View("Error");
            }

            model.SelectedSkills = skills.Where(s => model.Skills.Contains(s.SkillId)).ToList();
            model.JobPostingTitle = jobPostingTitle;

            editModel = model;

            return View(model);
        }

        [HttpPost]
        public IActionResult Submit()
        {
            bool result = false;
            try
            {
                result = _jobApplicationService.Add(new ApplicationDTO
                {
                    FirstName = editModel.FirstName,
                    LastName = editModel.LastName,
                    CurrentJobTitle = editModel.CurrentJobTitle,
                    PreferredLocation = editModel.PreferredLocation,
                    YearsOfExperience = editModel.YearsOfExperience,
                    HeardFromWhere = editModel.HeardFromWhere,
                    Email = editModel.Email,
                    Address = editModel.Address,
                    Phone = editModel.Phone,
                    NoticePeriod = editModel.NoticePeriod,
                    ResumeFilePath = editModel.ResumeFilePath,
                    Skills = editModel.Skills,
                    JobPostingId = editModel.JobPostingId
                });
            }
            catch
            {
                return View("Error");
            }

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
            
            var skillOptions = PopulateSkillOptions();
            if (skillOptions == null)
                return View("Error");

            editModel.SkillOptions = skillOptions;

            return View(editModel);
        }

        public IActionResult ViewApplications()
        {
            IEnumerable<ApplicationSKillDTO> applications = null;
            IEnumerable<JobApplicationFormVM> model = null;
            try
            {
                applications = _jobApplicationService.GetApplicationAndSKills();

                model = from a in applications
                        group a.SkillName by new { a.Id, a.FirstName, a.LastName, a.CurrentJobTitle, a.YearsOfExperience, a.PreferredLocation, a.HeardFromWhere, a.NoticePeriod, a.Phone, a.Email, a.Address, a.ResumeFilePath, a.JobPostingId } into g
                        select new JobApplicationFormVM
                        {
                            Id = g.Key.Id,
                            FirstName = g.Key.FirstName,
                            LastName = g.Key.LastName,
                            CurrentJobTitle = g.Key.CurrentJobTitle,
                            YearsOfExperience = g.Key.YearsOfExperience,
                            PreferredLocation = g.Key.PreferredLocation,
                            HeardFromWhere = g.Key.HeardFromWhere,
                            NoticePeriod = g.Key.NoticePeriod,
                            Phone = g.Key.Phone,
                            Email = g.Key.Email,
                            Address = g.Key.Address,
                            ResumeFilePath = g.Key.ResumeFilePath,
                            SkillList = g.ToList(),
                            JobPostingTitle = _jobPostingService.GetJobPostingById(g.Key.JobPostingId).JobTitle
                        };
            }
            catch
            {
                return View("Error");
            }

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
            IEnumerable<SkillDTO> skillGroups = null;
            try
            {
                skillGroups = _jobApplicationService.GetSkills();
            }
            catch
            {
                return null;
            }

            List<SelectListGroup> selectListGroups = skillGroups.GroupBy(sg => sg.SkillGroupName).Select(s => new SelectListGroup { Name = s.First().SkillGroupName }).ToList();
            List<SelectListItem> selectListItems = skillGroups.Select(o => new SelectListItem() { Text = o.SkillName, Value = o.SkillId.ToString(), Selected = o.SkillId == selected, Group = selectListGroups.Where(slg => slg.Name == o.SkillGroupName).First() }).ToList();

            return selectListItems;
        }
    }
}
