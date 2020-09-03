using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.Repository;
using Recruitment.RespondModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Repository
{
    public class JobTypeRepository : IJobTypeRepository
    {
        private readonly AppDbContext dbContext;

        public JobTypeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<JobTypes> FindByNameAsync(string name)
        {
            return await dbContext.JobTypes.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<JobTypes>> GetAllJobType()
        {
            return await dbContext.JobTypes.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(JobTypes model)
        {
            ResponseModel response = new ResponseModel();
            var newType = new JobTypes()
            {
                Name = model.Name
            };
            if (model.Name.Any())
            {
                dbContext.JobTypes.Add(newType);
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
                    dbContext.JobTypes.Local.Clear();
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
