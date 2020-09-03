using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Repository
{
    public class OrganizationUserRoleRepository : IOrganizationUserRoleRepository
    {
        private readonly AppDbContext dbContext;

        public OrganizationUserRoleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationUserRole userRole = await dbContext.OrganizationUserRoles.FindAsync(id);
                if (userRole != null)
                {
                    dbContext.OrganizationUserRoles.Remove(userRole);
                    await dbContext.SaveChangesAsync();
                    response.message = "User Role deleted successfully";
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
                dbContext.OrganizationUserRoles.Local.Clear();
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

        public async Task<IEnumerable<OrganizationUserRoleViewModel>> GetAll()
        {
            return await dbContext.OrganizationUserRoles.Select(x => new OrganizationUserRoleViewModel
            {
                CompanyId = x.OrganizationUser.Organisation.Id,
                CompanyName = x.OrganizationUser.Organisation.CompanyName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                ProfileId = x.ProfileId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRole.RoleName,
                Id = x.Id
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUserRoleViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.OrganizationUserRoles.Where(x => x.OrganizationUser.Organisation.Id == id).Select(x => new OrganizationUserRoleViewModel
            {
                CompanyId = x.OrganizationUser.Organisation.Id,
                CompanyName = x.OrganizationUser.Organisation.CompanyName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                ProfileId = x.ProfileId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRole.RoleName,
                Id = x.Id
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUserRoleViewModel>> GetAllByUserId(string UserId)
        {
            return await dbContext.OrganizationUserRoles.Where(x => x.OrganizationUser.UserId == UserId).Select(x => new OrganizationUserRoleViewModel
            {
                CompanyId = x.OrganizationUser.Organisation.Id,
                CompanyName = x.OrganizationUser.Organisation.CompanyName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                ProfileId = x.ProfileId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRole.RoleName,
                Id = x.Id
            }).ToListAsync();
        }

        public async Task<OrganizationUserRoleViewModel> GetUserRoleById(long id)
        {
            return await dbContext.OrganizationUserRoles.Where(x => x.Id == id).Select(x => new OrganizationUserRoleViewModel
            {
                CompanyId = x.OrganizationUser.Organisation.Id,
                CompanyName = x.OrganizationUser.Organisation.CompanyName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                ProfileId = x.ProfileId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRole.RoleName,
                Id = x.Id
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> RemoveUserRoleAsync(RemoveUserRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                foreach (long roleId in model.RoleIdList)
                {
                    OrganizationUserRole userRole = await dbContext.OrganizationUserRoles.Where(x => x.ProfileId == model.ProfileId && x.RoleId == roleId).FirstOrDefaultAsync();
                    if (userRole != null)
                    {
                        dbContext.OrganizationUserRoles.Remove(userRole);
                        await dbContext.SaveChangesAsync();
                    }
                }
                response.message = "User Role removed successfully";
                response.code = 200;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 400;
                dbContext.OrganizationUserRoles.Local.Clear();
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

        public async Task<ResponseModel> SaveAsync(OrganizationUserRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationUsersInfo user = await dbContext.OrganizationUsersInfo.Where(x => x.Id == model.ProfileId).FirstOrDefaultAsync();
                if (user != null)
                {
                    foreach (long newRoleId in model.RoleIdList)
                    {
                        var role = await dbContext.OrganizationRoles.Where(x => x.Id == newRoleId).FirstOrDefaultAsync();
                        if (role != null)
                        {
                            OrganizationUserRole organizationUserRole = await dbContext.OrganizationUserRoles.Where(x => x.ProfileId == model.ProfileId && x.RoleId == newRoleId).FirstOrDefaultAsync();
                            if (organizationUserRole == null)
                            {
                                OrganizationUserRole userRole = new OrganizationUserRole()
                                {
                                    DateCreated = DateTime.Now,
                                    DateUpdated = DateTime.Now,
                                    ProfileId = model.ProfileId,
                                    RoleId = newRoleId
                                };
                                dbContext.OrganizationUserRoles.Add(userRole);
                                await dbContext.SaveChangesAsync();
                            }
                        }
                    }
                    response.code = 200;
                    response.message = "User Role saved successfully";
                }
                else
                {
                    response.code = 400;
                    response.message = "User doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.OrganizationUserRoles.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, OrganizationUserRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationUserRole userRole = await dbContext.OrganizationUserRoles.FirstOrDefaultAsync(x => x.Id == id);
                if (userRole != null)
                {
                    userRole.DateUpdated = DateTime.Now;
                    userRole.ProfileId = model.ProfileId;
                    userRole.RoleId = model.RoleId;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "User Role updated successfully";
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
                dbContext.OrganizationUserRoles.Local.Clear();
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
