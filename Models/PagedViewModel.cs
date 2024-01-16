using System;
namespace CareerTrack.Models
{
	public class PagedViewModel
	{
        //private Job _job;
        public IEnumerable<Job> Data { get; set; }
        public PageInfo PageInfo { get; set; }
        public string Filter { get; set; }
       /* public string GetShortDate
        {
            get
            {
                return _job.DateApplied.ToString("dd/MM/yyyy");
            }
        }
        public PagedViewModel(Job job)
        {
            _job = job;
        }
       */
    }
	public class PageInfo
	{
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}

