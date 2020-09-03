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
    public class OrganizationDepartmentRepository : IOrganizationDepartmentRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationDepartmentRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationDepartments department = await dbContext.OrganizationDepartments.FindAsync(id);
                if (department != null)
                {
                    dbContext.OrganizationDepartments.Remove(department);
                    await dbContext.SaveChangesAsync();
                    response.message = "Department deleted successfully";
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
                dbContext.OrganizationDepartments.Local.Clear();
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

        public async Task<IEnumerable<OrganizationDepartmentViewModel>> GetAll()
        {
            return await dbContext.OrganizationDepartments.Select(x => new OrganizationDepartmentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Code = x.Code,
                DepartmentName = x.DepartmentName,
                OrganizationName = x.RecruitmentLocation.OrganizationProfile.CompanyName,
                Id = x.Id,
                IsHeadOffice = x.IsHeadOffice,
                OrganizationId = x.RecruitmentLocation.OrganizationProfileId,
                OrganizationUserId = x.RecruitmentLocation.OrganizationUserId,
                RecruitmentLocation = x.RecruitmentLocation.Location,
                RecruitmentLocationId = x.RecruitmentLocationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationDepartmentViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.OrganizationDepartments.Where(x => x.RecruitmentLocation.OrganizationProfileId == id).Select(x => new OrganizationDepartmentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Code = x.Code,
                DepartmentName = x.DepartmentName,
                OrganizationName = x.RecruitmentLocation.OrganizationProfile.CompanyName,
                Id = x.Id,
                IsHeadOffice = x.IsHeadOffice,
                OrganizationId = x.RecruitmentLocation.OrganizationProfileId,
                OrganizationUserId = x.RecruitmentLocation.OrganizationUserId,
                RecruitmentLocation = x.RecruitmentLocation.Location,
                RecruitmentLocationId = x.RecruitmentLocationId
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationDepartmentViewModel>> GetAllByRecruitmentLocation(long id)
        {
            RecruitmentLocation location = await dbContext.RecruitmentLocations.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (location.IsHeadOfficeStructure == true)
            {
                return await dbContext.OrganizationDepartments.Where(x => x.RecruitmentLocation.OrganizationProfileId == location.OrganizationProfileId && x.IsHeadOffice == true).Select(x => new OrganizationDepartmentViewModel
                {
                    DateCreated = x.DateCreated,
                    DateUpdated = x.DateUpdated,
                    Code = x.Code,
                    DepartmentName = x.DepartmentName,
                    OrganizationName = x.RecruitmentLocation.OrganizationProfile.CompanyName,
                    Id = x.Id,
                    IsHeadOffice = x.IsHeadOffice,
                    OrganizationId = x.RecruitmentLocation.OrganizationProfileId,
                    OrganizationUserId = x.RecruitmentLocation.OrganizationUserId,
                    RecruitmentLocation = x.RecruitmentLocation.Location,
                    RecruitmentLocationId = x.RecruitmentLocationId
                }).ToListAsync();
            }
            else
            {
                return await dbContext.OrganizationDepartments.Where(x => x.RecruitmentLocationId == id).Select(x => new OrganizationDepartmentViewModel
                {
                    DateCreated = x.DateCreated,
                    DateUpdated = x.DateUpdated,
                    Code = x.Code,
                    DepartmentName = x.DepartmentName,
                    OrganizationName = x.RecruitmentLocation.OrganizationProfile.CompanyName,
                    Id = x.Id,
                    IsHeadOffice = x.IsHeadOffice,
                    OrganizationId = x.RecruitmentLocation.OrganizationProfileId,
                    OrganizationUserId = x.RecruitmentLocation.OrganizationUserId,
                    RecruitmentLocation = x.RecruitmentLocation.Location,
                    RecruitmentLocationId = x.RecruitmentLocationId
                }).ToListAsync();
            }
        }

        public async Task<OrganizationDepartmentViewModel> GetDepartmentById(long id)
        {
            return await dbContext.OrganizationDepartments.Where(x => x.Id == id).Select(x => new OrganizationDepartmentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Code = x.Code,
                DepartmentName = x.DepartmentName,
                OrganizationName = x.RecruitmentLocation.OrganizationProfile.CompanyName,
                Id = x.Id,
                IsHeadOffice = x.IsHeadOffice,
                OrganizationId = x.RecruitmentLocation.OrganizationProfileId,
                OrganizationUserId = x.RecruitmentLocation.OrganizationUserId,
                RecruitmentLocation = x.RecruitmentLocation.Location,
                RecruitmentLocationId = x.RecruitmentLocationId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(OrganizationDepartmentViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                RecruitmentLocation location = await dbContext.RecruitmentLocations.Where(x => x.Id == model.RecruitmentLocationId).FirstOrDefaultAsync();
                if (location != null)
                {
                    OrganizationDepartments organizationDepartment = await dbContext.OrganizationDepartments.Where(x =>
                        x.DepartmentName.ToLower() == model.DepartmentName.ToLower() && x.RecruitmentLocationId == model.RecruitmentLocationId).FirstOrDefaultAsync();
                    if (organizationDepartment == null)
                    {
                        OrganizationDepartments department = new OrganizationDepartments()
                        {
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now,
                            Code = model.Code,
                            DepartmentName = model.DepartmentName,
                            IsHeadOffice = model.IsHeadOffice,
                            //OrganizationId = model.OrganizationId,
                            //OrganizationUserId = model.OrganizationUserId,
                            RecruitmentLocationId = model.RecruitmentLocationId
                        };
                        dbContext.OrganizationDepartments.Add(department);
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Department saved successfully";
                    }
                    else
                    {
                        response.code = 401;
                        response.message = "This department has been added already for this location";
                    }
                }
                else
                {
                    response.code = 400;
                    response.message = "Recruitment location doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.OrganizationDepartments.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, OrganizationDepartmentViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationDepartments department = await dbContext.OrganizationDepartments.FirstOrDefaultAsync(x => x.Id == id);
                if (department != null)
                {
                    RecruitmentLocation location = await dbContext.RecruitmentLocations.Where(x => x.Id == model.RecruitmentLocationId).FirstOrDefaultAsync();
                    if (location != null)
                    {
                        department.DateUpdated = DateTime.Now;
                        department.Code = model.Code;
                        department.DepartmentName = model.DepartmentName;
                        department.IsHeadOffice = model.IsHeadOffice;
                        department.RecruitmentLocationId = model.RecruitmentLocationId;
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Department updated successfully";
                    }
                    else
                    {
                        response.code = 400;
                        response.message = "Recruitment location doesn't exist";
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
                dbContext.OrganizationDepartments.Local.Clear();
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
