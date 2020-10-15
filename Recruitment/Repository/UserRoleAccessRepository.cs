using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModels;

namespace Recruitment.Repository
{
    public class UserRoleAccessRepository : IUserRoleAccessRepository
    {
        private readonly AppDbContext dbContext;

        public UserRoleAccessRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                UserRoleFunctionAccess userRole = await dbContext.UserRoleFunctionAccess.FindAsync(id);
                if (userRole != null)
                {
                    dbContext.UserRoleFunctionAccess.Remove(userRole);
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
                response.code = 400;
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

        public async Task<IEnumerable<RoleFuctionAccessViewModel>> GetAll()
        {
            return await dbContext.UserRoleFunctionAccess.Select(x => new RoleFuctionAccessViewModel
            {
                AccessId = x.AccessId,
                AccessType = x.UserAccessType.type,
                CreatedBy = x.OrganizationRoles.OrganizationUserId,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                FunctionId = x.FunctionId,
                FunctionName = x.UserFunction.Function,
                Id = x.Id,
                Organization = x.OrganizationRoles.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationRoles.OrganizationId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRoles.RoleName
            }).ToListAsync();
        }

        public async Task<IEnumerable<RoleFuctionAccessViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.UserRoleFunctionAccess.Where(x => x.OrganizationRoles.OrganizationId == id).Select(x => new RoleFuctionAccessViewModel
            {
                AccessId = x.AccessId,
                AccessType = x.UserAccessType.type,
                CreatedBy = x.OrganizationRoles.OrganizationUserId,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                FunctionId = x.FunctionId,
                FunctionName = x.UserFunction.Function,
                Id = x.Id,
                Organization = x.OrganizationRoles.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationRoles.OrganizationId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRoles.RoleName
            }).ToListAsync();
        }

        public async Task<IEnumerable<RoleFuctionAccessViewModel>> GetAllByRoleId(long id)
        {
            return await dbContext.UserRoleFunctionAccess.Where(x => x.RoleId == id).Select(x => new RoleFuctionAccessViewModel
            {
                AccessId = x.AccessId,
                AccessType = x.UserAccessType.type,
                CreatedBy = x.OrganizationRoles.OrganizationUserId,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                FunctionId = x.FunctionId,
                FunctionName = x.UserFunction.Function,
                Id = x.Id,
                Organization = x.OrganizationRoles.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationRoles.OrganizationId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRoles.RoleName
            }).ToListAsync();
        }

        public async Task<RoleFuctionAccessViewModel> GetUserRoleFuntionAccessById(long id)
        {
            return await dbContext.UserRoleFunctionAccess.Where(x => x.Id == id).Select(x => new RoleFuctionAccessViewModel
            {
                AccessId = x.AccessId,
                AccessType = x.UserAccessType.type,
                CreatedBy = x.OrganizationRoles.OrganizationUserId,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                FunctionId = x.FunctionId,
                FunctionName = x.UserFunction.Function,
                Id = x.Id,
                Organization = x.OrganizationRoles.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationRoles.OrganizationId,
                RoleId = x.RoleId,
                RoleName = x.OrganizationRoles.RoleName
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(RoleFuctionAccessViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                UserRoleFunctionAccess userRoleFunctionAccess = await dbContext.UserRoleFunctionAccess.Where(x => x.RoleId == model.RoleId && x.FunctionId == model.FunctionId && x.AccessId == model.AccessId).FirstOrDefaultAsync();
                if (userRoleFunctionAccess == null)
                {
                    OrganizationRoles user = await dbContext.OrganizationRoles.Where(x => x.Id == model.RoleId).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        UserFunction userFunction = await dbContext.UserFunctions.Where(x => x.Id == model.FunctionId).FirstOrDefaultAsync();
                        if (userFunction != null)
                        {
                            UserAccessType userAccessType = await dbContext.UserAccessTypes.Where(x => x.Id == model.AccessId).FirstOrDefaultAsync();
                            if (userAccessType != null)
                            {
                                UserRoleFunctionAccess functionAccess = new UserRoleFunctionAccess
                                {
                                    AccessId = model.AccessId,
                                    DateCreated = DateTime.Now,
                                    DateUpdated = DateTime.Now,
                                    FunctionId = model.FunctionId,
                                    RoleId = model.RoleId,
                                };
                                dbContext.UserRoleFunctionAccess.Add(functionAccess);
                                await dbContext.SaveChangesAsync();
                                response.code = 200;
                                response.message = "Saved successfully";
                            }
                            else
                            {
                                response.code = 402;
                                response.message = "AccessType doesn't exist";
                            }
                        }
                        else
                        {
                            response.code = 401;
                            response.message = "Function doesn't exist";
                        }
                    }
                    else
                    {
                        response.code = 400;
                        response.message = "User role doesn't exist";
                    }
                }
                else
                {
                    response.code = 405;
                    response.message = "Access has already been saved";
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

        public async Task<ResponseModel> UpdateAsync(long id, RoleFuctionAccessViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationRoles user = await dbContext.OrganizationRoles.Where(x => x.Id == model.RoleId).FirstOrDefaultAsync();
                if (user != null)
                {
                    UserFunction userFunction = await dbContext.UserFunctions.Where(x => x.Id == model.FunctionId).FirstOrDefaultAsync();
                    if (userFunction != null)
                    {
                        UserAccessType userAccessType = await dbContext.UserAccessTypes.Where(x => x.Id == model.AccessId).FirstOrDefaultAsync();
                        if (userAccessType != null)
                        {
                            UserRoleFunctionAccess userRole = await dbContext.UserRoleFunctionAccess.FirstOrDefaultAsync(x => x.Id == id);
                            if (userRole != null)
                            {
                                userRole.AccessId = model.AccessId;
                                userRole.DateUpdated = DateTime.Now;
                                userRole.FunctionId = model.FunctionId;
                                userRole.RoleId = model.RoleId;

                                await dbContext.SaveChangesAsync();
                                response.code = 200;
                                response.message = "Record updated successfully";
                            }
                            else
                            {
                                response.code = 404;
                                response.message = "Record not found";
                            }
                        }
                        else
                        {
                            response.code = 402;
                            response.message = "AccessType doesn't exist";
                        }
                    }
                    else
                    {
                        response.code = 401;
                        response.message = "Function doesn't exist";
                    }
                }
                else
                {
                    response.code = 400;
                    response.message = "User role doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 405;
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
