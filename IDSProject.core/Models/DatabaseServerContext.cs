using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using IDSProject.Models;

namespace IDSProject.core.Models;

public partial class DatabaseServerContext : DbContext
{
    public DatabaseServerContext()
    {
    }

    public DatabaseServerContext(DbContextOptions<DatabaseServerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventGuide> EventGuides { get; set; }

    public virtual DbSet<EventMember> EventMembers { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<LookUp> LookUps { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CO13J4R\\SQLEXPRESS;Database=IDSProject;User Id=fa;Password=fathy;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC07B8BFCDB0");

            entity.ToTable("Event");

            entity.HasIndex(e => e.Name, "UQ__Event__72E12F1BD91E8D88").IsUnique();

            entity.Property(e => e.CategoryCode).HasColumnName("categoryCode");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.DateFrom).HasColumnName("dateFrom");
            entity.Property(e => e.DateTo).HasColumnName("dateTo");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Destination)
                .HasMaxLength(100)
                .HasColumnName("destination");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            // Configure the relationship with LookUp
            entity.HasOne(e => e.CategoryCodeNavigation)  // Navigation property
                .WithMany()  // Assuming LookUp does not have a collection of Events
                .HasForeignKey(e => e.CategoryCode)  // Foreign key property
                .HasConstraintName("FK_Event_LookUp_CategoryCode");  // Optional: name the foreign key constraint
        });

        modelBuilder.Entity<EventGuide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventGui__3214EC07585D5FEA");

            entity.ToTable("EventGuide");

            entity.Property(e => e.EventId).HasColumnName("eventId");
            entity.Property(e => e.GuideId).HasColumnName("guideId");

            entity.HasOne(d => d.Event).WithMany(p => p.EventGuides)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventGuid__event__6477ECF3");

            entity.HasOne(d => d.Guide).WithMany(p => p.EventGuides)
                .HasForeignKey(d => d.GuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventGuid__guide__6383C8BA");
        });

        modelBuilder.Entity<EventMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventMem__3214EC07EFE3F065");

            entity.ToTable("EventMember");

            entity.Property(e => e.EventId).HasColumnName("eventId");
            entity.Property(e => e.MemberId).HasColumnName("memberId");

            entity.HasOne(d => d.Event).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventMemb__event__68487DD7");

            entity.HasOne(d => d.Member).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventMemb__membe__6754599E");
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Guide__3214EC07057185A8");

            entity.ToTable("Guide");

            entity.HasIndex(e => e.Username, "UQ__Guide__F3DBC572C42661CE").IsUnique();

            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("fullName");
            entity.Property(e => e.JoiningDate).HasColumnName("joiningDate");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Profession)
                .HasMaxLength(100)
                .HasColumnName("profession");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<LookUp>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__LookUp__357D4CF8F0E6D5EE");

            entity.ToTable("LookUp");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Member__3214EC07E136927B");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ__Member__AB6E6164ED190933").IsUnique();

            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EmergencyNum)
                .HasMaxLength(20)
                .HasColumnName("emergencyNum");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("fullName");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.JoiningDate).HasColumnName("joiningDate");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .HasColumnName("mobileNumber");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("nationality");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Profession)
                .HasMaxLength(100)
                .HasColumnName("profession");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
