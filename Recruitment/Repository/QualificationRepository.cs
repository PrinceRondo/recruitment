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
    public class QualificationRepository : IQualificationRepository
    {
        private readonly AppDbContext dbContext;

        public QualificationRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Qualification> FindByNameAsync(string name)
        {
            return await dbContext.Qualifications.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Qualification>> GetAllQualification()
        {
            return await dbContext.Qualifications.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(Qualification model)
        {
            ResponseModel response = new ResponseModel();
            var newQualification = new Qualification()
            {
                Name = model.Name,
                ApplicantLevelId = model.ApplicantLevelId
            };
            if (model.Name.Any())
            {
                dbContext.Qualifications.Add(newQualification);
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
                    dbContext.Qualifications.Local.Clear();
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
