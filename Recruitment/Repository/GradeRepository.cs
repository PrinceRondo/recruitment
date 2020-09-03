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
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext dbContext;

        public GradeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Grade> FindByNameAsync(string name)
        {
            return await dbContext.Grades.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Grade>> GetAllGrade()
        {
            return await dbContext.Grades.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(Grade model)
        {
            ResponseModel response = new ResponseModel();
            var newGrade = new Grade()
            {
                Name = model.Name
            };
            if (model.Name.Any())
            {
                dbContext.Grades.Add(newGrade);
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
                    dbContext.Grades.Local.Clear();
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
