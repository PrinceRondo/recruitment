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
    public class MaritalStatusRepository : IMaritalStatusRepository
    {
        private readonly AppDbContext dbContext;

        public MaritalStatusRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<MaritalStatus> FindByNameAsync(string name)
        {
            return await dbContext.MaritalStatus.Where(x => x.Status.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MaritalStatus>> GetAllMaritalStatus()
        {
            return await dbContext.MaritalStatus.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(MaritalStatus status)
        {
            ResponseModel response = new ResponseModel();
            var newStatus = new MaritalStatus()
            {
                Status = status.Status
            };
            if (status.Status.Any())
            {
                dbContext.MaritalStatus.Add(newStatus);
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
                    dbContext.MaritalStatus.Local.Clear();
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
