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
    public class RecruitmentLocationRepository : IRecruitmentLocationRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public RecruitmentLocationRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<ResponseModel> DeleteAsync(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                RecruitmentLocation location = await dbContext.RecruitmentLocations.FindAsync(id);
                if (location != null)
                {
                    dbContext.RecruitmentLocations.Remove(location);
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
                dbContext.RecruitmentLocations.Local.Clear();
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

        public async Task<IEnumerable<RecruitmentLocationViewModel>> GetAll()
        {
            return await dbContext.RecruitmentLocations.Select(x => new RecruitmentLocationViewModel
            {
                Id = x.Id,
                IsHeadOfficeStructure = x.IsHeadOfficeStructure,
                Location = x.Location,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationProfileId,
                OrganizationUserId = x.OrganizationUserId,
                RecruitmentLocationType = x.RecruitmentLocationType.LocationType,
                RecruitmentLocationTypeId = x.TypeId
            }).ToListAsync();
        }

        public async Task<IEnumerable<RecruitmentLocationViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.RecruitmentLocations.Where(x=>x.OrganizationProfileId == id).Select(x => new RecruitmentLocationViewModel
            {
                Id = x.Id,
                IsHeadOfficeStructure = x.IsHeadOfficeStructure,
                Location = x.Location,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationProfileId,
                OrganizationUserId = x.OrganizationUserId,
                RecruitmentLocationType = x.RecruitmentLocationType.LocationType,
                RecruitmentLocationTypeId = x.TypeId
            }).ToListAsync();
        }

        public async Task<RecruitmentLocationViewModel> GetRecruitmentLocationById(long id)
        {
            return await dbContext.RecruitmentLocations.Where(x => x.Id == id).Select(x => new RecruitmentLocationViewModel
            {
                Id = x.Id,
                IsHeadOfficeStructure = x.IsHeadOfficeStructure,
                Location = x.Location,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationProfileId,
                OrganizationUserId = x.OrganizationUserId,
                RecruitmentLocationType = x.RecruitmentLocationType.LocationType,
                RecruitmentLocationTypeId = x.TypeId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(RecruitmentLocationViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                //check if organization user exist
                var organizationUser = await userManager.FindByIdAsync(model.OrganizationUserId);
                if (organizationUser != null)
                {
                    //check if organization exist
                    OrganizationProfile organization = await dbContext.OrganizationProfiles.Where(x => x.Id == model.OrganizationId).FirstOrDefaultAsync();
                    if (organization != null)
                    {
                        //if(model.IsHeadOfficeStructure == true)
                        RecruitmentLocation recruitmentLocation = await dbContext.RecruitmentLocations.Where(x =>
                        x.Location.ToLower() == model.Location.ToLower() && x.OrganizationProfileId == model.OrganizationId).FirstOrDefaultAsync();
                        if (recruitmentLocation == null)
                        {
                            RecruitmentLocation location = new RecruitmentLocation()
                            {
                                Location = model.Location,
                                OrganizationProfileId = model.OrganizationId,
                                OrganizationUserId = model.OrganizationUserId,
                                IsHeadOfficeStructure = model.IsHeadOfficeStructure,
                                TypeId = model.RecruitmentLocationTypeId
                            };
                            dbContext.RecruitmentLocations.Add(location);
                            await dbContext.SaveChangesAsync();
                            response.code = 200;
                            response.message = "Recruitment Location saved successfully";
                        }
                        else
                        {
                            response.code = 402;
                            response.message = "This location has been saved already for this Organization";
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
                dbContext.RecruitmentLocations.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(int id, RecruitmentLocationViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                RecruitmentLocation location = await dbContext.RecruitmentLocations.FirstOrDefaultAsync(x => x.Id == id);
                if (location != null)
                {
                    location.Location = model.Location;
                    location.IsHeadOfficeStructure = model.IsHeadOfficeStructure;
                    location.TypeId = model.RecruitmentLocationTypeId;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Recruitment location updated successfully";
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
                dbContext.RecruitmentLocations.Local.Clear();
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
