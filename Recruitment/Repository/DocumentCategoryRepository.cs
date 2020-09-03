using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;

namespace Recruitment.Repository
{
    public class DocumentCategoryRepository : IDocumentCategoryRepository
    {
        private readonly AppDbContext dbContext;

        public DocumentCategoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<DocumentCategory> FindByNameAsync(string name)
        {
            return await dbContext.DocumentCategories.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DocumentCategory>> GetAllCategory()
        {
            return await dbContext.DocumentCategories.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(DocumentCategory model)
        {
            ResponseModel response = new ResponseModel();
            var newCategory = new DocumentCategory()
            {
                Name = model.Name
            };
            if (model.Name.Any())
            {
                dbContext.DocumentCategories.Add(newCategory);
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
                    dbContext.DocumentCategories.Local.Clear();
                    ErrorLog log = new ErrorLog();
                    log.ErrorDate = DateTime.Now;
                    log.ErrorMessage = ex.Message;
                    log.ErrorSource = ex.Source;
                    log.ErrorStackTrace = ex.StackTrace;
                    dbContext.ErrorLogs.Add(log);
                    dbContext.SaveChanges();
                }
            }
            return response;
        }
    }
}
