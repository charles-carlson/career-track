using System;
using CareerTrack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareerTrack.Data
{
	public class CareerDbContext : IdentityDbContext<IdentityUser>
    {
		public CareerDbContext(DbContextOptions<CareerDbContext> options) : base(options)
		{
		}
		public DbSet<Job> Job { get; set; }
		public DbSet<DateAppliedChart> DateAppliedChart { get; set; }
		public DbSet<DomainChart> DomainChart { get; set; }
		public DbSet<RolesBySource> RolesBySource { get; set; }
	}
}

