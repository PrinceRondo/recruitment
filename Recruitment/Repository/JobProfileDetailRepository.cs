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
using static Recruitment.Helper.UserAccess;

namespace Recruitment.Repository
{
    public class JobProfileDetailRepository : IJobProfileDetailRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public JobProfileDetailRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> DeleteAsync(long id, DeleteViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool deleteAccess = false;
                //check if user exist
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    //get user roles
                    var userRoles = await userManager.GetRolesAsync(user);
                    //get all user access
                    IList<UserRoleFunctionAccess> functionAccess = await dbContext.UserRoleFunctionAccess.Where(x => x.RoleId == model.OrganizationUserRoleId).ToListAsync();
                    if (functionAccess.Count() > 0)
                    {
                        foreach (UserRoleFunctionAccess access in functionAccess)
                        {
                            if (access.FunctionId == (int)Function.Manage_Job_Profiles && access.AccessId == (int)Access.Delete)
                            {
                                deleteAccess = true;
                            }
                        }
                    }
                    //check if user has access right to delete job profile
                    if (userRoles.Contains("organization") || (userRoles.Contains("organizationuser") && deleteAccess == true))
                    {
                        JobProfileDetail profile = await dbContext.JobProfileDetails.FindAsync(id);
                        if (profile != null)
                        {
                            dbContext.JobProfileDetails.Remove(profile);
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
                    else
                    {
                        response.code = 401;
                        response.message = "You don't have access to delete job profile";
                    }
                }
                else
                {
                    response.code = 405;
                    response.message = "User doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 400;
                dbContext.JobProfileDetails.Local.Clear();
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

        public async Task<IEnumerable<JobProfileDetailViewModel>> GetAll()
        {
            return await dbContext.JobProfileDetails.Select(x => new JobProfileDetailViewModel
            {
                Comment = x.Comment,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                IsMandatory = x.IsMandatory,
                Id = x.Id,
                JobElementId = x.JobElementId,
                JobProfile = x.JobProfile.Description,
                JobProfileElement = x.JobProfileElement.Element,
                JobProfileId = x.JobProfileId,
                MaxRequirement = x.MaxRequirement,
                MinRequirement = x.MinRequirement,
                LastUpdated = x.LastUpdated,
                Organization = x.JobProfile.Organization.CompanyName,
                OrganizationId = x.JobProfile.OrganizationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<JobProfileDetailViewModel>> GetAllByElementId(long elementId)
        {
            return await dbContext.JobProfileDetails.Where(x=>x.JobElementId == elementId).Select(x => new JobProfileDetailViewModel
            {
                Comment = x.Comment,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                IsMandatory = x.IsMandatory,
                Id = x.Id,
                JobElementId = x.JobElementId,
                JobProfile = x.JobProfile.Description,
                JobProfileElement = x.JobProfileElement.Element,
                JobProfileId = x.JobProfileId,
                MaxRequirement = x.MaxRequirement,
                MinRequirement = x.MinRequirement,
                LastUpdated = x.LastUpdated,
                Organization = x.JobProfile.Organization.CompanyName,
                OrganizationId = x.JobProfile.OrganizationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<JobProfileDetailViewModel>> GetAllByJobProfileId(long jobProfileId)
        {
            return await dbContext.JobProfileDetails.Where(x => x.JobProfileId == jobProfileId).Select(x => new JobProfileDetailViewModel
            {
                Comment = x.Comment,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                IsMandatory = x.IsMandatory,
                Id = x.Id,
                JobElementId = x.JobElementId,
                JobProfile = x.JobProfile.Description,
                JobProfileElement = x.JobProfileElement.Element,
                JobProfileId = x.JobProfileId,
                MaxRequirement = x.MaxRequirement,
                MinRequirement = x.MinRequirement,
                LastUpdated = x.LastUpdated,
                Organization = x.JobProfile.Organization.CompanyName,
                OrganizationId = x.JobProfile.OrganizationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<JobProfileDetailViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.JobProfileDetails.Where(x => x.JobProfile.OrganizationId == id).Select(x => new JobProfileDetailViewModel
            {
                Comment = x.Comment,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                IsMandatory = x.IsMandatory,
                Id = x.Id,
                JobElementId = x.JobElementId,
                JobProfile = x.JobProfile.Description,
                JobProfileElement = x.JobProfileElement.Element,
                JobProfileId = x.JobProfileId,
                MaxRequirement = x.MaxRequirement,
                MinRequirement = x.MinRequirement,
                LastUpdated = x.LastUpdated,
                Organization = x.JobProfile.Organization.CompanyName,
                OrganizationId = x.JobProfile.OrganizationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<JobProfileDetailViewModel>> GetAllByUserId(string userId)
        {
            return await dbContext.JobProfileDetails.Where(x => x.CreatedBy == userId).Select(x => new JobProfileDetailViewModel
            {
                Comment = x.Comment,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                IsMandatory = x.IsMandatory,
                Id = x.Id,
                JobElementId = x.JobElementId,
                JobProfile = x.JobProfile.Description,
                JobProfileElement = x.JobProfileElement.Element,
                JobProfileId = x.JobProfileId,
                MaxRequirement = x.MaxRequirement,
                MinRequirement = x.MinRequirement,
                LastUpdated = x.LastUpdated,
                Organization = x.JobProfile.Organization.CompanyName,
                OrganizationId = x.JobProfile.OrganizationId
            }).ToListAsync();
        }

        public async Task<JobProfileDetailViewModel> GetJobProfileById(long id)
        {
            return await dbContext.JobProfileDetails.Where(x => x.Id == id).Select(x => new JobProfileDetailViewModel
            {
                Comment = x.Comment,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                IsMandatory = x.IsMandatory,
                Id = x.Id,
                JobElementId = x.JobElementId,
                JobProfile = x.JobProfile.Description,
                JobProfileElement = x.JobProfileElement.Element,
                JobProfileId = x.JobProfileId,
                MaxRequirement = x.MaxRequirement,
                MinRequirement = x.MinRequirement,
                LastUpdated = x.LastUpdated,
                Organization = x.JobProfile.Organization.CompanyName,
                OrganizationId = x.JobProfile.OrganizationId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(JobProfileDetailViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool createAccess = false;
                //Check if job profile exist
                JobProfile profile = await dbContext.JobProfiles.Where(x => x.Id == model.JobProfileId).FirstOrDefaultAsync();
                if (profile != null)
                {
                    //Check if job profile element exist
                    JobProfileElement element = await dbContext.JobProfileElements.Where(x => x.Id == model.JobElementId).FirstOrDefaultAsync();
                    if (element != null)
                    {
                        //check if user exist
                        var user = await userManager.FindByIdAsync(model.UserId);
                        if (user != null)
                        {
                            //get user roles
                            var userRoles = await userManager.GetRolesAsync(user);
                            //get all user access
                            IList<UserRoleFunctionAccess> functionAccess = await dbContext.UserRoleFunctionAccess.Where(x => x.RoleId == model.OrganizationUserRoleId).ToListAsync();
                            if (functionAccess.Count() > 0)
                            {
                                foreach (UserRoleFunctionAccess access in functionAccess)
                                {
                                    if (access.FunctionId == (int)Function.Manage_Job_Profiles && access.AccessId == (int)Access.Create)
                                    {
                                        createAccess = true;
                                    }
                                }
                            }
                            //check if user has access right to create job profile
                            if (userRoles.Contains("organization") || (userRoles.Contains("organizationuser") && createAccess == true))
                            {
                                //create job profile
                                JobProfileDetail jobProfile = new JobProfileDetail
                                {
                                    Comment = model.Comment,
                                    CreatedBy = model.UserId,
                                    DateCreated = DateTime.Now,
                                    IsMandatory = model.IsMandatory,
                                    JobElementId = model.JobElementId,
                                    JobProfileId = model.JobProfileId,
                                    LastUpdated = DateTime.Now,
                                    MaxRequirement = model.MaxRequirement,
                                    MinRequirement = model.MinRequirement
                                };
                                dbContext.JobProfileDetails.Add(jobProfile);
                                await dbContext.SaveChangesAsync();
                                response.code = 200;
                                response.message = "Saved successfully";
                            }
                            else
                            {
                                response.code = 401;
                                response.message = "You dont have access to create job profile";
                            }
                        }
                        else
                        {
                            response.code = 405;
                            response.message = "User doesn't exist";
                        }
                    }
                    else
                    {
                        response.code = 406;
                        response.message = "Job Profile Element doesn't exist";
                    }
                }
                else
                {
                    response.code = 400;
                    response.message = "Job Profile doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.JobProfileDetails.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, JobProfileDetailViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool updateAccess = false;
                //check if record exist
                JobProfileDetail jobProfile = await dbContext.JobProfileDetails.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (jobProfile != null)
                {
                    //Check if job profile exist
                    JobProfile profile = await dbContext.JobProfiles.Where(x => x.Id == model.JobProfileId).FirstOrDefaultAsync();
                    if (profile != null)
                    {
                        //Check if job profile element exist
                        JobProfileElement element = await dbContext.JobProfileElements.Where(x => x.Id == model.JobElementId).FirstOrDefaultAsync();
                        if (element != null)
                        {
                            //check if user exist
                            var user = await userManager.FindByIdAsync(model.UserId);
                            if (user != null)
                            {
                                //get user roles
                                var userRoles = await userManager.GetRolesAsync(user);
                                //get all user access
                                IList<UserRoleFunctionAccess> functionAccess = await dbContext.UserRoleFunctionAccess.Where(x => x.RoleId == model.OrganizationUserRoleId).ToListAsync();
                                if (functionAccess.Count() > 0)
                                {
                                    foreach (UserRoleFunctionAccess access in functionAccess)
                                    {
                                        if (access.FunctionId == (int)Function.Manage_Job_Profiles && access.AccessId == (int)Access.Update)
                                        {
                                            updateAccess = true;
                                        }
                                    }
                                }
                                //check if user has access right to create job profile
                                if (userRoles.Contains("organization") || (userRoles.Contains("organizationuser") && updateAccess == true))
                                {
                                    
                                    jobProfile.Comment = model.Comment;
                                    jobProfile.IsMandatory = model.IsMandatory;
                                    jobProfile.JobElementId = model.JobElementId;
                                    jobProfile.JobProfileId = model.JobProfileId;
                                    jobProfile.MaxRequirement = model.MaxRequirement;
                                    jobProfile.MinRequirement = model.MinRequirement;
                                    jobProfile.LastUpdated = DateTime.Now;
                                    await dbContext.SaveChangesAsync();
                                    response.code = 200;
                                    response.message = "Record Updated successfully";
                                }
                                else
                                {
                                    response.code = 401;
                                    response.message = "You don't have access to update job profile";
                                }
                            }
                            else
                            {
                                response.code = 405;
                                response.message = "User doesn't exist";
                            }
                        }
                        else
                        {
                            response.code = 406;
                            response.message = "Job Profile Element doesn't exist";
                        }
                    }
                    else
                    {
                        response.code = 400;
                        response.message = "Job Profile doesn't exist";
                    }
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
                response.code = 402;
                dbContext.JobProfileDetails.Local.Clear();
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
