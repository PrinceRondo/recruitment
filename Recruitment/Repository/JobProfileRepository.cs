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
    public class JobProfileRepository : IJobProfileRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public JobProfileRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
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
                        JobProfile profile = await dbContext.JobProfiles.FindAsync(id);
                        if (profile != null)
                        {
                            dbContext.JobProfiles.Remove(profile);
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
                dbContext.JobProfiles.Local.Clear();
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

        public async Task<IEnumerable<JobProfileViewModel>> GetAll()
        {
            return await dbContext.JobProfiles.Select(x => new JobProfileViewModel
            {
                Code = x.Code,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Id = x.Id,
                LastUpdated = x.LastUpdated,
                Organization = x.Organization.CompanyName,
                OrganizationId = x.OrganizationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<JobProfileViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.JobProfiles.Where(x => x.OrganizationId == id).Select(x => new JobProfileViewModel
            {
                Code = x.Code,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Id = x.Id,
                LastUpdated = x.LastUpdated,
                Organization = x.Organization.CompanyName,
                OrganizationId = x.OrganizationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<JobProfileViewModel>> GetAllByUserId(string userId)
        {
            return await dbContext.JobProfiles.Where(x => x.CreatedBy == userId).Select(x => new JobProfileViewModel
            {
                Code = x.Code,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Id = x.Id,
                LastUpdated = x.LastUpdated,
                Organization = x.Organization.CompanyName,
                OrganizationId = x.OrganizationId
            }).ToListAsync();
        }

        public async Task<JobProfileViewModel> GetJobProfileById(long id)
        {
            return await dbContext.JobProfiles.Where(x => x.Id == id).Select(x => new JobProfileViewModel
            {
                Code = x.Code,
                UserId = x.CreatedBy,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Id = x.Id,
                LastUpdated = x.LastUpdated,
                Organization = x.Organization.CompanyName,
                OrganizationId = x.OrganizationId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(JobProfileViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool createAccess = false;
                //Check if organization exist
                OrganizationProfile profile = await dbContext.OrganizationProfiles.Where(x => x.Id == model.OrganizationId).FirstOrDefaultAsync();
                if (profile != null)
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
                            JobProfile jobProfile = new JobProfile
                            {
                                Code = model.Code,
                                CreatedBy = model.UserId,
                                DateCreated = DateTime.Now,
                                Description = model.Description,
                                LastUpdated = DateTime.Now,
                                OrganizationId = model.OrganizationId
                            };
                            dbContext.JobProfiles.Add(jobProfile);
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
                    response.code = 400;
                    response.message = "Organization doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.UserRoleFunctionAccess.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, JobProfileViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool updateAccess = false;
                //check if record exist
                JobProfile jobProfile = await dbContext.JobProfiles.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (jobProfile != null)
                {
                    //Check if organization exist
                    OrganizationProfile profile = await dbContext.OrganizationProfiles.Where(x => x.Id == jobProfile.OrganizationId).FirstOrDefaultAsync();
                    if (profile != null)
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
                                
                                jobProfile.Code = model.Code;
                                jobProfile.Description = model.Description;
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
                        response.code = 400;
                        response.message = "Organization doesn't exist";
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
                dbContext.UserRoleFunctionAccess.Local.Clear();
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
