using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recruitment.Data;
using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModels;

namespace Recruitment.Repository
{
    public class AcademicsRepository : IAcademicsRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AcademicsRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<ResponseModel> DeleteAsync(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ApplicantAcademics academics = await dbContext.ApplicantAcademics.FindAsync(id);
                if (academics != null)
                {
                    dbContext.ApplicantAcademics.Remove(academics);
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
                dbContext.ApplicantAcademics.Local.Clear();
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

        public async Task<IEnumerable<ApplicantAcademicsViewModel>> GetAllApplicantQualification()
        {
            return await dbContext.ApplicantAcademics.Select(x => new ApplicantAcademicsViewModel
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                EndDate = x.EndDate,
                Id = x.Id,
                InstitutionId = x.InstitutionId,
                InstitutionName = x.InstitutionName,
                IsCourseVerified = x.IsCourseVerified,
                IsInstitutionVerified = x.IsInstitutionVerified,
                QualificationId = x.QualificationId,
                QualificationName = x.Qualification.Name,
                StartDate = x.StartDate,
                userId = x.UserId,
                GradeId = x.GradeId,
                GradeName=x.GradeName,
                IsGradeVerified = x.IsGradeVerified
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantAcademicsViewModel>> GetAllByQualificationId(int qualificationId)
        {
            return await dbContext.ApplicantAcademics.Where(x=>x.QualificationId == qualificationId).Select(x => new ApplicantAcademicsViewModel
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                EndDate = x.EndDate,
                Id = x.Id,
                InstitutionId = x.InstitutionId,
                InstitutionName = x.InstitutionName,
                IsCourseVerified = x.IsCourseVerified,
                IsInstitutionVerified = x.IsInstitutionVerified,
                QualificationId = x.QualificationId,
                QualificationName = x.Qualification.Name,
                StartDate = x.StartDate,
                userId = x.UserId,
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsGradeVerified = x.IsGradeVerified
            }).ToListAsync();
        }

        public async Task<IEnumerable<ApplicantAcademicsViewModel>> GetAllByUserId(string userId)
        {
            return await dbContext.ApplicantAcademics.Where(x => x.UserId == userId).Select(x => new ApplicantAcademicsViewModel
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                EndDate = x.EndDate,
                Id = x.Id,
                InstitutionId = x.InstitutionId,
                InstitutionName = x.InstitutionName,
                IsCourseVerified = x.IsCourseVerified,
                IsInstitutionVerified = x.IsInstitutionVerified,
                QualificationId = x.QualificationId,
                QualificationName = x.Qualification.Name,
                StartDate = x.StartDate,
                userId = x.UserId,
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsGradeVerified = x.IsGradeVerified
            }).ToListAsync();
        }

        public async Task<ApplicantAcademicsViewModel> GetQualificationById(int id)
        {
            return await dbContext.ApplicantAcademics.Where(x => x.Id == id).Select(x => new ApplicantAcademicsViewModel
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                EndDate = x.EndDate,
                Id = x.Id,
                InstitutionId = x.InstitutionId,
                InstitutionName = x.InstitutionName,
                IsCourseVerified = x.IsCourseVerified,
                IsInstitutionVerified = x.IsInstitutionVerified,
                QualificationId = x.QualificationId,
                QualificationName = x.Qualification.Name,
                StartDate = x.StartDate,
                userId = x.UserId,
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsGradeVerified = x.IsGradeVerified
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(ApplicantAcademicsViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool isInstitutionVerified = true;
                string institition = "";
                int institutionId;
                bool isCourseVerified = true;
                string course = "";
                int courseId;
                bool isGradeVerified = true;
                string grade = "";
                int gradeId;
                var user = await userManager.FindByIdAsync(model.userId);
                if (user != null)
                {
                    //ApplicantAcademics academics = await dbContext.ApplicantAcademics.FirstOrDefaultAsync(x => x.UserId == model.userId && x.QualificationId == model.QualificationId);
                    //if (academics == null)
                    //{
                        //If other institution or no institution(pri/sec) is selected
                        if(model.InstitutionId == 1 || model.InstitutionId == 0)
                        {
                            isInstitutionVerified = false;
                            institition = model.InstitutionName;
                            institutionId = 1;
                        }
                        else
                        {
                            Institution institutions = dbContext.Institutions.FirstOrDefault(x => x.Id == model.InstitutionId);
                            institition = institutions.Name;
                            institutionId = institutions.Id;
                        }
                        //If other course or no course(pri/sec) is selected
                        if (model.CourseId == 1 || model.CourseId == 0)
                        {
                            isCourseVerified = false;
                            course = model.CourseName;
                            courseId = 1;
                        }
                        else
                        {
                            Course courses = dbContext.Courses.FirstOrDefault(x => x.Id == model.CourseId);
                            course = courses.Name;
                            courseId = courses.Id;
                        }
                        //If other grade or no grade(pri/sec) is selected
                        if (model.GradeId == 1 || model.GradeId == 0)
                        {
                            isGradeVerified = false;
                            grade = model.GradeName;
                            gradeId = 1;
                        }
                        else
                        {
                            Grade grades = dbContext.Grades.FirstOrDefault(x => x.Id == model.GradeId);
                            grade = grades.Name;
                            gradeId = grades.Id;
                        }
                        ApplicantAcademics applicant = new ApplicantAcademics()
                        {
                            CourseId = courseId,
                            CourseName = course,
                            GradeId = gradeId,
                            GradeName = grade,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now,
                            EndDate = model.EndDate,
                            InstitutionId = institutionId,
                            InstitutionName = institition,
                            QualificationId = model.QualificationId,
                            StartDate = model.StartDate,
                            UserId = model.userId,
                            IsInstitutionVerified = isInstitutionVerified,
                            IsCourseVerified = isCourseVerified,
                            IsGradeVerified = isGradeVerified
                        };
                        dbContext.ApplicantAcademics.Add(applicant);
                        await dbContext.SaveChangesAsync();
                        response.code = 200;
                        response.message = "Qualification saved successfully";
                    //}
                    //else
                    //{
                    //    response.code = 404;
                    //    response.message = "Qualification has been saved already";
                    //}
                }
                else
                {
                    response.code = 404;
                    response.message = "User doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 404;
                dbContext.ApplicantAcademics.Local.Clear();
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

        public async Task<ResponseModel> UpdateAsync(int id, ApplicantAcademicsViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                bool isInstitutionVerified = true;
                string institition = "";
                int institutionId;
                bool isCourseVerified = true;
                string course = "";
                int courseId;
                bool isGradeVerified = true;
                string grade = "";
                int gradeId;
                ApplicantAcademics academics = await dbContext.ApplicantAcademics.FirstOrDefaultAsync(x => x.Id == id);
                if (academics != null)
                {
                    //If other institution or no institution(pri/sec) is selected
                    if (model.InstitutionId == 1 || model.InstitutionId == 0)
                    {
                        isInstitutionVerified = false;
                        institition = model.InstitutionName;
                        institutionId = 1;
                    }
                    else
                    {
                        Institution institutions = dbContext.Institutions.FirstOrDefault(x => x.Id == model.InstitutionId);
                        institition = institutions.Name;
                        institutionId = institutions.Id;
                    }
                    //If other course or no course(pri/sec) is selected
                    if (model.CourseId == 1 || model.CourseId == 0)
                    {
                        isCourseVerified = false;
                        course = model.CourseName;
                        courseId = 1;
                    }
                    else
                    {
                        Course courses = dbContext.Courses.FirstOrDefault(x => x.Id == model.InstitutionId);
                        course = courses.Name;
                        courseId = courses.Id;
                    }
                    //If other grade or no grade(pri/sec) is selected
                    if (model.GradeId == 1 || model.GradeId == 0)
                    {
                        isGradeVerified = false;
                        grade = model.GradeName;
                        gradeId = 1;
                    }
                    else
                    {
                        Grade grades = dbContext.Grades.FirstOrDefault(x => x.Id == model.GradeId);
                        grade = grades.Name;
                        gradeId = grades.Id;
                    }

                    academics.CourseId = courseId;
                    academics.CourseName = course;
                    academics.GradeId = gradeId;
                    academics.GradeName = grade;
                    academics.DateUpdated = DateTime.Now;
                    academics.EndDate = model.EndDate;
                    academics.InstitutionId = institutionId;
                    academics.InstitutionName = institition;
                    academics.IsCourseVerified = isCourseVerified;
                    academics.IsInstitutionVerified = isInstitutionVerified;
                    academics.IsGradeVerified = isGradeVerified;
                    academics.QualificationId = model.QualificationId;
                    academics.StartDate = model.StartDate;
                    await dbContext.SaveChangesAsync();
                    response.code = 200;
                    response.message = "Qualification updated successfully";
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
                dbContext.ApplicantAcademics.Local.Clear();
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
