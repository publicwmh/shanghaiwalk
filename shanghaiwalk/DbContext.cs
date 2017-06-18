using System;
using Microsoft.EntityFrameworkCore;
using shanghaiwalk.model;
using shanghaiwalk.Baiye;

namespace shanghaiwalk
{
	public class BaiYeContext : DbContext
	{
		public BaiYeContext(DbContextOptions<BaiYeContext> options) : base(options)
		{
		}

		public DbSet<BaiyeBookPage> BaiYeBookPages { get; set; }
        public DbSet<POI> POIs { get; set; }
        public DbSet<QueryHis> QueryHiss { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BaiyeBookPage>().ToTable("baiyebookpage");
            modelBuilder.Entity<POI>().ToTable("poi");
            modelBuilder.Entity<QueryHis>().ToTable("queryhis");
        }
	}
}
