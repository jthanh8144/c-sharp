using System;
using Microsoft.EntityFrameworkCore;
using AbbyWeb.Model;

namespace AbbyWeb.Data
{
	public class ApplicationDbContext: DbContext
	{
		public DbSet<Category> Categories { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
	}
}

