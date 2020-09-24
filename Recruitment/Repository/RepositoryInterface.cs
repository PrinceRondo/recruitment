using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModel;
using Recruitment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Repository
{
    public interface IUserRepository
    {
        Task<ResponseModel> ApplicantRegistration(ApplicantProfileViewModel model);
        Task<ResponseModel> OrganizationRegistration(OrganizationProfileViewModel model);
        Task<ResponseModel> OrganizationUserRegistration(OrganizationUserViewModel model);
        Task<object> Login(LoginViewModel model);
        Task<ResponseModel> ConfirmEmail(string email);
    }

    public interface IUserProfileRepository
    {
        Task<ResponseModel> UpdateOrganizationProfileAsync(long id, UpdateProfileViewModel model);
        Task<ResponseModel> UpdateApplicantProfileAsync(long id, UpdateProfileViewModel model);
        Task<ResponseModel> UpdateOrganizationUserProfileAsync(long id, UpdateProfileViewModel model);
        Task<IEnumerable<OrganizationProfile>> GetAllOrganizationProfile();
        Task<IEnumerable<ApplicantProfile>> GetAllApplicantProfile();
        Task<IEnumerable<OrganizationUsersInfo>> GetAllOrganizationUserProfile(long OrganizationId);
        Task<OrganizationProfile> GetOrganizationProfileById(long id);
        Task<ApplicantProfile> GetApplicantProfileById(long id);
        Task<OrganizationUsersInfo> GetOrganizationUserProfileById(long id);

    }
    public interface IGenderRepository
    {
        Task<Gender> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(Gender gender);
        Task<IEnumerable<Gender>> GetAllGender();
    }
    public interface IMaritalStatusRepository
    {
        Task<MaritalStatus> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(MaritalStatus status);
        Task<IEnumerable<MaritalStatus>> GetAllMaritalStatus();
    }

    public interface ILevelRepository
    {
        Task<ApplicantLevel> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(ApplicantLevel level);
        Task<IEnumerable<ApplicantLevel>> GetAllLevel();
    }
    public interface IUserAccessTypeRepository
    {
        Task<UserAccessType> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(UserAccessType model);
        Task<IEnumerable<UserAccessType>> GetAllAccessType();
    }
    public interface IQualificationRepository
    {
        Task<Qualification> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(Qualification model);
        Task<IEnumerable<Qualification>> GetAllQualification();
    }

    public interface IInstitutionRepository
    {
        Task<Institution> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(Institution model);
        Task<IEnumerable<Institution>> GetAllInstitution();
    }
    public interface ICourseRepository
    {
        Task<Course> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(Course model);
        Task<IEnumerable<Course>> GetAllCourse();
    }
    public interface IGradeRepository
    {
        Task<Grade> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(Grade model);
        Task<IEnumerable<Grade>> GetAllGrade();
    }
    public interface IIndustryRepository
    {
        Task<Industry> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(Industry model);
        Task<IEnumerable<Industry>> GetAllIndustry();
    }
    public interface IJobRoleRepository
    {
        Task<JobRoles> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(JobRoles model);
        Task<ResponseModel> UpdateAsync(long id, JobRoles model);
        Task<ResponseModel> DeleteAsync(long id);
        Task<IEnumerable<JobRoles>> GetAllRole();
        Task<IEnumerable<JobRoles>> GetAllRoleByIndustry(long industryId);
    }
    public interface IJobTypeRepository
    {
        Task<JobTypes> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(JobTypes model);
        Task<IEnumerable<JobTypes>> GetAllJobType();
    }
    public interface IBioDataRepository
    {
        //Task<MaritalStatus> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(ApplicantBiodataViewModel model);
        Task<ResponseModel> UpdateAsync(int id, ApplicantBiodataViewModel model);
        Task<IEnumerable<ApplicantBiodataViewModel>> GetAllBiodata();
        Task<ApplicantBiodataViewModel> GetBiodataByUserId(string userId);
        Task<ApplicantBiodataViewModel> GetBiodataById(int id);
        Task<ResponseModel> DeleteAsync(int id);
    }

    public interface IAcademicsRepository
    {
        //Task<MaritalStatus> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(ApplicantAcademicsViewModel model);
        Task<ResponseModel> UpdateAsync(int id, ApplicantAcademicsViewModel model);
        Task<IEnumerable<ApplicantAcademicsViewModel>> GetAllApplicantQualification();
        Task<IEnumerable<ApplicantAcademicsViewModel>> GetAllByUserId(string userId);
        Task<IEnumerable<ApplicantAcademicsViewModel>> GetAllByQualificationId(int qualificationId);
        Task<ApplicantAcademicsViewModel> GetQualificationById(int id);
        Task<ResponseModel> DeleteAsync(int id);
    }

    public interface IDocumentRepository
    {
        //Task<MaritalStatus> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(ApplicantDocumentViewModel model);
        Task<ResponseModel> UpdateAsync(int id, ApplicantDocumentViewModel model);
        Task<IEnumerable<ApplicantDocumentViewModel>> GetAllApplicantDocument();
        Task<IEnumerable<ApplicantDocumentViewModel>> GetAllByUserId(string userId);
        Task<IEnumerable<ApplicantDocumentViewModel>> GetAllByCategoryId(int categoryId);
        Task<IEnumerable<ApplicantDocumentViewModel>> GetAllByTypeId(int typeId);
        Task<ApplicantDocumentViewModel> GetDocumentById(int id);
        Task<ResponseModel> DeleteAsync(int id);
    }

    public interface IExperienceRepository
    {
        //Task<MaritalStatus> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(ApplicantExperienceViewModel model);
        Task<ResponseModel> UpdateAsync(long id, ApplicantExperienceViewModel model);
        Task<IEnumerable<ApplicantExperienceViewModel>> GetAllApplicantExperience();
        Task<IEnumerable<ApplicantExperienceViewModel>> GetAllByUserId(string userId);
        Task<IEnumerable<ApplicantExperienceViewModel>> GetAllByIndustryId(int industryId);
        Task<IEnumerable<ApplicantExperienceViewModel>> GetAllByRoleId(long roleId);
        Task<ApplicantExperienceViewModel> GetExperienceById(long id);
        Task<ResponseModel> DeleteAsync(long id);
    }

    public interface IDocumentCategoryRepository
    {
        Task<DocumentCategory> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(DocumentCategory model);
        Task<IEnumerable<DocumentCategory>> GetAllCategory();
    }

    public interface IDocumentTypeRepository
    {
        Task<IEnumerable<DocumentTypeViewModel>> GetAllByCategory(int id);
        Task<ResponseModel> SaveAsync(DocumentTypeViewModel model);
        Task<IEnumerable<DocumentTypeViewModel>> GetAll();
    }

    public interface IRecruitmentLocationRepository
    {
        Task<IEnumerable<RecruitmentLocationViewModel>> GetAllByOrgnizationId(long id);
        Task<ResponseModel> SaveAsync(RecruitmentLocationViewModel model);
        Task<IEnumerable<RecruitmentLocationViewModel>> GetAll();
        Task<RecruitmentLocationViewModel> GetRecruitmentLocationById(long id);
        Task<ResponseModel> UpdateAsync(long id, RecruitmentLocationViewModel model);
        Task<ResponseModel> DeleteAsync(long id);
    }

    public interface IOrganizationDocumentRepository
    {
        Task<IEnumerable<OrganizationDocumentViewModel>> GetAllByOrgnizationId(long id);
        Task<ResponseModel> SaveAsync(OrganizationDocumentViewModel model);
        Task<IEnumerable<OrganizationDocumentViewModel>> GetAll();
        Task<OrganizationDocumentViewModel> GetDocumentById(long id);
        Task<ResponseModel> UpdateAsync(long id, OrganizationDocumentViewModel model);
        Task<ResponseModel> DeleteAsync(long id);
    }
    public interface IOrganizationDepartmentRepository
    {
        Task<IEnumerable<OrganizationDepartmentViewModel>> GetAllByOrgnizationId(long id);
        Task<IEnumerable<OrganizationDepartmentViewModel>> GetAllByRecruitmentLocation(long id);
        Task<ResponseModel> SaveAsync(OrganizationDepartmentViewModel model);
        Task<IEnumerable<OrganizationDepartmentViewModel>> GetAll();
        Task<OrganizationDepartmentViewModel> GetDepartmentById(long id);
        Task<ResponseModel> UpdateAsync(long id, OrganizationDepartmentViewModel model);
        Task<ResponseModel> DeleteAsync(long id);
    }

    public interface IOrganizationJobRoleRepository
    {
        Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAllByOrgnizationId(long id);
        Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAllByRecruitmentLocationId(long id);
        Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAllByDepartmentId(long id);
        Task<ResponseModel> SaveAsync(OrganizationJobeRoleViewModel model);
        Task<IEnumerable<OrganizationJobeRoleViewModel>> GetAll();
        Task<OrganizationJobeRoleViewModel> GetJobRoleById(long id);
        Task<ResponseModel> UpdateAsync(long id, OrganizationJobeRoleViewModel model);
        Task<ResponseModel> DeleteAsync(long id);
    }

    public interface IOrganizationRoleRepository
    {
        Task<IEnumerable<OrganizationRoleViewModel>> GetAllByOrgnizationId(long id);
        Task<ResponseModel> SaveAsync(OrganizationRoleViewModel model);
        Task<IEnumerable<OrganizationRoleViewModel>> GetAll();
        Task<OrganizationRoleViewModel> GetRoleById(long id);
        Task<ResponseModel> UpdateAsync(long id, OrganizationRoleViewModel model);
        Task<ResponseModel> DeleteAsync(long id);
    }

    public interface IOrganizationUserRoleRepository
    {
        Task<IEnumerable<OrganizationUserRoleViewModel>> GetAllByOrgnizationId(long id);
        Task<ResponseModel> SaveAsync(OrganizationUserRoleViewModel model);
        Task<IEnumerable<OrganizationUserRoleViewModel>> GetAll();
        Task<IEnumerable<OrganizationUserRoleViewModel>> GetAllByUserId(string UserId);
        Task<OrganizationUserRoleViewModel> GetUserRoleById(long id);
        Task<ResponseModel> UpdateAsync(long id, OrganizationUserRoleViewModel model);
        Task<ResponseModel> RemoveUserRoleAsync(RemoveUserRoleViewModel model);
        Task<ResponseModel> DeleteAsync(long id);
    }

    public interface IRecruitmentLocationTypeRepository
    {
        Task<RecruitmentLocationType> FindByNameAsync(string name);
        Task<ResponseModel> SaveAsync(RecruitmentLocationType model);
        Task<IEnumerable<RecruitmentLocationType>> GetAllLocationType();
    }
}
