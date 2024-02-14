using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CareerTrack.Models;
using CareerTrack.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CareerTrack.Controllers
{
    public class JobAPIController : Controller
    {
        private readonly IJobRepository _jobService;
        public JobAPIController(IJobRepository jobService)
        {
            _jobService = jobService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Job newJob)
        {
            if (ModelState.IsValid)
            {
                _jobService.AddJob(newJob);
                return Ok(newJob); // This will return the newJob object as JSON
            }
            else
            {
                return NotFound(newJob);
            }
        }
    }
}

