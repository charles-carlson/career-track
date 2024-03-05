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
        var domainChart = _jobService.GetDomainChartView();
        List<string> domains = new List<string>();
        List<int> domainCount = new List<int>();
        List<DateTime> dates = new List<DateTime>(); ;
        List<int> dateCount = new List<int>();
        for(int i = 0; i < dateAppliedChart.Count(); i++)
        {
            dates.Add(dateAppliedChart.ElementAt(i).DateApplied);
            dateCount.Add(dateAppliedChart.ElementAt(i).DateAppliedCount);
        }
        int other = 0;
        for(int i = 0; i < domainChart.Count(); i++)
        {
            if(domainChart.ElementAt(i).Domain != null)
            {
                if(domainChart.ElementAt(i).Domain_count == 1)
                {
                    other += 1;
                }
                else
                {
                    domains.Add(domainChart.ElementAt(i).Domain);
                    domainCount.Add(domainChart.ElementAt(i).Domain_count);
                }

            }
            
        }
        domains.Add("Other");
        domainCount.Add(other);
        ViewBag.Domains = domains;
        ViewBag.DomainCount = domainCount;
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

