using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Repository
{
    public class LevelRepository : ILevelRepository
    {
        private readonly AppDbContext dbContext;

        public LevelRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ApplicantLevel> FindByNameAsync(string name)
        {
            return await dbContext.ApplicantLevels.Where(x => x.Level.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApplicantLevel>> GetAllLevel()
        {
            return await dbContext.ApplicantLevels.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(ApplicantLevel level)
        {
            ResponseModel response = new ResponseModel();
            var newLevel = new ApplicantLevel()
            {
                Level = level.Level
            };
            if (level.Level.Any())
            {
                dbContext.ApplicantLevels.Add(newLevel);
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
                    dbContext.ApplicantLevels.Local.Clear();
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
