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
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly AppDbContext dbContext;

        public InstitutionRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Institution> FindByNameAsync(string name)
        {
            return await dbContext.Institutions.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Institution>> GetAllInstitution()
        {
            return await dbContext.Institutions.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(Institution model)
        {
            ResponseModel response = new ResponseModel();
            var newInstitution = new Institution()
            {
                Name = model.Name
            };
            if (model.Name.Any())
            {
                dbContext.Institutions.Add(newInstitution);
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
                    dbContext.Institutions.Local.Clear();
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
