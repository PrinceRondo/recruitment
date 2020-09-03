using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Recruitment.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicantAuthenticationInfo> ApplicantAuthenticationInfo { get; set; }
        public virtual DbSet<ApplicantBiodataInfo> ApplicantBiodataInfo { get; set; }
        public virtual DbSet<ApplicantExperienceInfo> ApplicantExperienceInfo { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Industry> Industry { get; set; }
        public virtual DbSet<JobRoles> JobRoles { get; set; }
        public virtual DbSet<JobTypes> JobTypes { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<OrganisationDepartments> OrganisationDepartments { get; set; }
        public virtual DbSet<OrganisationInfo> OrganisationInfo { get; set; }
        public virtual DbSet<OrganisationJobRoles> OrganisationJobRoles { get; set; }
        public virtual DbSet<OrganisationRoles> OrganisationRoles { get; set; }
        public virtual DbSet<OrganisationUsersInfo> OrganisationUsersInfo { get; set; }
        public virtual DbSet<VerificationStatus> VerificationStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=Recruitment;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Trusted_Connection=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantAuthenticationInfo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.IsEmailVerified).IsUnicode(false);

                entity.Property(e => e.IsEmailVerifiedDate)
                    .HasColumnName("isEmailVerifiedDate")
                    .HasColumnType("date");

                entity.Property(e => e.LastLoginDate).HasColumnType("date");

                entity.Property(e => e.LastPasswordChangedDate).HasColumnType("date");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<ApplicantAuthenticationInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicantAuthenticationInfo_ApplicantAuthenticationInfo");
            });

            modelBuilder.Entity<ApplicantBiodataInfo>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.LastUpdated).HasColumnType("date");

                entity.Property(e => e.OtherName).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.ApplicantBiodataInfo)
                    .HasForeignKey(d => d.Gender)
                    .HasConstraintName("FK_ApplicantBiodataInfo_Gender");

                entity.HasOne(d => d.MaritalStatusNavigation)
                    .WithMany(p => p.ApplicantBiodataInfo)
                    .HasForeignKey(d => d.MaritalStatus)
                    .HasConstraintName("FK_ApplicantBiodataInfo_MaritalStatus");
            });

            modelBuilder.Entity<ApplicantExperienceInfo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyName).HasMaxLength(10);

                entity.Property(e => e.DateAdded).HasColumnType("date");

                entity.Property(e => e.JobDescription).IsUnicode(false);

                entity.Property(e => e.StartDate).IsUnicode(false);

                entity.Property(e => e.StopDate).IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ApplicantExperienceInfo)
                    .HasForeignKey<ApplicantExperienceInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicantExperienceInfo_Industry");

                entity.HasOne(d => d.JobRole)
                    .WithMany(p => p.ApplicantExperienceInfo)
                    .HasForeignKey(d => d.JobRoleId)
                    .HasConstraintName("FK_ApplicantExperienceInfo_JobRoles");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.ApplicantExperienceInfo)
                    .HasForeignKey(d => d.JobTypeId)
                    .HasConstraintName("FK_ApplicantExperienceInfo_JobTypes");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Industry>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<JobRoles>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Industry)
                    .WithMany(p => p.JobRoles)
                    .HasForeignKey(d => d.IndustryId)
                    .HasConstraintName("FK_JobRoles_Industry");
            });

            modelBuilder.Entity<JobTypes>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.Property(e => e.Status).IsUnicode(false);
            });

            modelBuilder.Entity<OrganisationDepartments>(entity =>
            {
                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.DepartmentName).IsUnicode(false);
            });

            modelBuilder.Entity<OrganisationInfo>(entity =>
            {
                entity.Property(e => e.Abbreviation).IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.HeadQuarterAddress).IsUnicode(false);

                entity.Property(e => e.IsEmailVerifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.HasOne(d => d.Industry)
                    .WithMany(p => p.OrganisationInfo)
                    .HasForeignKey(d => d.IndustryId)
                    .HasConstraintName("FK_OrganizationInfo_Industry");
            });

            modelBuilder.Entity<OrganisationJobRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.JobRoleName).IsUnicode(false);

                entity.Property(e => e.RoleLevel).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.OrganisationJobRoles)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_OrganisationJobRoles_OrganisationDepartments");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.OrganisationJobRoles)
                    .HasForeignKey<OrganisationJobRoles>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrganisationJobRoles_OrganisationInfo");
            });

            modelBuilder.Entity<OrganisationRoles>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.OrganisationRoleName).IsUnicode(false);
            });

            modelBuilder.Entity<OrganisationUsersInfo>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.Firstname).IsUnicode(false);

                entity.Property(e => e.LastLoginDate).HasColumnType("date");

                entity.Property(e => e.LastPasswordChangedDate).HasColumnType("date");

                entity.Property(e => e.Lastname).IsUnicode(false);

                entity.Property(e => e.Othername).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.StaffOrganisationId)
                    .HasColumnName("StaffOrganisationID")
                    .IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Organisation)
                    .WithMany(p => p.OrganisationUsersInfo)
                    .HasForeignKey(d => d.OrganisationId)
                    .HasConstraintName("FK_OrganisationUsersInfo_OrganisationInfo");

                entity.HasOne(d => d.OrganisationRole)
                    .WithMany(p => p.OrganisationUsersInfo)
                    .HasForeignKey(d => d.OrganisationRoleId)
                    .HasConstraintName("FK_OrganisationUsersInfo_OrganisationRoles");
            });

            modelBuilder.Entity<VerificationStatus>(entity =>
            {
                entity.Property(e => e.StatusName).IsUnicode(false);
            });
        }
    }
}
