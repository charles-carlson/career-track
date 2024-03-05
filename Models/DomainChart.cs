using System;
using Microsoft.EntityFrameworkCore;
namespace CareerTrack.Models
{
	[Keyless]
	public class DomainChart
	{
		public string? Domain { get; set; }
		public int Domain_count { get; set; }
	}
}

