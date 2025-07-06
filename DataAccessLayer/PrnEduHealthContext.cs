using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class PrnEduHealthContext : DbContext
{
    public PrnEduHealthContext()
    {
    }

    public PrnEduHealthContext(DbContextOptions<PrnEduHealthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GenderType> GenderTypes { get; set; }

    public virtual DbSet<HealthCheckup> HealthCheckups { get; set; }

    public virtual DbSet<HealthCheckupResult> HealthCheckupResults { get; set; }

    public virtual DbSet<HealthRecord> HealthRecords { get; set; }

    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<MedicalEventType> MedicalEventTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SeverityLevel> SeverityLevels { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VaccinationCampaign> VaccinationCampaigns { get; set; }

    public virtual DbSet<VaccinationRecord> VaccinationRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());
    
    //Using ConfigurationBuilder to init Configuration object  for reading appsettings.json file
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenderType>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__GenderTy__4E24E8175894AB7C");

            entity.ToTable("GenderType");

            entity.Property(e => e.GenderId)
                .ValueGeneratedNever()
                .HasColumnName("GenderID");
            entity.Property(e => e.GenderName).HasMaxLength(20);
        });

        modelBuilder.Entity<HealthCheckup>(entity =>
        {
            entity.HasKey(e => e.CheckupId).HasName("PK__HealthCh__626E853113D10785");

            entity.ToTable("HealthCheckup");

            entity.Property(e => e.CheckupId).HasColumnName("CheckupID");
            entity.Property(e => e.CheckupName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HealthCheckups)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__HealthChe__Creat__70DDC3D8");
        });

        modelBuilder.Entity<HealthCheckupResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__HealthCh__97690228340EF048");

            entity.ToTable("HealthCheckupResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.BloodPressure).HasMaxLength(20);
            entity.Property(e => e.Bmi)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("BMI");
            entity.Property(e => e.CheckupId).HasColumnName("CheckupID");
            entity.Property(e => e.CheckupTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DentalStatus).HasMaxLength(100);
            entity.Property(e => e.GeneralCondition).HasMaxLength(255);
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Vision).HasMaxLength(10);
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Checkup).WithMany(p => p.HealthCheckupResults)
                .HasForeignKey(d => d.CheckupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthChe__Check__76969D2E");

            entity.HasOne(d => d.Student).WithMany(p => p.HealthCheckupResults)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthChe__Stude__75A278F5");
        });

        modelBuilder.Entity<HealthRecord>(entity =>
        {
            entity.HasKey(e => e.HealthRecordId).HasName("PK__HealthRe__3BE0B89D771C4F59");

            entity.ToTable("HealthRecord");

            entity.HasIndex(e => e.StudentId, "UQ__HealthRe__32C52A781014DD7B").IsUnique();

            entity.Property(e => e.HealthRecordId).HasColumnName("HealthRecordID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Student).WithOne(p => p.HealthRecord)
                .HasForeignKey<HealthRecord>(d => d.StudentId)
                .HasConstraintName("FK__HealthRec__Stude__5FB337D6");
        });

        modelBuilder.Entity<Incident>(entity =>
        {
            entity.HasKey(e => e.IncidentId).HasName("PK__Incident__3D80539204AF0B7B");

            entity.ToTable("Incident");

            entity.Property(e => e.IncidentId).HasColumnName("IncidentID");
            entity.Property(e => e.IncidentDate).HasColumnType("datetime");
            entity.Property(e => e.IncidentTypeId).HasColumnName("IncidentTypeID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.SeverityId).HasColumnName("SeverityID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.IncidentType).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.IncidentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Incident__Incide__6477ECF3");

            entity.HasOne(d => d.ReportedByNavigation).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.ReportedBy)
                .HasConstraintName("FK__Incident__Report__656C112C");

            entity.HasOne(d => d.Severity).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.SeverityId)
                .HasConstraintName("FK__Incident__Severi__66603565");

            entity.HasOne(d => d.Student).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Incident__Studen__6383C8BA");
        });

        modelBuilder.Entity<MedicalEventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__MedicalE__A9216B1FF077FA72");

            entity.ToTable("MedicalEventType");

            entity.Property(e => e.EventTypeId)
                .ValueGeneratedNever()
                .HasColumnName("EventTypeID");
            entity.Property(e => e.EventTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A6235C0C1");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SeverityLevel>(entity =>
        {
            entity.HasKey(e => e.SeverityId).HasName("PK__Severity__C618A951CE4EC9D6");

            entity.ToTable("SeverityLevel");

            entity.Property(e => e.SeverityId)
                .ValueGeneratedNever()
                .HasColumnName("SeverityID");
            entity.Property(e => e.SeverityName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A79B5F8E24F");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");

            entity.HasOne(d => d.Gender).WithMany(p => p.Students)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__GenderI__59063A47");

            entity.HasOne(d => d.Parent).WithMany(p => p.Students)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Student__ParentI__59FA5E80");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE4435058");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4FE5158B4").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsFirstLogin).HasDefaultValue(true);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__5535A963");
        });

        modelBuilder.Entity<VaccinationCampaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PK__Vaccinat__3F5E8D79C11B4DEA");

            entity.ToTable("VaccinationCampaign");

            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.VaccineName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VaccinationCampaigns)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Vaccinati__Creat__693CA210");
        });

        modelBuilder.Entity<VaccinationRecord>(entity =>
        {
            entity.HasKey(e => e.VaccinationRecordId).HasName("PK__Vaccinat__AEF2975C938166A2");

            entity.ToTable("VaccinationRecord");

            entity.Property(e => e.VaccinationRecordId).HasColumnName("VaccinationRecordID");
            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.VaccinationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Campaign).WithMany(p => p.VaccinationRecords)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("FK__Vaccinati__Campa__6E01572D");

            entity.HasOne(d => d.Student).WithMany(p => p.VaccinationRecords)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Vaccinati__Stude__6D0D32F4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
