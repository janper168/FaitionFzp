using JKF.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.DbContexts
{

    

    /// <summary>
    /// 定义基础的DbContext
    /// </summary>
    public partial class BaseDbContext : DbContext
    {
        //连接字符串字段
        private static string _dbConnectionString = @"server=localhost,3306;database=ftbasicdb;uid=sa;pwd=123qwe!@#";

        //连接字符从的属性，通过外部属性设定内部字段
        public static string DbConnectionString 
        { 
            get
            { 
                return _dbConnectionString;
            }
            set 
            {
                _dbConnectionString = value;
            }
        
        }

        public BaseDbContext()
        {
        }

        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseMySql(_dbConnectionString, new MySqlServerVersion(new Version(8, 0, 3)));
            //.UseSqlServer(_dbConnectionString);              
            optionsBuilder
                .ConfigureWarnings(warnnings => warnnings.Log(CoreEventId.DetachedLazyLoadingWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostUser>()
            .HasKey(bc => new { bc.PostId, bc.UserId });

            modelBuilder.Entity<PostUser>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.PostUserList)
                .HasForeignKey(bc => bc.PostId);

            modelBuilder.Entity<PostUser>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.PostUserList)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<RoleUser>()
            .HasKey(bc => new { bc.RoleId, bc.UserId });

            modelBuilder.Entity<RoleUser>()
                .HasOne(bc => bc.Role)
                .WithMany(b => b.RoleUserList)
                .HasForeignKey(bc => bc.RoleId);

            modelBuilder.Entity<RoleUser>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.RoleUserList)
                .HasForeignKey(bc => bc.UserId);

        }
    }
}
