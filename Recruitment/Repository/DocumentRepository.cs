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
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public DocumentRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> DeleteAsync(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantDocument document = await dbContext.ApplicantDocuments.FindAsync(id);
                if (document != null)
                {
                    dbContext.ApplicantDocuments.Remove(document);
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
                dbContext.ApplicantDocuments.Local.Clear();
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

        public async Task<IEnumerable<ApplicantDocumentViewModel>> GetAllApplicantDocument()
        {
            return await dbContext.ApplicantDocuments.Select(x => new ApplicantDocumentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Description = x.Description,
                DocumentCategory = x.DocumentType.DocumentCategory.Name,
                DocumentCategoryId = x.DocumentType.CategoryId,
                DocumentType = x.DocumentType.Type,
                DocumentTypeId = x.DocumentTypeId,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                UserId = x.UserId,
                Year = x.Year
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantDocumentViewModel>> GetAllByCategoryId(int categoryId)
        {
            return await dbContext.ApplicantDocuments.Where(x => x.DocumentType.CategoryId == categoryId).Select(x => new ApplicantDocumentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Description = x.Description,
                DocumentCategory = x.DocumentType.DocumentCategory.Name,
                DocumentCategoryId = x.DocumentType.CategoryId,
                DocumentType = x.DocumentType.Type,
                DocumentTypeId = x.DocumentTypeId,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                UserId = x.UserId,
                Year = x.Year
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantDocumentViewModel>> GetAllByTypeId(int typeId)
        {
            return await dbContext.ApplicantDocuments.Where(x => x.DocumentTypeId == typeId).Select(x => new ApplicantDocumentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Description = x.Description,
                DocumentCategory = x.DocumentType.DocumentCategory.Name,
                DocumentCategoryId = x.DocumentType.CategoryId,
                DocumentType = x.DocumentType.Type,
                DocumentTypeId = x.DocumentTypeId,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                UserId = x.UserId,
                Year = x.Year
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantDocumentViewModel>> GetAllByUserId(string userId)
        {
            return await dbContext.ApplicantDocuments.Where(x => x.UserId == userId).Select(x => new ApplicantDocumentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Description = x.Description,
                DocumentCategory = x.DocumentType.DocumentCategory.Name,
                DocumentCategoryId = x.DocumentType.CategoryId,
                DocumentType = x.DocumentType.Type,
                DocumentTypeId = x.DocumentTypeId,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                UserId = x.UserId,
                Year = x.Year
            }).ToListAsync();
        }

        public async Task<ApplicantDocumentViewModel> GetDocumentById(int id)
        {
            return await dbContext.ApplicantDocuments.Where(x => x.Id == id).Select(x => new ApplicantDocumentViewModel
            {
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Description = x.Description,
                DocumentCategory = x.DocumentType.DocumentCategory.Name,
                DocumentCategoryId = x.DocumentType.CategoryId,
                DocumentType = x.DocumentType.Type,
                DocumentTypeId = x.DocumentTypeId,
                FileName = x.FileName,
                FileType = x.FileType,
                Id = x.Id,
                UserId = x.UserId,
                Year = x.Year
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(ApplicantDocumentViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    ApplicantDocument applicantDocument = await dbContext.ApplicantDocuments.Where(x => 
                    x.FileName == model.FileName && x.UserId == model.UserId).FirstOrDefaultAsync();
                    if (applicantDocument == null)
                    {
                        ApplicantDocument document = new ApplicantDocument()
                        {
                            DocumentTypeId = model.DocumentTypeId,
                            Description = model.Description,
                            FileName = model.FileName,
                            FileType = model.FileType,
                            UserId = model.UserId,
                            Year = model.Year,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now,
                        };
                        dbContext.ApplicantDocuments.Add(document);
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Document saved successfully";
                    }
                    else
                    {
                        response.code = 401;
                        response.message = "This document has been added already for this User";
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
                dbContext.ApplicantDocuments.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(int id, ApplicantDocumentViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantDocument document = await dbContext.ApplicantDocuments.FirstOrDefaultAsync(x => x.Id == id);
                if (document != null)
                {
                    document.DateUpdated = DateTime.Now;
                    document.DocumentTypeId = model.DocumentTypeId;
                    document.Description = model.Description;
                    document.FileName = model.FileName;
                    document.FileType = model.FileType;
                    document.Year = model.Year;
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
                dbContext.ApplicantDocuments.Local.Clear();
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
