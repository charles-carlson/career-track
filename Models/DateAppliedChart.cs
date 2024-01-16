using System;
using Microsoft.EntityFrameworkCore;

namespace CareerTrack.Models
{
	[Keyless]
	public class DateAppliedChart
	{
        public DateTime DateApplied { get; set; }
		public int DateAppliedCount { get; set; }
	}
}

