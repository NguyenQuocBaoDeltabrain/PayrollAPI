﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace SuperHeroAPI.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<Staff> Staffs { get; set; } = null!;

        public DbSet<OverTime> OverTimes { get; set; } = null!;

        public DbSet<Salary> Salaries { get; set; } = null!;


    }
}