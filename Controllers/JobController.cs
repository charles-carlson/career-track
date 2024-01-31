﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerTrack.Models;
using CareerTrack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CareerTrack.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobService;
        public JobController(IJobRepository jobService)
        {
            _jobService = jobService;
        }
        public IActionResult Index(int page = 1, int pageSize = 25, string filter="", string sort="")
        {
            ModelState.Remove("filter");
            var sortType = sort;
            if(sort != "")
            {
                sortType = sort.Split(',')[1];   
            }
            var currentApplied = _jobService.GetNumberofJobs();
            var jobData = _jobService.GetPagedJobs(page, pageSize, filter,sort);
            int totalJobs = _jobService.GetNumberofPagedJobs(filter);
            int totalPages = (int)Math.Ceiling((double)totalJobs / pageSize);
            var viewModel = new PagedViewModel
            {
                
                Data = jobData,
                Filter=filter,
                PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalJobs, TotalPages = totalPages }
            };
            ViewData["Sort"] = sortType;
            ViewData["jobSum"] = currentApplied;
            ViewData["SelectList"] = new SelectList(new List<int> { 10, 25, 50, 100 },pageSize);
            ViewData["Selected"] = pageSize;
            ViewData["Filter"] = filter;
            return View(viewModel);
        }
        public IActionResult Add()
        {
            return PartialView("~/Views/Job/Add.cshtml");
        }
        [HttpPost]
        public IActionResult Add(Job newJob)
        {
            if (ModelState.IsValid)
            {
                _jobService.AddJob(newJob);
                return RedirectToAction("Index");
            }
            return View(newJob);
        }
        public IActionResult Edit(int id)
        {
            Job editJob = _jobService.GetJobById(id);
            if(editJob == null)
            {
                return RedirectToAction("Edit");
            }
            return PartialView("~/Views/Job/Edit.cshtml",editJob);
        }
        [HttpPost]
        public IActionResult Edit(Job updateJob)
        {
            if (ModelState.IsValid)
            {
                _jobService.UpdateJob(updateJob);
                return RedirectToAction("Index");
            }
            return View(updateJob);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Job deleteJob = _jobService.GetJobById(id);
            if (deleteJob == null)
            {
                return NotFound();
                
            }
            _jobService.DeleteJob(deleteJob);
            return RedirectToAction("Index");
        }
    }
}

