using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CareerTrack.Models;
using CareerTrack.Services;

namespace CareerTrack.Controllers;

public class HomeController : Controller
{
    private readonly IJobRepository _jobService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,IJobRepository jobService)
    {
        _logger = logger;
        _jobService = jobService;
    }

    public IActionResult Index()
    {
        ViewData["totalReplies"] = _jobService.GetTotalReplies();
        ViewData["totalApplied"] = _jobService.GetNumberofJobs();
        ViewData["mostApplied"] = _jobService.GetMostAppliedPosition();
        ViewData["totalmostApplied"] = _jobService.GetTotalMostAppliedPosition();
        ViewData["mostappliedLocation"] = _jobService.GetMostAppliedLocation();
        var dateAppliedChart = _jobService.GetDateAppliedView();
        List<DateTime> dates = new List<DateTime>(); ;
        List<int> dateCount = new List<int>();
        for(int i = 0; i < dateAppliedChart.Count(); i++)
        {
            dates.Add(dateAppliedChart.ElementAt(i).DateApplied);
            dateCount.Add(dateAppliedChart.ElementAt(i).DateAppliedCount);
        }
        
        ViewBag.Dates = dates;
        ViewBag.DateCount = dateCount;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

