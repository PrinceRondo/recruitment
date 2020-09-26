using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }
        public virtual DbSet<ApplicantAuthenticationInfo> ApplicantAuthenticationInfo { get; set; }
        public virtual DbSet<ApplicantBiodata> ApplicantBiodatas { get; set; }
        public virtual DbSet<ApplicantExperience> ApplicantExperience { get; set; }
        public virtual DbSet<ApplicantLevel> ApplicantLevels { get; set; }
        public virtual DbSet<ApplicantAcademics> ApplicantAcademics { get; set; }
        public virtual DbSet<ApplicantDocument> ApplicantDocuments { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<JobRoles> JobRoles { get; set; }
        public virtual DbSet<JobTypes> JobTypes { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<OrganizationDepartments> OrganizationDepartments { get; set; }
        public virtual DbSet<OrganizationProfile> OrganizationProfiles { get; set; }
        public virtual DbSet<OrganizationJobRoles> OrganisationJobRoles { get; set; }
        public virtual DbSet<OrganizationRoles> OrganizationRoles { get; set; }
        public virtual DbSet<OrganizationUserRole> OrganizationUserRoles { get; set; }
        public virtual DbSet<OrganizationUsersInfo> OrganizationUsersInfo { get; set; }
        public virtual DbSet<OrganizationDocument> OrganizationDocuments { get; set; }
        public virtual DbSet<VerificationStatus> VerificationStatus { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<ApplicantProfile> ApplicantProfiles { get; set; }
        public virtual DbSet<RecruitmentLocation> RecruitmentLocations { get; set; }
        public virtual DbSet<RecruitmentLocationType> RecruitmentLocationTypes { get; set; }
        public virtual DbSet<UserAccessType> UserAccessTypes { get; set; }
        public virtual DbSet<UserFunction> UserFunctions { get; set; }
    }
}
