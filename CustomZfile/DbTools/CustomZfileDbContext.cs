using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomZfile.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomZfile.DbTools
{
	public class CustomZfileDbContext : DbContext
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DriveAllowUser>()
				.HasKey(c => new { c.drive_id, c.user_id });
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//配置mysql连接字符串
			optionsBuilder.UseMySql("Server=127.0.0.1;Database=custom_zfile; User=root;Password=root;");
		}

		//添加表实体
		public DbSet<User> user { get; set; }
		public DbSet<Drive> drive { get; set; }
		public DbSet<DriveAllowUser> drive_allow_user { get; set; }
	}
}
