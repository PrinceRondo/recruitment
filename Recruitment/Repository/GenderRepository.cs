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
    public class GenderRepository : IGenderRepository
    {
        private readonly AppDbContext dbContext;

        public GenderRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Gender> FindByNameAsync(string name)
        {
            return await dbContext.Gender.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Gender>> GetAllGender()
        {
            return await dbContext.Gender.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(Gender gender)
        {
            ResponseModel response = new ResponseModel();
            var newGender = new Gender()
            {
                Name = gender.Name
            };
            if (gender.Name.Any())
            {
                dbContext.Gender.Add(newGender);
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
                    dbContext.Gender.Local.Clear();
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
