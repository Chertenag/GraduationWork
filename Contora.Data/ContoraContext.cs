using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Contora.Data;

public partial class ContoraContext : DbContext
{
    public ContoraContext()
    {
    }

    public ContoraContext(DbContextOptions<ContoraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Agentstatus> Agentstatuses { get; set; }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Target> Targets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(Settings.ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("agent");

            entity.HasIndex(e => e.PositionId, "agent_position_fk_idx");

            entity.HasIndex(e => e.RankId, "agent_rank_fk_idx");

            entity.HasIndex(e => e.DepartmentId, "department_id");

            entity.HasIndex(e => e.StatusId, "status_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.RankId).HasColumnName("rank_id");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("status_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Agents)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("agent_ibfk_1");

            entity.HasOne(d => d.Position).WithMany(p => p.Agents)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("agent_ibfk_3");

            entity.HasOne(d => d.Rank).WithMany(p => p.Agents)
                .HasForeignKey(d => d.RankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("agent_ibfk_2");

            entity.HasOne(d => d.Status).WithMany(p => p.Agents)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("agent_ibfk_4");
        });

        modelBuilder.Entity<Agentstatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("agentstatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("case");

            entity.HasIndex(e => e.DepartmentId, "department_id");

            entity.HasIndex(e => e.PrimaryAgentId, "primary_agent_id");

            entity.HasIndex(e => e.SecondaryAgentId, "secondary_agent_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateClose).HasColumnName("date_close");
            entity.Property(e => e.DateOpen).HasColumnName("date_open");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.PrimaryAgentId).HasColumnName("primary_agent_id");
            entity.Property(e => e.SecondaryAgentId).HasColumnName("secondary_agent_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Cases)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("case_ibfk_2");

            entity.HasOne(d => d.PrimaryAgent).WithMany(p => p.CasePrimaryAgents)
                .HasForeignKey(d => d.PrimaryAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("case_ibfk_3");

            entity.HasOne(d => d.SecondaryAgent).WithMany(p => p.CaseSecondaryAgents)
                .HasForeignKey(d => d.SecondaryAgentId)
                .HasConstraintName("case_ibfk_4");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("department");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("position");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rank");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Target>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("target");

            entity.HasIndex(e => e.CaseId, "case_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalInfo)
                .HasMaxLength(250)
                .HasColumnName("additional_info");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Case).WithMany(p => p.Targets)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("target_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
