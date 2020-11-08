using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Recruitment.Data;
using Recruitment.Helper;
using Recruitment.Repository;

namespace Recruitment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContextPool<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("RecruitmentDBconnection")));
            //services.AddTransient<AppDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireDigit = false;
                option.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<SeedData>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IBioDataRepository, BioDataRepository>();
            services.AddTransient<IAcademicsRepository, AcademicsRepository>();
            services.AddTransient<IMaritalStatusRepository, MaritalStatusRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<ILevelRepository, LevelRepository>();
            services.AddTransient<IQualificationRepository, QualificationRepository>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IDocumentCategoryRepository, DocumentCategoryRepository>();
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<IIndustryRepository, IndustryRepository>();
            services.AddTransient<IJobRoleRepository, JobRoleRepository>();
            services.AddTransient<IJobTypeRepository, JobTypeRepository>();
            services.AddTransient<IExperienceRepository, ExperienceRepository>();
            services.AddTransient<IRecruitmentLocationRepository, RecruitmentLocationRepository>();
            services.AddTransient<IRecruitmentLocationTypeRepository, RecruitmentLocationTypeRepository>();
            services.AddTransient<IOrganizationDocumentRepository, OrganizationDocumentRepository>();
            services.AddTransient<IOrganizationDepartmentRepository, OrganizationDepartmentRepository>();
            services.AddTransient<IOrganizationJobRoleRepository, OrganizationJobRoleRepository>();
            services.AddTransient<IOrganizationRoleRepository, OrganizationRoleRepository>();
            services.AddTransient<IOrganizationUserRoleRepository, OrganizationUserRoleRepository>();
            services.AddTransient<IUserAccessTypeRepository, UserAccessTypeRepository>(); 
            services.AddTransient<IUserFunctionRepository, UserFunctionRepository>();
            services.AddTransient<IUserRoleAccessRepository, UserRoleAccessRepository>();
            services.AddTransient<IJobProfileElementRepository, JobProfileElementRepository>();
            services.AddTransient<IJobProfileRepository, JobProfileRepository>();
            services.AddTransient<IJobProfileDetailRepository, JobProfileDetailRepository>();
            services.AddTransient<Mailer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IMaritalStatusRepository statusRepository, IGenderRepository genderRepository,
            ILevelRepository levelRepository, IQualificationRepository qualificationRepository,
            IInstitutionRepository institutionRepository, ICourseRepository courseRepository,
            IDocumentCategoryRepository categoryRepository, IGradeRepository gradeRepository,
            IJobTypeRepository jobTypeRepository, IIndustryRepository industryRepository,
            IRecruitmentLocationTypeRepository locationTypeRepository, IUserAccessTypeRepository accessTypeRepository,
            IUserFunctionRepository functionRepository, IJobProfileElementRepository elementRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            SeedData.SeedDatas(roleManager, userManager, statusRepository,
                genderRepository, levelRepository,qualificationRepository,
                institutionRepository, courseRepository, categoryRepository,
                gradeRepository, industryRepository, jobTypeRepository,
                locationTypeRepository, accessTypeRepository, functionRepository,
                elementRepository);
        }
    }
}
