using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite2.Models;

namespace WebSite2.Infrastructure
{
    public class MysqlDbContext : DbContext
    {
        public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options)
        {

        }
        public DbSet<Student> students { get; set; }
        public DbSet<RequestResponseLog> RRLogs { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=.;database=TestDb;user=root;password=123456;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RequestResponseLog>(ConfigureRequestResponseLog);

        }
        private void ConfigureRequestResponseLog(EntityTypeBuilder<RequestResponseLog> builder)
        {
            builder.ToTable("request_response_log");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(50)");

            builder.Property(e => e.CreateTime)
                .HasColumnName("create_time")
                .HasColumnType("datetime");
            builder.Property(e => e.ResponseJson)
                .HasColumnName("response_json")
                .HasColumnType("text");
            builder.Property(e => e.RequestJson)
                .HasColumnName("request_json")
                .HasColumnType("text");

        }
    }
}
