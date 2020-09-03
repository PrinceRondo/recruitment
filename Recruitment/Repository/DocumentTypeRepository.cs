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
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly AppDbContext dbContext;

        public DocumentTypeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<DocumentTypeViewModel>> GetAll()
        {
            return await dbContext.DocumentTypes.Select(x=> new DocumentTypeViewModel { CategoryId = x.CategoryId, CategoryName = x.DocumentCategory.Name, Id = x.Id, Type = x.Type }).ToListAsync();
        }

        public async Task<IEnumerable<DocumentTypeViewModel>> GetAllByCategory(int id)
        {
            return await dbContext.DocumentTypes.Where(x=>x.CategoryId==id).Select(x => new DocumentTypeViewModel { CategoryId = x.CategoryId, CategoryName = x.DocumentCategory.Name, Id = x.Id, Type = x.Type }).ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(DocumentTypeViewModel model)
        {
            ResponseModel response = new ResponseModel();
            DocumentType documentType = await dbContext.DocumentTypes.FirstOrDefaultAsync(x => x.Type.ToLower() == model.Type.ToLower());
            if (documentType != null)
            {
                response.message = "Document type has been saved already";
                response.code = 400;
            }
            else
            {
                var newType = new DocumentType()
                {
                    Type = model.Type,
                    CategoryId = model.CategoryId,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                };
                if (model.Type.Any())
                {
                    dbContext.DocumentTypes.Add(newType);
                    try
                    {
                        dbContext.SaveChanges();
                        response.message = "Saved Successfully";
                        response.code = 200;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine($"Save Partner Status Error: {ex}");
                        response.message = ex.Message;
                        response.code = 400;
                        dbContext.DocumentTypes.Local.Clear();
                        ErrorLog log = new ErrorLog();
                        log.ErrorDate = DateTime.Now;
                        log.ErrorMessage = ex.Message;
                        log.ErrorSource = ex.Source;
                        log.ErrorStackTrace = ex.StackTrace;
                        dbContext.ErrorLogs.Add(log);
                        dbContext.SaveChanges();
                    }
                }
            }
            return response;
        }
    }
}
