using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardiffMetroUni.StudentEnrollment.Infrastructure.Data
{

    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> configuration)
        {
            // The primary key is a backing field because it is not part of the domain model.
            //configuration.Property(p => p.Id).HasField("_id");
        }
    }


    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        //
        //  State:  DbSets
        public DbSet<Student> Students { get; set; }

        //
        //  DbContext Overrides
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
        }
    }
}