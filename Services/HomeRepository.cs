using System;
using System.Linq;
using System.Text.RegularExpressions;
using CareerTrack.Data;
using CareerTrack.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace CareerTrack.Services
{
	public interface IHomeRepository
	{
		int GetNumberofPositionBySource(string source);
		IEnumerable<RoleChartBySource> GetRolesBySource(string source);
	}

    public class HomeRepository
	{
        private readonly CareerDbContext _careerDbContext;
		public HomeRepository(CareerDbContext dbContext)
		{
			_careerDbContext = dbContext;
        }
        public int GetNumberofPositionBySource(string source)
        {
            return 0;
        }

        public List<RoleChartBySource> GetRolesBySource(string source)
        {
            var result = _careerDbContext.RolesBySource
                .Where(item => EF.Functions.Like(item.Source, $"%{source}%"))
                .GroupBy(item => item.Position)
                .Select(group => new RoleChartBySource
                       {
                        Position = group.Key,
                        Count = group.Count(),
                        })
                .ToList();
            return result;
        }
    }
}

