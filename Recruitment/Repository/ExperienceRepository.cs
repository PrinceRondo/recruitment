using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModels;

namespace Recruitment.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public ExperienceRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantExperience experience = await dbContext.ApplicantExperience.FindAsync(id);
                if (experience != null)
                {
                    dbContext.ApplicantExperience.Remove(experience);
                    await dbContext.SaveChangesAsync();
                    response.message = "Record deleted successfully";
                    response.code = 200;
                }
                else
                {
                    response.message = "Record not found";
                    response.code = 404;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.ApplicantExperience.Local.Clear();
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            return response;
        }

        public async Task<IEnumerable<ApplicantExperienceViewModel>> GetAllApplicantExperience()
        {
            return await dbContext.ApplicantExperience.Select(x => new ApplicantExperienceViewModel
            {
                DateAdded = x.DateAdded,
                DateUpdated = x.DateUpdated,
                CompanyName = x.CompanyName,
                Industry = x.Industry.Name,
                IndustryId = x.IndustryId,
                JobDescription = x.JobDescription,
                JobRole = x.JobRole.Name,
                JobRoleId = x.JobRoleId,
                JobType = x.JobType.Name,
                JobTypeId = x.JobTypeId,
                StartDate = x.StartDate,
                StopDate = x.StopDate,
                Id = x.Id,
                userId = x.UserId
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantExperienceViewModel>> GetAllByIndustryId(int industryId)
        {
            return await dbContext.ApplicantExperience.Where(x=>x.IndustryId == industryId).Select(x => new ApplicantExperienceViewModel
            {
                DateAdded = x.DateAdded,
                DateUpdated = x.DateUpdated,
                CompanyName = x.CompanyName,
                Industry = x.Industry.Name,
                IndustryId = x.IndustryId,
                JobDescription = x.JobDescription,
                JobRole = x.JobRole.Name,
                JobRoleId = x.JobRoleId,
                JobType = x.JobType.Name,
                JobTypeId = x.JobTypeId,
                StartDate = x.StartDate,
                StopDate = x.StopDate,
                Id = x.Id,
                userId = x.UserId
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantExperienceViewModel>> GetAllByRoleId(long roleId)
        {
            return await dbContext.ApplicantExperience.Where(x => x.JobRoleId == roleId).Select(x => new ApplicantExperienceViewModel
            {
                DateAdded = x.DateAdded,
                DateUpdated = x.DateUpdated,
                CompanyName = x.CompanyName,
                Industry = x.Industry.Name,
                IndustryId = x.IndustryId,
                JobDescription = x.JobDescription,
                JobRole = x.JobRole.Name,
                JobRoleId = x.JobRoleId,
                JobType = x.JobType.Name,
                JobTypeId = x.JobTypeId,
                StartDate = x.StartDate,
                StopDate = x.StopDate,
                Id = x.Id,
                userId = x.UserId
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantExperienceViewModel>> GetAllByUserId(string userId)
        {
            return await dbContext.ApplicantExperience.Where(x => x.UserId == userId).Select(x => new ApplicantExperienceViewModel
            {
                DateAdded = x.DateAdded,
                DateUpdated = x.DateUpdated,
                CompanyName = x.CompanyName,
                Industry = x.Industry.Name,
                IndustryId = x.IndustryId,
                JobDescription = x.JobDescription,
                JobRole = x.JobRole.Name,
                JobRoleId = x.JobRoleId,
                JobType = x.JobType.Name,
                JobTypeId = x.JobTypeId,
                StartDate = x.StartDate,
                StopDate = x.StopDate,
                Id = x.Id,
                userId = x.UserId
            }).ToListAsync();
        }

        public async Task<ApplicantExperienceViewModel> GetExperienceById(long id)
        {
            return await dbContext.ApplicantExperience.Where(x => x.Id == id).Select(x => new ApplicantExperienceViewModel
            {
                DateAdded = x.DateAdded,
                DateUpdated = x.DateUpdated,
                CompanyName = x.CompanyName,
                Industry = x.Industry.Name,
                IndustryId = x.IndustryId,
                JobDescription = x.JobDescription,
                JobRole = x.JobRole.Name,
                JobRoleId = x.JobRoleId,
                JobType = x.JobType.Name,
                JobTypeId = x.JobTypeId,
                StartDate = x.StartDate,
                StopDate = x.StopDate,
                Id = x.Id,
                userId = x.UserId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(ApplicantExperienceViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var user = await userManager.FindByIdAsync(model.userId);
                if (user != null)
                {
                    ApplicantExperience experience = new ApplicantExperience()
                    {
                        CompanyName = model.CompanyName,
                        IndustryId = model.IndustryId,
                        JobDescription = model.JobDescription,
                        JobRoleId = model.JobRoleId,
                        JobTypeId = model.JobTypeId,
                        StartDate = model.StartDate,
                        StopDate = model.StopDate,
                        UserId = model.userId,
                        DateAdded = DateTime.Now,
                        DateUpdated = DateTime.Now,
                    };
                    dbContext.ApplicantExperience.Add(experience);
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Work experience saved successfully";
                }
                else
                {
                    response.code = 404;
                    response.message = "User doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.ApplicantExperience.Local.Clear();
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(long id, ApplicantExperienceViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantExperience experience = await dbContext.ApplicantExperience.FirstOrDefaultAsync(x => x.Id == id);
                if (experience != null)
                {
                    experience.CompanyName = model.CompanyName;
                    experience.DateUpdated = DateTime.Now;
                    experience.IndustryId = model.IndustryId;
                    experience.JobDescription = model.JobDescription;
                    experience.JobRoleId = model.JobRoleId;
                    experience.JobTypeId = model.JobTypeId;
                    experience.StartDate = model.StartDate;
                    experience.StopDate = model.StopDate;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Work experience updated successfully";
                }
                else
                {
                    response.code = 404;
                    response.message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 400;
                dbContext.ApplicantExperience.Local.Clear();
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            return response;
        }
    }
}
