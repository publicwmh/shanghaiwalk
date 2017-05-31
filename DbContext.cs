using System;
using Microsoft.EntityFrameworkCore;
using shanghaiwalk.model;

namespace shanghaiwalk
{
	public class BaiYeContext : DbContext
	{
		public BaiYeContext(DbContextOptions<BaiYeContext> options) : base(options)
		{
		}

		public DbSet<BaiyeBookPage> BaiYeBookPages { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BaiyeBookPage>().ToTable("baiyebookpage"); 
		}
	}
}
