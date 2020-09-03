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
    public class OrganizationJobRoleRepository : IOrganizationJobRoleRepository
    {
        private readonly AppDbContext dbContext;

        public OrganizationJobRoleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationJobRoles jobRole = await dbContext.OrganisationJobRoles.FindAsync(id);
                if (jobRole != null)
                {
                    dbContext.OrganisationJobRoles.Remove(jobRole);
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
                dbContext.OrganisationJobRoles.Local.Clear();
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

        public async Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAll()
        {
            return await dbContext.OrganisationJobRoles.Select(x => new OrganizationJobeRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                DepartmentId = x.DepartmentId,
                Department = x.Department.DepartmentName,
                Id = x.Id,
                RoleName = x.JobRoleName
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAllByDepartmentId(long id)
        {
            return await dbContext.OrganisationJobRoles.Where(x => x.DepartmentId == id).Select(x => new OrganizationJobeRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                DepartmentId = x.DepartmentId,
                Department = x.Department.DepartmentName,
                Id = x.Id,
                RoleName = x.JobRoleName
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.OrganisationJobRoles.Where(x => x.Department.RecruitmentLocation.OrganizationProfileId == id).Select(x => new OrganizationJobeRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                DepartmentId = x.DepartmentId,
                Department = x.Department.DepartmentName,
                Id = x.Id,
                RoleName = x.JobRoleName
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAllByRecruitmentLocationId(long id)
        {
            return await dbContext.OrganisationJobRoles.Where(x => x.Department.RecruitmentLocationId == id).Select(x => new OrganizationJobeRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                DepartmentId = x.DepartmentId,
                Department = x.Department.DepartmentName,
                Id = x.Id,
                RoleName = x.JobRoleName
            }).ToListAsync();
        }

        public async Task<OrganizationJobeRoleViewModel> GetJobRoleById(long id)
        {
            return await dbContext.OrganisationJobRoles.Where(x => x.Id == id).Select(x => new OrganizationJobeRoleViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                DepartmentId = x.DepartmentId,
                Department = x.Department.DepartmentName,
                Id = x.Id,
                RoleName = x.JobRoleName
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(OrganizationJobeRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationDepartments department = await dbContext.OrganizationDepartments.Where(x => x.Id == model.DepartmentId).FirstOrDefaultAsync();
                if (department != null)
                {
                    OrganizationJobRoles jobRole = await dbContext.OrganisationJobRoles.Where(x =>
                        x.JobRoleName.ToLower() == model.RoleName.ToLower() && x.DepartmentId == model.DepartmentId).FirstOrDefaultAsync();
                    if (jobRole == null)
                    {
                        OrganizationJobRoles role = new OrganizationJobRoles()
                        {
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now,
                            DepartmentId = model.DepartmentId,
                            JobRoleName = model.RoleName
                        };
                        dbContext.OrganisationJobRoles.Add(role);
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Role saved successfully";
                    }
                    else
                    {
                        response.code = 401;
                        response.message = "This role has been added already for this department";
                    }
                }
                else
                {
                    response.code = 400;
                    response.message = "Department doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.OrganisationJobRoles.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, OrganizationJobeRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationJobRoles role = await dbContext.OrganisationJobRoles.FirstOrDefaultAsync(x => x.Id == id);
                if (role != null)
                {
                    OrganizationDepartments department = await dbContext.OrganizationDepartments.Where(x => x.Id == model.DepartmentId).FirstOrDefaultAsync();
                    if (department != null)
                    {
                        role.DateUpdated = DateTime.Now;
                        role.DepartmentId = department.Id;
                        role.JobRoleName = model.RoleName;
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Role updated successfully";
                    }
                    else
                    {
                        response.code = 400;
                        response.message = "Department doesn't exist";
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
                response.code = 400;
                dbContext.OrganisationJobRoles.Local.Clear();
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
