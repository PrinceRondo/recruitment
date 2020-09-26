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
    public class UserFunctionRepository : IUserFunctionRepository
    {
        private readonly AppDbContext dbContext;

        public UserFunctionRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserFunction> FindByNameAsync(string name)
        {
            return await dbContext.UserFunctions.Where(x => x.Function.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserFunction>> GetAllFunction()
        {
            return await dbContext.UserFunctions.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(UserFunction model)
        {
            ResponseModel response = new ResponseModel();
            var newFunction = new UserFunction()
            {
                Function = model.Function
            };
            if (model.Function.Any())
            {
                dbContext.UserFunctions.Add(newFunction);
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
                    dbContext.UserFunctions.Local.Clear();
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
