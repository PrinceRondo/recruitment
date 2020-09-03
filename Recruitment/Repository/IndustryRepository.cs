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
    public class IndustryRepository : IIndustryRepository
    {
        private readonly AppDbContext dbContext;

        public IndustryRepository( AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Industry> FindByNameAsync(string name)
        {
            return await dbContext.Industries.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Industry>> GetAllIndustry()
        {
            return await dbContext.Industries.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(Industry model)
        {
            ResponseModel response = new ResponseModel();
            var newIndustry = new Industry()
            {
                Name = model.Name
            };
            if (model.Name.Any())
            {
                dbContext.Industries.Add(newIndustry);
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
                    response.code = 404;
                    dbContext.Industries.Local.Clear();
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
