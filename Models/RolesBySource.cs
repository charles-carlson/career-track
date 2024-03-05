using System;
using Microsoft.EntityFrameworkCore;

namespace CareerTrack.Models
{
	[Keyless]
	public class RolesBySource
	{
		public string? Position { get; set; }
		public string? Source { get; set; }
	}
}

