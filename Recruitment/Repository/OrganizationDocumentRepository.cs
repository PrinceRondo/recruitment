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
    public class OrganizationDocumentRepository : IOrganizationDocumentRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationDocumentRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationDocument document = await dbContext.OrganizationDocuments.FindAsync(id);
                if (document != null)
                {
                    dbContext.OrganizationDocuments.Remove(document);
                    await dbContext.SaveChangesAsync();
                    response.message = "Document deleted successfully";
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
                dbContext.OrganizationDocuments.Local.Clear();
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

        public async Task<IEnumerable<OrganizationDocumentViewModel>> GetAll()
        {
            return await dbContext.OrganizationDocuments.Select(x => new OrganizationDocumentViewModel
            {
                DateCreated = x.DateCreated,
                LastUpdated = x.LastUpdated,
                Description = x.Description,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationId,
                OrganizationUserId = x.OrganizationUserId
            }).ToListAsync();
        }

        public async Task<IEnumerable<OrganizationDocumentViewModel>> GetAllByOrgnizationId(long id)
        {
            return await dbContext.OrganizationDocuments.Where(x=>x.OrganizationId == id).Select(x => new OrganizationDocumentViewModel
            {
                DateCreated = x.DateCreated,
                LastUpdated = x.LastUpdated,
                Description = x.Description,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationId,
                OrganizationUserId = x.OrganizationUserId
            }).ToListAsync();
        }

        public async Task<OrganizationDocumentViewModel> GetDocumentById(long id)
        {
            return await dbContext.OrganizationDocuments.Where(x => x.Id == id).Select(x => new OrganizationDocumentViewModel
            {
                DateCreated = x.DateCreated,
                LastUpdated = x.LastUpdated,
                Description = x.Description,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                Organization = x.OrganizationProfile.CompanyName,
                OrganizationId = x.OrganizationId,
                OrganizationUserId = x.OrganizationUserId
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(OrganizationDocumentViewModel model)
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
                        OrganizationDocument organizationDocument = await dbContext.OrganizationDocuments.Where(x =>
                        x.FileName.ToLower() == model.FileName.ToLower() && x.OrganizationId == model.OrganizationId).FirstOrDefaultAsync();
                        if (organizationDocument == null)
                        {
                            OrganizationDocument document = new OrganizationDocument()
                            {
                                DateCreated = DateTime.Now,
                                LastUpdated = DateTime.Now,
                                Description = model.Description,
                                FileName = model.FileName,
                                FileType = model.FileType,
                                OrganizationId = model.OrganizationId,
                                OrganizationUserId = model.OrganizationUserId
                            };
                            dbContext.OrganizationDocuments.Add(document);
                            await dbContext.SaveChangesAsync();
                            response.code = 200;
                            response.message = "Document saved successfully";
                        }
                        else
                        {
                            response.code = 401;
                            response.message = "This document has been saved already for this organization";
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
                dbContext.OrganizationDocuments.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, OrganizationDocumentViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationDocument document = await dbContext.OrganizationDocuments.FirstOrDefaultAsync(x => x.Id == id);
                if (document != null)
                {
                    document.LastUpdated = DateTime.Now;
                    document.Description = model.Description;
                    document.FileName = model.FileName;
                    document.FileType = model.FileType;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Document updated successfully";
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
                dbContext.OrganizationDocuments.Local.Clear();
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
