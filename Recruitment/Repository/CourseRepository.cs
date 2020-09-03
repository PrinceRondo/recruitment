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
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext dbContext;

        public CourseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Course> FindByNameAsync(string name)
        {
            return await dbContext.Courses.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCourse()
        {
            return await dbContext.Courses.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(Course model)
        {
            ResponseModel response = new ResponseModel();
            var newCourse = new Course()
            {
                Name = model.Name
            };
            if (model.Name.Any())
            {
                dbContext.Courses.Add(newCourse);
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
                    dbContext.Courses.Local.Clear();
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
