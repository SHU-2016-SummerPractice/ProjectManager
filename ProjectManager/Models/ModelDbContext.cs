﻿using System;
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

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//将模型映射到自己指定的表
			modelBuilder.Configurations.Add(new EntityTypeConfiguration<Staff>().ToTable("Staff"));
			base.OnModelCreating(modelBuilder);
		}
	}
}