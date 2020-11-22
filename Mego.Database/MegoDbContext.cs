using System;
using System.Collections.Generic;
using Mego.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Mego.Database
{
    public class MegoDbContext : DbContext
    {
        public MegoDbContext(DbContextOptions<MegoDbContext> option) :  base(option)
        {
            Database.EnsureCreated();
        }

        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable("Report").HasKey(x => x.Id);
           
            modelBuilder.Entity<Report>()
                .Property(p => p.SummaryPrice)
                .HasColumnType("decimal(18,4)");

            var reports = new List<Report>()
            {
                new Report() {Id = 1, OrderDate = DateTime.Now.AddDays(-2), SummaryPrice = 2240.5m },
                new Report() {Id = 2, OrderDate = DateTime.Now, SummaryPrice = 64230 },
                new Report() {Id = 3, OrderDate = DateTime.Now.AddDays(-5), SummaryPrice = 32510.5242m },
                new Report() {Id = 4, OrderDate = DateTime.Now.AddDays(-17), SummaryPrice = 5001 },
                new Report() {Id = 5, OrderDate = DateTime.Now.AddDays(-10), SummaryPrice = 1 },
                new Report() {Id = 6, OrderDate = DateTime.Now.AddDays(-17), SummaryPrice = 5000 },
                new Report() {Id = 7, OrderDate = DateTime.Now.AddDays(-17), SummaryPrice = 1000 },
                new Report() {Id = 8, OrderDate = DateTime.Now.AddDays(-1), SummaryPrice = 41 },
                new Report() {Id = 9, OrderDate = DateTime.Now.AddDays(-7), SummaryPrice = 9999 },
                new Report() {Id = 10, OrderDate = DateTime.Now.AddDays(-117), SummaryPrice = 2057 }
            };

            modelBuilder.Entity<Report>().HasData(reports);
            base.OnModelCreating(modelBuilder);
        }
    }
}