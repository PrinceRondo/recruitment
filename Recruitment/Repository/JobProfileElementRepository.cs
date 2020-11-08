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
    public class JobProfileElementRepository : IJobProfileElementRepository
    {
        private readonly AppDbContext dbContext;

        public JobProfileElementRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<JobProfileElement> FindByNameAsync(string name)
        {
            return await dbContext.JobProfileElements.Where(x => x.Element.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<JobProfileElement>> GetAllElement()
        {
            return await dbContext.JobProfileElements.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(JobProfileElement model)
        {
            ResponseModel response = new ResponseModel();
            var newElement = new JobProfileElement()
            {
                Element = model.Element
            };
            if (model.Element.Any())
            {
                dbContext.JobProfileElements.Add(newElement);
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
                    dbContext.JobProfileElements.Local.Clear();
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
