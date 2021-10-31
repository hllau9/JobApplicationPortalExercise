using JobApplication.Entities;
using JobApplication.Web.Models;
using Microsoft.AspNetCore.Mvc;
using JobApplication.Services;
using System.Collections.Generic;
using System.Linq;

namespace JobApplication.Web.Controllers
{
    public class JobPostingController : Controller
    {
        private readonly IJobPostingService _jobPostingService;

        public JobPostingController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }
        public IActionResult Index()
        {
            IEnumerable<JobPostingDTO> jobPostings = null;
            try
            {
                jobPostings = _jobPostingService.GetJobPostings();
            }
            catch
            {
                return View("Error");
            }

            var model = from jp in jobPostings
                          group jp.SkillName by new { jp.Id, jp.JobTitle, jp.JobDescription, jp.Employer } into g
                          select new JobPostingVM
                          {
                              Id = g.Key.Id,
                              JobTitle = g.Key.JobTitle,
                              JobDescription = g.Key.JobDescription,
                              Employer = g.Key.Employer,
                              SkillList = g.ToList()
                          };

            return View(model);
        }
    }
}
