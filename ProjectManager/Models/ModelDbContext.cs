using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
	public class ModelDbContext : DbContext
	{
		//模型上下文
		public DbSet<Staff> Staffs { get; set; }
		public DbSet<PBInfo> PbInfoes { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<ProjectInfo> ProjectInfoes { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//将模型映射到自己指定的表
			modelBuilder.Configurations.Add(new EntityTypeConfiguration<Staff>().ToTable("Staff"));
			modelBuilder.Configurations.Add(new EntityTypeConfiguration<PBInfo>().ToTable("PBInfo"));
            modelBuilder.Configurations.Add(new EntityTypeConfiguration<Domain> ().ToTable("Domain"));
            modelBuilder.Configurations.Add ( new EntityTypeConfiguration<ProjectInfo>().ToTable("ProjectInfo"));
            modelBuilder.Configurations.Add(new EntityTypeConfiguration<WorkHour>().ToTable("WorkHours"));
            base.OnModelCreating(modelBuilder);
		}
	}
}