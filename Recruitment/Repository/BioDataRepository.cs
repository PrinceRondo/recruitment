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
using static Recruitment.Helper.Utility;

namespace Recruitment.Repository
{
    public class BioDataRepository : IBioDataRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public BioDataRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> DeleteAsync(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantBiodata biodata = await dbContext.ApplicantBiodatas.FindAsync(id);
                if(biodata != null)
                {
                    dbContext.ApplicantBiodatas.Remove(biodata);
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
                dbContext.ApplicantBiodatas.Local.Clear();
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

        public async Task<IEnumerable<ApplicantBiodataViewModel>> GetAllBiodata()
        {
            return await dbContext.ApplicantBiodatas.Select(x => new ApplicantBiodataViewModel
            {
                Address = x.Address,
                DateCreated = x.DateCreated,
                DateOfBirth = x.DateOfBirth,
                EmailAddress = x.EmailAddress,
                FirstName = x.FirstName,
                Gender = x.Gender.Name,
                LastName = x.LastName,
                LastUpdated = x.LastUpdated,
                MaritalStatus = x.MaritalStatus.Status,
                OtherName = x.OtherName,
                PhoneNumber = x.PhoneNumber,
                id = x.Id,
                userId = x.UserId,
                GenderId = x.GenderId,
                MaritalStatusId = x.MaritalStatusId
            }).ToListAsync();
        }

        public async Task<ApplicantBiodataViewModel> GetBiodataById(int id)
        {
            return await dbContext.ApplicantBiodatas.Where(x => x.Id == id).Select(x => new ApplicantBiodataViewModel
            {
                Address = x.Address,
                DateCreated = x.DateCreated,
                DateOfBirth = x.DateOfBirth,
                EmailAddress = x.EmailAddress,
                FirstName = x.FirstName,
                Gender = x.Gender.Name,
                LastName = x.LastName,
                LastUpdated = x.LastUpdated,
                MaritalStatus = x.MaritalStatus.Status,
                OtherName = x.OtherName,
                PhoneNumber = x.PhoneNumber,
                id = x.Id,
                userId = x.UserId,
                GenderId = x.GenderId,
                MaritalStatusId = x.MaritalStatusId
            }).FirstOrDefaultAsync();
        }

        public async Task<ApplicantBiodataViewModel> GetBiodataByUserId(string userId)
        {
            return await dbContext.ApplicantBiodatas.Where(x => x.UserId == userId).Select(x => new ApplicantBiodataViewModel
            {
                Address = x.Address,
                DateCreated = x.DateCreated,
                DateOfBirth = x.DateOfBirth,
                EmailAddress = x.EmailAddress,
                FirstName = x.FirstName,
                Gender = x.Gender.Name,
                LastName = x.LastName,
                LastUpdated = x.LastUpdated,
                MaritalStatus = x.MaritalStatus.Status,
                OtherName = x.OtherName,
                PhoneNumber = x.PhoneNumber,
                id = x.Id,
                userId = x.UserId,
                GenderId = x.GenderId,
                MaritalStatusId = x.MaritalStatusId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(ApplicantBiodataViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var user = await userManager.FindByIdAsync(model.userId);
                if (user != null)
                {
                    ApplicantBiodata biodata = await dbContext.ApplicantBiodatas.FirstOrDefaultAsync(x => x.UserId == model.userId);
                    if (biodata == null)
                    {
                        ApplicantBiodata applicant = new ApplicantBiodata()
                        {
                            Address = model.Address,
                            DateCreated = DateTime.Now,
                            DateOfBirth = model.DateOfBirth,
                            EmailAddress = model.EmailAddress,
                            FirstName = model.FirstName,
                            GenderId = model.GenderId,
                            LastName = model.LastName,
                            LastUpdated = DateTime.Now,
                            MaritalStatusId = model.MaritalStatusId,
                            OtherName = model.OtherName,
                            PhoneNumber = model.PhoneNumber,
                            UserId = model.userId,
                            ApplicantLevelId = (int)level.Level_0,
                            YearsOfExperience= model.YearsOfExperience
                        };
                        dbContext.ApplicantBiodatas.Add(applicant);
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Bio Data saved successfully";
                    }
                    else
                    {
                        response.code = 404;
                        response.message = "Bio Data has been saved already";
                    }
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
                dbContext.MaritalStatus.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(int id, ApplicantBiodataViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantBiodata biodata = await dbContext.ApplicantBiodatas.FirstOrDefaultAsync(x => x.Id == id);
                if (biodata != null)
                {
                    biodata.Address = model.Address;
                    biodata.DateOfBirth = model.DateOfBirth;
                    biodata.EmailAddress = model.EmailAddress;
                    biodata.FirstName = model.FirstName;
                    biodata.GenderId = model.GenderId;
                    biodata.LastName = model.LastName;
                    biodata.LastUpdated = DateTime.Now;
                    biodata.MaritalStatusId = model.MaritalStatusId;
                    biodata.OtherName = model.OtherName;
                    biodata.PhoneNumber = model.PhoneNumber;
                    biodata.YearsOfExperience = model.YearsOfExperience;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Bio Data updated successfully";
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
                response.code = 404;
                dbContext.ApplicantBiodatas.Local.Clear();
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
