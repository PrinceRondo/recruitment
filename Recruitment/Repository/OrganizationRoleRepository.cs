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
    public class OrganizationRoleRepository : IOrganizationRoleRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationRoleRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationRoles role = await dbContext.OrganizationRoles.FindAsync(id);
                if (role != null)
                {
                    dbContext.OrganizationRoles.Remove(role);
                    await dbContext.SaveChangesAsync();
                    response.message = "Role deleted successfully";
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
                response.code = 400;
                dbContext.OrganizationRoles.Local.Clear();
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

        public async Task<IEnumerable<OrganizationRoleViewModel>> GetAll()
        {
            return await dbContext.OrganizationRoles.Select(x => new OrganizationRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                RoleName = x.RoleName,
                Id = x.Id,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationId,
                OrganizationUserId = x.OrganizationUserId
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationRoleViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.OrganizationRoles.Where(x => x.OrganizationId == id).Select(x => new OrganizationRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                RoleName = x.RoleName,
                Id = x.Id,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationId,
                OrganizationUserId = x.OrganizationUserId
            }).ToListAsync();
        }

        public async Task<OrganizationRoleViewModel> GetRoleById(long id)
        {
            return await dbContext.OrganizationRoles.Where(x => x.Id == id).Select(x => new OrganizationRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                RoleName = x.RoleName,
                Id = x.Id,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationId,
                OrganizationUserId = x.OrganizationUserId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(OrganizationRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var organizationUser = await userManager.FindByIdAsync(model.OrganizationUserId);
                if (organizationUser != null)
                {
                    OrganizationProfile organization = await dbContext.OrganizationProfiles.Where(x => x.Id == model.OrganizationId).FirstOrDefaultAsync();
                    if (organization != null)
                    {
                        OrganizationRoles organizationRole = await dbContext.OrganizationRoles.Where(x =>
                        x.RoleName.ToLower() == model.RoleName.ToLower() && x.OrganizationId == model.OrganizationId).FirstOrDefaultAsync();
                        if (organizationRole == null)
                        {
                            OrganizationRoles role = new OrganizationRoles()
                            {
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                RoleName = model.RoleName,
                                OrganizationId = model.OrganizationId,
                                OrganizationUserId = model.OrganizationUserId
                            };
                            dbContext.OrganizationRoles.Add(role);
                            await dbContext.SaveChangesAsync();
                            response.code = 200;
                            response.message = "Role saved successfully";
                        }
                        else
                        {
                            response.code = 402;
                            response.message = "This role has been added already for this Organization";
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
                    response.code = 401;
                    response.message = "Organization User doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.OrganizationRoles.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, OrganizationRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationRoles role = await dbContext.OrganizationRoles.FirstOrDefaultAsync(x => x.Id == id);
                if (role != null)
                {
                    role.DateUpdated = DateTime.Now;
                    role.RoleName = model.RoleName;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Role updated successfully";
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
                dbContext.OrganizationRoles.Local.Clear();
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
