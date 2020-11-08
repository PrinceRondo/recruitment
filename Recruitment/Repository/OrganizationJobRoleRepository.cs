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
    public class OrganizationJobRoleRepository : IOrganizationJobRoleRepository
    {
        private readonly AppDbContext dbContext;

        public OrganizationJobRoleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationJobRoles jobRole = await dbContext.OrganisationJobRoles.FindAsync(id);
                if (jobRole != null)
                {
                    dbContext.OrganisationJobRoles.Remove(jobRole);
                    await dbContext.SaveChangesAsync();
                    response.message = "Role deleted successfully";
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
                response.code = 400;
                dbContext.OrganisationJobRoles.Local.Clear();
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

        public async Task<IEnumerable<JobRoleResponseModel>> GetAll()
        {
            //return await dbContext.OrganisationJobRoles.Select(x => new OrganizationJobeRoleViewModel
            //{
            //    DateCreated = x.DateCreated,
            //    DateUpdated = x.DateUpdated,
            //    DepartmentId = x.DepartmentId,
            //    Department = x.Department.DepartmentName,
            //    Id = x.Id,
            //    RoleName = x.JobRoleName
            //}).ToListAsync();

            IList<JobRoleResponseModel> responseModel = new List<JobRoleResponseModel>();
            IList<OrganizationJobRoles> jobRoles = await dbContext.OrganisationJobRoles.ToListAsync();
            foreach (OrganizationJobRoles job in jobRoles)
            {
                OrganizationDepartments departments = await dbContext.OrganizationDepartments.Where(x => x.Id == job.DepartmentId).FirstOrDefaultAsync();
                var jobProfile = await dbContext.JobProfiles.Where(x => x.Id == job.JobProfileId).FirstOrDefaultAsync();
                var jobDetailList = await dbContext.JobProfileDetails.Where(x => x.JobProfileId == job.JobProfileId).Select(x => new JobProfileDetailViewModel
                {
                    Comment = x.Comment,
                    DateCreated = x.DateCreated,
                    Id = x.Id,
                    IsMandatory = x.IsMandatory,
                    JobElementId = x.JobElementId,
                    JobProfileElement = x.JobProfileElement.Element,
                    LastUpdated = x.LastUpdated,
                    MaxRequirement = x.MaxRequirement,
                    MinRequirement = x.MinRequirement,
                    UserId = x.CreatedBy
                }).ToListAsync();

                JobRoleResponseModel model = new JobRoleResponseModel
                {
                    Id = job.Id,
                    JobRole = job.JobRoleName,
                    DepartmentId = (long)job.DepartmentId,
                    Department = departments.DepartmentName,
                    JobProfileId = job.JobProfileId,
                    JobProfile = jobProfile.Description,
                    JobProfileDetails = jobDetailList,
                    Message = "Success",
                    Code = 200
                };

                responseModel.Add(model);
            }
            return responseModel;
        }

        public async Task<IEnumerable<JobRoleResponseModel>> GetAllByDepartmentId(long id)
        {
            IList<JobRoleResponseModel> responseModel = new List<JobRoleResponseModel>();
            IList<OrganizationJobRoles> jobRoles = await dbContext.OrganisationJobRoles.Where(x=>x.DepartmentId == id).ToListAsync();
            foreach (OrganizationJobRoles job in jobRoles)
            {
                OrganizationDepartments departments = await dbContext.OrganizationDepartments.Where(x => x.Id == job.DepartmentId).FirstOrDefaultAsync();
                var jobProfile = await dbContext.JobProfiles.Where(x => x.Id == job.JobProfileId).FirstOrDefaultAsync();
                var jobDetailList = await dbContext.JobProfileDetails.Where(x => x.JobProfileId == job.JobProfileId).Select(x => new JobProfileDetailViewModel
                {
                    Comment = x.Comment,
                    DateCreated = x.DateCreated,
                    Id = x.Id,
                    IsMandatory = x.IsMandatory,
                    JobElementId = x.JobElementId,
                    JobProfileElement = x.JobProfileElement.Element,
                    LastUpdated = x.LastUpdated,
                    MaxRequirement = x.MaxRequirement,
                    MinRequirement = x.MinRequirement,
                    UserId = x.CreatedBy
                }).ToListAsync();

                JobRoleResponseModel model = new JobRoleResponseModel
                {
                    Id = job.Id,
                    JobRole = job.JobRoleName,
                    DepartmentId = (long)job.DepartmentId,
                    Department = departments.DepartmentName,
                    JobProfileId = job.JobProfileId,
                    JobProfile = jobProfile.Description,
                    JobProfileDetails = jobDetailList,
                    Message = "Success",
                    Code = 200
                };

                responseModel.Add(model);
            }
            return responseModel;
        }

        public async Task<IEnumerable<JobRoleResponseModel>> GetAllByOrgnizationId(long id)
        {
            IList<JobRoleResponseModel> responseModel = new List<JobRoleResponseModel>();
            IList<OrganizationJobRoles> jobRoles = await dbContext.OrganisationJobRoles.Where(x => x.JobProfile.OrganizationId == id).ToListAsync();
            foreach (OrganizationJobRoles job in jobRoles)
            {
                OrganizationDepartments departments = await dbContext.OrganizationDepartments.Where(x => x.Id == job.DepartmentId).FirstOrDefaultAsync();
                var jobProfile = await dbContext.JobProfiles.Where(x => x.Id == job.JobProfileId).FirstOrDefaultAsync();
                var jobDetailList = await dbContext.JobProfileDetails.Where(x => x.JobProfileId == job.JobProfileId).Select(x => new JobProfileDetailViewModel
                {
                    Comment = x.Comment,
                    DateCreated = x.DateCreated,
                    Id = x.Id,
                    IsMandatory = x.IsMandatory,
                    JobElementId = x.JobElementId,
                    JobProfileElement = x.JobProfileElement.Element,
                    LastUpdated = x.LastUpdated,
                    MaxRequirement = x.MaxRequirement,
                    MinRequirement = x.MinRequirement,
                    UserId = x.CreatedBy
                }).ToListAsync();

                JobRoleResponseModel model = new JobRoleResponseModel
                {
                    Id = job.Id,
                    JobRole = job.JobRoleName,
                    DepartmentId = (long)job.DepartmentId,
                    Department = departments.DepartmentName,
                    JobProfileId = job.JobProfileId,
                    JobProfile = jobProfile.Description,
                    JobProfileDetails = jobDetailList,
                    Message = "Success",
                    Code = 200
                };

                responseModel.Add(model);
            }
            return responseModel;
        }

        public async Task<IEnumerable<JobRoleResponseModel>> GetAllByRecruitmentLocationId(long id)
        {
            IList<JobRoleResponseModel> responseModel = new List<JobRoleResponseModel>();
            IList<OrganizationJobRoles> jobRoles = await dbContext.OrganisationJobRoles.Where(x => x.Department.RecruitmentLocationId == id).ToListAsync();
            foreach (OrganizationJobRoles job in jobRoles)
            {
                OrganizationDepartments departments = await dbContext.OrganizationDepartments.Where(x => x.Id == job.DepartmentId).FirstOrDefaultAsync();
                var jobProfile = await dbContext.JobProfiles.Where(x => x.Id == job.JobProfileId).FirstOrDefaultAsync();
                var jobDetailList = await dbContext.JobProfileDetails.Where(x => x.JobProfileId == job.JobProfileId).Select(x => new JobProfileDetailViewModel
                {
                    Comment = x.Comment,
                    DateCreated = x.DateCreated,
                    Id = x.Id,
                    IsMandatory = x.IsMandatory,
                    JobElementId = x.JobElementId,
                    JobProfileElement = x.JobProfileElement.Element,
                    LastUpdated = x.LastUpdated,
                    MaxRequirement = x.MaxRequirement,
                    MinRequirement = x.MinRequirement,
                    UserId = x.CreatedBy
                }).ToListAsync();

                JobRoleResponseModel model = new JobRoleResponseModel
                {
                    Id = job.Id,
                    JobRole = job.JobRoleName,
                    DepartmentId = (long)job.DepartmentId,
                    Department = departments.DepartmentName,
                    JobProfileId = job.JobProfileId,
                    JobProfile = jobProfile.Description,
                    JobProfileDetails = jobDetailList,
                    Message = "Success",
                    Code = 200
                };

                responseModel.Add(model);
            }
            return responseModel;
        }

        public async Task<JobRoleResponseModel> GetJobRoleById(long id)
        {
            JobRoleResponseModel response = new JobRoleResponseModel();
            OrganizationJobRoles jobRole = await dbContext.OrganisationJobRoles.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (jobRole != null)
            {
                OrganizationDepartments departments = await dbContext.OrganizationDepartments.Where(x => x.Id == jobRole.DepartmentId).FirstOrDefaultAsync();
                var jobProfile = await dbContext.JobProfiles.Where(x => x.Id == jobRole.JobProfileId).FirstOrDefaultAsync();
                var jobDetailList = await dbContext.JobProfileDetails.Where(x => x.JobProfileId == jobRole.JobProfileId).Select(x => new JobProfileDetailViewModel
                {
                    Comment = x.Comment,
                    DateCreated = x.DateCreated,
                    Id = x.Id,
                    IsMandatory = x.IsMandatory,
                    JobElementId = x.JobElementId,
                    JobProfileElement = x.JobProfileElement.Element,
                    LastUpdated = x.LastUpdated,
                    MaxRequirement = x.MaxRequirement,
                    MinRequirement = x.MinRequirement,
                    UserId = x.CreatedBy
                }).ToListAsync();

                response.Id = jobRole.Id;
                response.JobRole = jobRole.JobRoleName;
                response.DepartmentId = (long)departments.Id;
                response.Department = departments.DepartmentName;
                response.JobProfileId = jobRole.JobProfileId;
                response.JobProfile = jobProfile.Description;
                response.JobProfileDetails = jobDetailList;
                response.Message = "Success";
                response.Code = 200;
            }
            else
            {
                response.Message = "Record not found";
                response.Code = 401;
            }
            return response;
        }

        public async Task<ResponseModel> SaveAsync(OrganizationJobeRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                //checck if department exist
                OrganizationDepartments department = await dbContext.OrganizationDepartments.Where(x => x.Id == model.DepartmentId).FirstOrDefaultAsync();
                if (department != null)
                {
                    //check if job profile exist
                    JobProfile profile = await dbContext.JobProfiles.Where(x => x.Id == model.JobProfileId).FirstOrDefaultAsync();
                    if (profile != null)
                    {
                        OrganizationJobRoles jobRole = await dbContext.OrganisationJobRoles.Where(x =>
                        x.JobRoleName.ToLower() == model.RoleName.ToLower() && x.DepartmentId == model.DepartmentId).FirstOrDefaultAsync();
                        if (jobRole == null)
                        {
                            OrganizationJobRoles role = new OrganizationJobRoles()
                            {
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                DepartmentId = model.DepartmentId,
                                JobProfileId = model.JobProfileId,
                                JobRoleName = model.RoleName
                            };
                            dbContext.OrganisationJobRoles.Add(role);
                            await dbContext.SaveChangesAsync();
                            response.code = 200;
                            response.message = "Role saved successfully";
                        }
                        else
                        {
                            response.code = 401;
                            response.message = "This role has been added already for this department";
                        }
                    }
                    else
                    {
                        response.code = 402;
                        response.message = "Job Profile doesn't exist";
                    }
                }
                else
                {
                    response.code = 400;
                    response.message = "Department doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.OrganisationJobRoles.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(long id, OrganizationJobeRoleViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                OrganizationJobRoles role = await dbContext.OrganisationJobRoles.FirstOrDefaultAsync(x => x.Id == id);
                if (role != null)
                {
                    JobProfile profile = await dbContext.JobProfiles.Where(x => x.Id == model.JobProfileId).FirstOrDefaultAsync();
                    if (profile != null)
                    {
                        OrganizationDepartments department = await dbContext.OrganizationDepartments.Where(x => x.Id == model.DepartmentId).FirstOrDefaultAsync();
                        if (department != null)
                        {
                            role.DateUpdated = DateTime.Now;
                            role.DepartmentId = department.Id;
                            role.JobProfileId = model.JobProfileId;
                            role.JobRoleName = model.RoleName;
                            await dbContext.SaveChangesAsync();
                            response.code = 200;
                            response.message = "Role updated successfully";
                        }
                        else
                        {
                            response.code = 400;
                            response.message = "Department doesn't exist";
                        }
                    }
                    else
                    {
                        response.code = 401;
                        response.message = "Job Profile doesn't exist";
                    }
                }
                else
                {
                    response.code = 404;
                    response.message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 400;
                dbContext.OrganisationJobRoles.Local.Clear();
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
