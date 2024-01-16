using System;
using System.Net;
using CareerTrack.Data;
using CareerTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerTrack.Services
{
	public interface IJobRepository
	{
		IEnumerable<Job> GetJobs();
		int GetNumberofJobs();
		IEnumerable<Job> GetPagedJobs(int page, int pageSize, string filter);
		int GetNumberofPagedJobs(string filter);
		void AddJob(Job newJob);
		Job GetJobById(int id);
		void UpdateJob(Job updateJob);
		int GetTotalReplies();
		string GetMostAppliedPosition();
		int GetTotalMostAppliedPosition();
		string GetMostAppliedLocation();
	    IEnumerable<DateAppliedChart> GetDateAppliedView();
		

    }
	public class JobRepository : IJobRepository
	{
		private readonly CareerDbContext _careerDbContext;
        public JobRepository(CareerDbContext dbContext)
        {
            _careerDbContext = dbContext;
        }
		public IEnumerable<Job> GetJobs()
		{
			return _careerDbContext.Job.ToList();
		}
		public int GetNumberofJobs()
		{
			return _careerDbContext.Job.Count();
		}
		public int GetTotalReplies()
		{
			return _careerDbContext.Job.Count(j => j.ReplyBack != null);
		}
		public string GetMostAppliedLocation()
		{
			var location = _careerDbContext.Job.GroupBy(j => j.Location)
                .OrderByDescending(pos => pos.Count())
                .Take(1)
                .Select(g => g.Key).ToList();
			return location[0];
        }
		public int GetTotalMostAppliedPosition()
		{
            var position = _careerDbContext.Job.GroupBy(j => j.Position)
                   .OrderByDescending(pos => pos.Count())
                   .Take(1)
                   .Select(g => g.Count()).ToList();
            return position[0];
        }
        public string GetMostAppliedPosition()
		{
			var position = _careerDbContext.Job.GroupBy(j => j.Position)
				.OrderByDescending(pos => pos.Count())
				.Take(1)
				.Select(g => g.Key).ToList();

			return position[0];
		}
		public IEnumerable<Job> GetPagedJobs(int page, int pageSize, string filter)
		{
			Console.WriteLine(filter);
			var query = _careerDbContext.Job.AsQueryable();
			if (!string.IsNullOrEmpty(filter))
			{
				if(DateTime.TryParse(filter, out var filterDate))
				{
                    query = query.Where(item =>
						EF.Functions.Like(item.Name, $"%{filter}%") ||
						EF.Functions.Like(item.Position, $"%{filter}%") ||
						EF.Functions.Like(item.Languages, $"%{filter}%") ||
						(item.DateApplied >= filterDate && item.DateApplied < filterDate.AddDays(1)) ||
						EF.Functions.Like(item.Location, $"%{filter}%") ||
						EF.Functions.Like(item.IsWFH, $"%{filter}%") ||
						(item.Salary != null && EF.Functions.Like(item.Salary, $"%{filter}%")) ||
						(item.ReplyBack != null && EF.Functions.Like(item.ReplyBack, $"%{filter}%")) ||
						(item.Interview != null && EF.Functions.Like(item.Interview, $"%{filter}%")) ||
						(item.Offer != null && EF.Functions.Like(item.Offer, $"%{filter}%"))
						);
                }
                query = query.Where(item =>
					EF.Functions.Like(item.Name, $"%{filter}%") ||
					EF.Functions.Like(item.Position, $"%{filter}%") ||
					EF.Functions.Like(item.Languages, $"%{filter}%") ||
					EF.Functions.Like(item.Location, $"%{filter}%") ||
					EF.Functions.Like(item.IsWFH, $"%{filter}%") ||
					(item.Salary != null && EF.Functions.Like(item.Salary, $"%{filter}%")) ||
					(item.ReplyBack != null && EF.Functions.Like(item.ReplyBack, $"%{filter}%")) ||
					(item.Interview != null && EF.Functions.Like(item.Interview, $"%{filter}%")) ||
					(item.Offer != null && EF.Functions.Like(item.Offer, $"%{filter}%"))
					);


            }
			int skip = (page - 1) * pageSize;
			var pagedData = query.OrderBy(x=>x.Id).Skip(skip).Take(pageSize).ToList();
			return pagedData;
		}
		public int GetNumberofPagedJobs(string filter)
		{
			Console.WriteLine(filter);
            var query = _careerDbContext.Job.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                if (DateTime.TryParse(filter, out var filterDate))
                {
                    query = query.Where(item =>
                        EF.Functions.Like(item.Name, $"%{filter}%") ||
                        EF.Functions.Like(item.Position, $"%{filter}%") ||
                        EF.Functions.Like(item.Languages, $"%{filter}%") ||
                        (item.DateApplied >= filterDate && item.DateApplied < filterDate.AddDays(1)) ||
                        EF.Functions.Like(item.Location, $"%{filter}%") ||
                        EF.Functions.Like(item.IsWFH, $"%{filter}%") ||
                        (item.Salary != null && EF.Functions.Like(item.Salary, $"%{filter}%")) ||
                        (item.ReplyBack != null && EF.Functions.Like(item.ReplyBack, $"%{filter}%")) ||
                        (item.Interview != null && EF.Functions.Like(item.Interview, $"%{filter}%")) ||
                        (item.Offer != null && EF.Functions.Like(item.Offer, $"%{filter}%"))
                        );
                }
                query = query.Where(item =>
                    EF.Functions.Like(item.Name, $"%{filter}%") ||
                    EF.Functions.Like(item.Position, $"%{filter}%") ||
                    EF.Functions.Like(item.Languages, $"%{filter}%") ||
                    EF.Functions.Like(item.Location, $"%{filter}%") ||
                    EF.Functions.Like(item.IsWFH, $"%{filter}%") ||
                    (item.Salary != null && EF.Functions.Like(item.Salary, $"%{filter}%")) ||
                    (item.ReplyBack != null && EF.Functions.Like(item.ReplyBack, $"%{filter}%")) ||
                    (item.Interview != null && EF.Functions.Like(item.Interview, $"%{filter}%")) ||
                    (item.Offer != null && EF.Functions.Like(item.Offer, $"%{filter}%"))
                    );


            }
            return query.Count();
        }
		public async void AddJob(Job newJob)
		{
			_careerDbContext.Job.Add(newJob);
			await _careerDbContext.SaveChangesAsync();
		}
		public Job? GetJobById(int id)
		{
			var job = _careerDbContext.Job.Find(id);
            if(job == null)
			{
				return null;

            }
			return job;
		}
		public async void UpdateJob(Job updateJob)
		{
			_careerDbContext.Update(updateJob);
			await _careerDbContext.SaveChangesAsync();
		}
		public IEnumerable<DateAppliedChart> GetDateAppliedView()
		{
			return _careerDbContext.DateAppliedChart.ToList();
		}
		
    }
}

