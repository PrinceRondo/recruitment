using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModel;
using Recruitment.ViewModels;

namespace Recruitment.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext dbContext;

        public UserProfileRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ApplicantProfile>> GetAllApplicantProfile()
        {
            return await dbContext.ApplicantProfiles.ToListAsync();
        }

        public async Task<IEnumerable<OrganizationProfile>> GetAllOrganizationProfile()
        {
            return await dbContext.OrganizationProfiles.ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUsersInfo>> GetAllOrganizationUserProfile(long OrganizationId)
        {
            return await dbContext.OrganizationUsersInfo.ToListAsync();
        }

        public async Task<ApplicantProfile> GetApplicantProfileById(long id)
        {
            return await dbContext.ApplicantProfiles.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<OrganizationProfile> GetOrganizationProfileById(long id)
        {
            return await dbContext.OrganizationProfiles.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<OrganizationUsersInfo> GetOrganizationUserProfileById(long id)
        {
            return await dbContext.OrganizationUsersInfo.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> UpdateApplicantProfileAsync(long id, UpdateProfileViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantProfile profile = await dbContext.ApplicantProfiles.FirstOrDefaultAsync(x => x.Id == id);
                if (profile != null)
                {
                    profile.Address = model.address;
                    profile.FirstName = model.firstName;
                    profile.IsActive = true;
                    profile.LastName = model.lastName;
                    profile.ModifiedAt = DateTime.Now;
                    profile.PhoneNumber = model.phoneNumber;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Profile updated successfully";
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
                response.code = 401;
                dbContext.ApplicantProfiles.Local.Clear();
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

        public async Task<ResponseModel> UpdateOrganizationProfileAsync(long id, UpdateProfileViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationProfile profile = await dbContext.OrganizationProfiles.FirstOrDefaultAsync(x => x.Id == id);
                if (profile != null)
                {
                    profile.Abbreviation = model.abbreviation;
                    profile.Address = model.address;
                    profile.CompanyName = model.companyName;
                    profile.ContactEmail = model.contactEmail;
                    profile.ContactFirstName = model.contactFirstName;
                    profile.ContactLastName = model.contactLastName;
                    profile.ContactPhoneNumber = model.contactPhoneNumber;
                    profile.DateUpdated = DateTime.Now;
                    profile.HeadQuarterAddress = model.headQuarterAddress;
                    profile.IndustryId = model.industryId;
                    profile.IsActive = true;
                    profile.PhoneNumber = model.phoneNumber;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Profile updated successfully";
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
                response.code = 401;
                dbContext.OrganizationProfiles.Local.Clear();
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

        public async Task<ResponseModel> UpdateOrganizationUserProfileAsync(long id, UpdateProfileViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationUsersInfo usersInfo = await dbContext.OrganizationUsersInfo.FirstOrDefaultAsync(x => x.Id == id);
                if (usersInfo != null)
                {
                    usersInfo.DateUpdated = DateTime.Now;
                    usersInfo.Firstname = model.firstName;
                    usersInfo.IsActive = true;
                    usersInfo.Lastname = model.lastName;
                    usersInfo.PhoneNumber = model.phoneNumber;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Profile updated successfully";
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
                response.code = 401;
                dbContext.OrganizationUsersInfo.Local.Clear();
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
