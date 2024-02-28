using System;
using System.Collections.Generic;
using BlazorComponentsDemo.DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorComponentsDemo.DataModels.Data;

public partial class EFTestDataDBContext : DbContext
{
    public EFTestDataDBContext()
    {
    }

    public EFTestDataDBContext(DbContextOptions<EFTestDataDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PeopleTestData> PeopleTestData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=2ZGBDY3-LT\\SQL2022;Database=EFTestData;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PeopleTestData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PeopleTe__3214EC27FFC473A5");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BirthDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Email).HasMaxLength(70);
            entity.Property(e => e.FirstName).HasMaxLength(70);
            entity.Property(e => e.LastName).HasMaxLength(70);
            entity.Property(e => e.Status).HasMaxLength(70);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
