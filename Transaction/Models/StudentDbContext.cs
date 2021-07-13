using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Address>()
            //    .HasOne(p => p.Student)
            //    .WithOne(l => l.Address)
            //    .HasForeignKey("StudentId");
            //modelBuilder.Entity<Student>()
            //    .HasOne(p => p.Address)
            //    .WithOne(l => l.Student)
            //    .HasForeignKey("AddressId");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
