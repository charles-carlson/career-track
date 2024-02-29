using System;
using System.ComponentModel.DataAnnotations;

namespace CareerTrack.Models
{
	public class Job
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Position { get; set; }
		[Required]
		public string Languages { get; set; }
		[Required]
		public DateTime DateApplied { get; set; }
		[Required]
		public string Location { get; set; }
		[Required]
		public string IsWFH { get; set; }
		public string? Salary { get; set; }
		public string? ReplyBack { get; set; }
		public string? Interview { get; set; }
		public string? Offer { get; set; }
		public string? Source { get; set; }
	}
}