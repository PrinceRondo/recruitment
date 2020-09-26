using Microsoft.AspNetCore.Identity;
using Recruitment.Models;
using Recruitment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Data
{
    public class SeedData
    {
        public static void SeedDatas(
           RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
           IMaritalStatusRepository statusRepository, IGenderRepository genderRepository,
           ILevelRepository levelRepository, IQualificationRepository qualificationRepository,
           IInstitutionRepository institutionRepository, ICourseRepository courseRepository,
           IDocumentCategoryRepository categoryRepository, IGradeRepository gradeRepository,
           IIndustryRepository industryRepository, IJobTypeRepository typeRepository,
           IRecruitmentLocationTypeRepository LocationtypeRepository, IUserAccessTypeRepository accessTypeRepository,
           IUserFunctionRepository functionRepository
           )
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedGender(genderRepository);
            SeedMaritalStatus(statusRepository);
            SeedApplicantLevel(levelRepository);
            SeedQualification(qualificationRepository);
            SeedInstitution(institutionRepository);
            SeedCourse(courseRepository);
            SeedDocumentCategory(categoryRepository);
            SeedGrade(gradeRepository);
            SeedIndustry(industryRepository);
            SeedJobType(typeRepository);
            SeedRecruitmentLocationType(LocationtypeRepository);
            SeedUserAccessType(accessTypeRepository);
            SeedUserFunction(functionRepository);
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                IdentityRole adminRole = new IdentityRole();

                adminRole.Name = "admin";
                adminRole.NormalizedName = "ADMIN";
                IdentityResult adminRoleResult = roleManager.CreateAsync(adminRole).Result;
            }
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                IdentityRole apiUserRole = new IdentityRole();

                apiUserRole.Name = "user";
                apiUserRole.NormalizedName = "USER";
                IdentityResult apiUserRoleResult = roleManager.CreateAsync(apiUserRole).Result;
            }
            if (!roleManager.RoleExistsAsync("organization").Result)
            {
                IdentityRole apiUserRole = new IdentityRole();

                apiUserRole.Name = "organization";
                apiUserRole.NormalizedName = "ORGANIZATION";
                IdentityResult apiUserRoleResult = roleManager.CreateAsync(apiUserRole).Result;
            }
            if (!roleManager.RoleExistsAsync("organizationuser").Result)
            {
                IdentityRole apiUserRole = new IdentityRole();

                apiUserRole.Name = "organizationuser";
                apiUserRole.NormalizedName = "ORGANIZATIONUSER";
                IdentityResult apiUserRoleResult = roleManager.CreateAsync(apiUserRole).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {

            if (userManager.FindByNameAsync("admin").Result == null)
            {

                ApplicationUser adminUser = new ApplicationUser();
                adminUser.UserName = "admin";
                adminUser.Email = "admin@admin.com";
                adminUser.EmailConfirmed = true;

                try
                {
                    IdentityResult adminUserResult = userManager.CreateAsync(adminUser, "password").Result;
                    if (adminUserResult.Succeeded)
                    {

                        userManager.AddToRoleAsync(adminUser, "admin").Wait();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }
        public static void SeedGender(IGenderRepository genderRepository)
        {
            string[] genderList = new string[] { "Male", "Female"};
            foreach (string genders in genderList)
            {
                if (genderRepository.FindByNameAsync(genders).Result == null)
                {
                    Gender gender = new Gender()
                    {
                        Name = genders
                    };
                    try
                    {
                        genderRepository.SaveAsync(gender);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        public static void SeedMaritalStatus(IMaritalStatusRepository statusRepository)
        {
            string[] statusList = new string[] { "Single", "Married", "Divorced", "Widow" };
            foreach (string marital in statusList)
            {
                if (statusRepository.FindByNameAsync(marital).Result == null)
                {
                    MaritalStatus status = new MaritalStatus()
                    {
                        Status = marital
                    };
                    try
                    {
                        statusRepository.SaveAsync(status);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedApplicantLevel(ILevelRepository levelRepository)
        {
            string[] levelList = new string[] { "Level 0", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8" };
            foreach (string level in levelList)
            {
                if (levelRepository.FindByNameAsync(level).Result == null)
                {
                    ApplicantLevel applicantLevel = new ApplicantLevel()
                    {
                        Level = level
                    };
                    try
                    {
                        levelRepository.SaveAsync(applicantLevel);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedQualification(IQualificationRepository qualificationRepository)
        {
            string[] qualificationList = new string[] { "FSLC-2", "SSCE-3", "OND/NCE-4", "HND/BSC-5", "Master-6", "PHD-7"};
            foreach (string level in qualificationList)
            {
                string[] qualification = level.Split("-");
                if (qualificationRepository.FindByNameAsync(qualification[0]).Result == null)
                {
                    Qualification qualifications = new Qualification()
                    {
                        Name = qualification[0],
                        ApplicantLevelId = Convert.ToInt16(qualification[1])
                    };
                    try
                    {
                        qualificationRepository.SaveAsync(qualifications);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedInstitution(IInstitutionRepository institutionRepository)
        {
            string[] institutionList = new string[] { "Other","University of Lagos", "Federal Polytechnic Offa", "College of Education Oro" };
            foreach (string institutions in institutionList)
            {
                if (institutionRepository.FindByNameAsync(institutions).Result == null)
                {
                    Institution institution = new Institution()
                    {
                        Name = institutions
                    };
                    try
                    {
                        institutionRepository.SaveAsync(institution);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedCourse(ICourseRepository courseRepository)
        {
            string[] courseList = new string[] { "Other", "Law", "Computer Engineering", "Arabic Education" };
            foreach (string courses in courseList)
            {
                if (courseRepository.FindByNameAsync(courses).Result == null)
                {
                    Course course = new Course()
                    {
                        Name = courses
                    };
                    try
                    {
                        courseRepository.SaveAsync(course);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedDocumentCategory(IDocumentCategoryRepository categoryRepository)
        {
            string[] categoryList = new string[] { "Academic", "Professional", "Industry" };
            foreach (string category in categoryList)
            {
                if (categoryRepository.FindByNameAsync(category).Result == null)
                {
                    DocumentCategory document = new DocumentCategory()
                    {
                        Name = category
                    };
                    try
                    {
                        categoryRepository.SaveAsync(document);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        public static void SeedIndustry(IIndustryRepository industryRepository)
        {
            string[] industryList = new string[] { "Banking", "Education", "IT", "Agriculture","Manufacturing" };
            foreach (string industry in industryList)
            {
                if (industryRepository.FindByNameAsync(industry).Result == null)
                {
                    Industry objIndustry = new Industry()
                    {
                        Name = industry
                    };
                    try
                    {
                        industryRepository.SaveAsync(objIndustry);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedJobType(IJobTypeRepository typeRepository)
        {
            string[] typeList = new string[] { "Full time", "Part time", "Contract", "Intern", "Remote" };
            foreach (string type in typeList)
            {
                if (typeRepository.FindByNameAsync(type).Result == null)
                {
                    JobTypes objType = new JobTypes()
                    {
                        Name = type
                    };
                    try
                    {
                        typeRepository.SaveAsync(objType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedGrade(IGradeRepository gradeRepository)
        {
            string[] gradeList = new string[] { "Other","First Class", "Second Class(Upper)", "Second Class(Lower)","Third Class","Pass",
            "Distinction", "Upper Credit", "Lower Credit",};
            foreach (string grade in gradeList)
            {
                if (gradeRepository.FindByNameAsync(grade).Result == null)
                {
                    Grade newGrade = new Grade()
                    {
                        Name = grade
                    };
                    try
                    {
                        gradeRepository.SaveAsync(newGrade);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedRecruitmentLocationType(IRecruitmentLocationTypeRepository typeRepository)
        {
            string[] typeList = new string[] { "Head Office", "Branch"};
            foreach (string type in typeList)
            {
                if (typeRepository.FindByNameAsync(type).Result == null)
                {
                    RecruitmentLocationType newLocation = new RecruitmentLocationType()
                    {
                        LocationType = type
                    };
                    try
                    {
                        typeRepository.SaveAsync(newLocation);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedUserAccessType(IUserAccessTypeRepository typeRepository)
        {
            string[] typeList = new string[] { "Create", "Read", "Update", "Delete", "Download" };
            foreach (string type in typeList)
            {
                if (typeRepository.FindByNameAsync(type).Result == null)
                {
                    UserAccessType newType = new UserAccessType()
                    {
                        type = type
                    };
                    try
                    {
                        typeRepository.SaveAsync(newType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedUserFunction(IUserFunctionRepository functionRepository)
        {
            string[] functionList = new string[] { "Manage Job Vacancy", "Manage Job Postings", "Manage Interview Templates",
                "Manage Interview Rooms", "Manage Job Profiles", "Process Applications", "Manage Schedules",
                "Schedule Tests and Interviews", "Manage Reports", "Review Job Postings", "Review Job Profiles",
                "Review Applications", "Review Templates", "Review Schedules", "Review  Tests and Interviews",
                "Approve Applications", "Manage Schedules", "Manage Tasks and Events", "Submit Interview Scores",
                "Manage Reports", "Manage Tasks and Events","Manage Profile"};
            foreach (string func in functionList)
            {
                if (functionRepository.FindByNameAsync(func).Result == null)
                {
                    UserFunction newFunction = new UserFunction()
                    {
                        Function = func
                    };
                    try
                    {
                        functionRepository.SaveAsync(newFunction);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
