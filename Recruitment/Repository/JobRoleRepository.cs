using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModels;

namespace Recruitment.Repository
{
    public class JobRoleRepository : IJobRoleRepository
    {
        private readonly AppDbContext dbContext;

        public JobRoleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ResponseModel> DeleteAsync(long id)
        {

            ResponseModel response = new ResponseModel();
            try
            {
                JobRoles role = await dbContext.JobRoles.FindAsync(id);
                if (role != null)
                {
                    dbContext.JobRoles.Remove(role);
                    await dbContext.SaveChangesAsync();
                    response.message = "Record deleted successfully";
                    response.code = 200;
                }
                else
                {
                    response.message = "Record not found";
                    response.code = 404;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.JobRoles.Local.Clear();
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            return response;
        }

        public async Task<JobRoles> FindByNameAsync(string name)
        {
            return await dbContext.JobRoles.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<JobRoles>> GetAllJobRoles()
        {
            return await dbContext.JobRoles.ToListAsync();
        }

        public async Task<IEnumerable<JobRoles>> GetAllJobRolesByIndustry(long industryId)
        {
            return await dbContext.JobRoles.Where(x=>x.IndustryId == industryId).ToListAsync();
        }

        public async Task<JobRoles> GetJobRoleById(long Id)
        {
            return await dbContext.JobRoles.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(JobRoles model)
        {

            ResponseModel response = new ResponseModel();
            Industry industry = await dbContext.Industries.Where(x => x.Id == model.IndustryId).FirstOrDefaultAsync();
            if (industry != null)
            {
                JobRoles jobRole = await dbContext.JobRoles.FirstOrDefaultAsync(x => x.Name.ToLower() == model.Name.ToLower() && x.IndustryId == model.IndustryId);
                if (jobRole == null)
                {
                    var newRole = new JobRoles()
                    {
                        Name = model.Name,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        IndustryId = model.IndustryId,
                        CreatedBy = model.CreatedBy
                    };
                    if (model.Name.Any())
                    {
                        dbContext.JobRoles.Add(newRole);
                        try
                        {
                            await dbContext.SaveChangesAsync();
                            response.message = "Saved Successfully";
                            response.code = 200;
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine($"Save Partner Status Error: {ex}");
                            response.message = ex.Message;
                            response.code = 404;
                            dbContext.JobRoles.Local.Clear();
                            ErrorLog log = new ErrorLog();
                            log.ErrorDate = DateTime.Now;
                            log.ErrorMessage = ex.Message;
                            log.ErrorSource = ex.Source;
                            log.ErrorStackTrace = ex.StackTrace;
                            dbContext.ErrorLogs.Add(log);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    response.message = "Job Role has already been saved for this industry";
                    response.code = 200;
                }
            }
            else
            {
                response.message = "Industry doesn't exit";
                response.code = 400;
            }
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(long id, JobRoles model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                Industry industry = await dbContext.Industries.Where(x => x.Id == model.IndustryId).FirstOrDefaultAsync();
                if (industry != null)
                {
                    JobRoles role = await dbContext.JobRoles.FirstOrDefaultAsync(x => x.Id == id);
                    if (role != null)
                    {
                        role.DateUpdated = DateTime.Now;
                        role.IndustryId = model.IndustryId;
                        role.Name = model.Name;
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Job role updated successfully";
                    }
                    else
                    {
                        response.code = 404;
                        response.message = "Record not found";
                    }
                }
                else
                {
                    response.message = "Industry doesn't exit";
                    response.code = 400;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 401;
                dbContext.JobRoles.Local.Clear();
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            return response;
        }
    }
}
