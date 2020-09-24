﻿using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Repository
{
    public class UserAccessTypeRepository : IUserAccessTypeRepository
    {
        private readonly AppDbContext dbContext;

        public UserAccessTypeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserAccessType> FindByNameAsync(string name)
        {
            return await dbContext.UserAccessTypes.Where(x => x.type.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserAccessType>> GetAllAccessType()
        {
            return await dbContext.UserAccessTypes.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(UserAccessType model)
        {
            ResponseModel response = new ResponseModel();
            var newType = new UserAccessType()
            {
                type = model.type
            };
            if (model.type.Any())
            {
                dbContext.UserAccessTypes.Add(newType);
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
                    dbContext.UserAccessTypes.Local.Clear();
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
