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
    public class RecruitmentLocationTypeRepository : IRecruitmentLocationTypeRepository
    {
        private readonly AppDbContext dbContext;

        public RecruitmentLocationTypeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RecruitmentLocationType> FindByNameAsync(string name)
        {
            return await dbContext.RecruitmentLocationTypes.Where(x => x.LocationType.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RecruitmentLocationType>> GetAllLocationType()
        {
            return await dbContext.RecruitmentLocationTypes.ToListAsync();
        }

        public async Task<ResponseModel> SaveAsync(RecruitmentLocationType model)
        {
            ResponseModel response = new ResponseModel();
            var newType = new RecruitmentLocationType()
            {
                LocationType = model.LocationType
            };
            if (model.LocationType.Any())
            {
                dbContext.RecruitmentLocationTypes.Add(newType);
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
                    dbContext.RecruitmentLocationTypes.Local.Clear();
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
