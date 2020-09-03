using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Recruitment.Data;
using Recruitment.Helper;
using Recruitment.Models;
using Recruitment.RespondModels;
using Recruitment.ViewModel;
using Recruitment.ViewModels;

namespace Recruitment.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly Mailer mailer;

        public UserRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole>
            roleManager, Mailer mailer)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mailer = mailer;
        }
        public async Task<ResponseModel> ApplicantRegistration(ApplicantProfileViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                //if (ModelState.IsValid)
                //{
                var user = new ApplicationUser
                {
                    UserName = model.email,
                    Email = model.email,
                    FirstName = model.firstName,
                    LastName = model.lastName
                };
                if (model.password.Count() < 6)
                {
                    responseModel.message = "Password should not be lesser than 6 characters";
                    responseModel.code = 404;
                    return responseModel;
                }
                var userExist = await userManager.FindByEmailAsync(user.Email);
                if (userExist == null)
                {
                    var result = await userManager.CreateAsync(user, model.password);
                    result = await userManager.AddToRoleAsync(user, "user");
                    if (result.Succeeded)
                    {
                        var getNewUser = await userManager.FindByEmailAsync(model.email);
                        if (getNewUser != null)
                        {
                            var newUser = new ApplicantProfile
                            {
                                UserId = getNewUser.Id,
                                Email = model.email,
                                FirstName = model.firstName,
                                LastName = model.lastName,
                                Address = model.address,
                                PhoneNumber = model.phoneNumber,
                                CreatedAt = DateTime.Now,
                                ModifiedAt = DateTime.Now,
                                IsActive = true,
                            };
                            dbContext.ApplicantProfiles.Add(newUser);
                            dbContext.SaveChanges();

                            string confirmationLink = "http://recruitmentapi.ivslng.com/api/user/confirmEmail?email=" + model.email;
                            //string confirmationLink = "https://localhost:44368/api/user/confirmEmail?email=" + model.email;
                            BodyBuilder bodyBuilder = new BodyBuilder();
                            MimeMessage message = new MimeMessage();
                            message.Subject = "Applicant Registration";
                            bodyBuilder.HtmlBody = "<h1>Recruitment Email Confirmation</h1><br/><h3>Click <a href=" + confirmationLink + ">here</a> to confirm your email</h3>";
                            mailer.mailing(model.firstName, model.email, bodyBuilder, message);
                            //BackgroundJob.Enqueue(()=> helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink));
                            //BackgroundJob.Schedule(() => helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink), TimeSpan.FromSeconds(30));
                            //RecurringJob.AddOrUpdate(() => helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink), Cron.Minutely);
                            responseModel.message = "User Created Successfully";
                            responseModel.code = 200;
                        }

                    }
                    else
                    {
                        responseModel.message = "User Creation Failed";
                        responseModel.code = 404;
                    }
                }
                else
                {
                    responseModel.message = "Email has been used";
                    responseModel.code = 400;
                }
                //}
                return responseModel;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.ErrorDate = DateTime.Now;
                errorLog.ErrorMessage = ex.Message;
                errorLog.ErrorSource = ex.Source;
                errorLog.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(errorLog);
                dbContext.SaveChanges();
                responseModel.message = ex.Message;
                responseModel.code = 404;
                return responseModel;
            }
        }

        public async Task<ResponseModel> ConfirmEmail(string email)
        {
            ResponseModel responseModels = new ResponseModel();
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
                responseModels.message = "Email Confirmed";
                responseModels.code = 200;
            }
            else
            {
                responseModels.message = "Email Confirmation Failed";
                responseModels.code = 404;
            }
            return responseModels;
        }

        public async Task<object> Login(LoginViewModel model)
        {
            ResponseModel responseModels = new ResponseModel();
            try
            {
                var user = await userManager.FindByEmailAsync(model.email);
                if (user != null)
                {
                    if (!user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, model.password)))
                    {
                        responseModels.message = "Email not confirmed yet";
                        responseModels.code = 400;
                        return responseModels;
                    }

                    var result = await signInManager.PasswordSignInAsync(user.UserName, model.password, true, true);
                    if (result.Succeeded)
                    {
                        var userRoles = await userManager.GetRolesAsync(user);
                        if (userRoles.Contains("user"))
                        {
                            return await dbContext.ApplicantProfiles.Where(x => x.Email == model.email).Select(e => new ApplicantProfileViewModel
                            {
                                id = e.Id,
                                userId = e.UserId,
                                address = e.Address,
                                email = e.Email,
                                firstName = e.FirstName,
                                lastName = e.LastName,
                                phoneNumber = e.PhoneNumber,
                                createdAt = e.CreatedAt,
                                modifiedAt = e.ModifiedAt,
                                isActive = e.IsActive,
                                role = "user"
                            }).FirstOrDefaultAsync();

                        }
                        else if (userRoles.Contains("organization"))
                        {
                            return await dbContext.OrganizationProfiles.Where(x => x.Email == model.email).Select(e => new OrganizationProfileViewModel
                            {
                                id = e.Id,
                                userId = e.UserId,
                                address = e.Address,
                                email = e.Email,
                                contactFirstName = e.ContactFirstName,
                                contactLastName = e.ContactLastName,
                                phoneNumber = e.PhoneNumber,
                                companyName = e.CompanyName,
                                abbreviation = e.Abbreviation,
                                contactEmail = e.ContactEmail,
                                contactPhoneNumber = e.ContactPhoneNumber,
                                role = "organization",
                                headQuarterAddress = e.HeadQuarterAddress,
                                industry = e.Industry.Name,
                                isActive = e.IsActive,
                                dateCreated = e.DateCreated,
                                dateUpdated = e.DateUpdated
                            }).FirstOrDefaultAsync();
                        }
                        else if (userRoles.Contains("organizationuser"))
                        {
                            return await dbContext.OrganizationUsersInfo.Where(x => x.EmailAddress == model.email).Select(e => new OrganizationUserViewModel
                            {
                                DateCreated = e.DateCreated,
                                DateUpdated = e.DateUpdated,
                                EmailAddress = e.EmailAddress,
                                Firstname = e.Firstname,
                                Id = e.Id,
                                IsActive = e.IsActive,
                                Lastname = e.Lastname,
                                OrganisationId = e.OrganisationId,
                                Organization = e.Organisation.CompanyName,
                                PhoneNumber = e.PhoneNumber
                            }).FirstOrDefaultAsync();
                        }
                        else if (userRoles.Contains("admin"))
                        {
                            AdminResponseModel responseModel = new AdminResponseModel();
                            responseModel.UserId = user.Id;
                            responseModel.Role = "admin";
                            responseModel.Message = "Admin login Sucessfully";
                            responseModel.Code = 200;
                            return responseModel;
                        }
                        else
                        {
                            responseModels.message = "Invalid Login Role";
                            responseModels.code = 401;
                            return responseModels;
                        }
                    }
                    else
                    {
                        responseModels.message = "Invalid username or password";
                        responseModels.code = 404;
                        return responseModels;
                    }
                }
                else
                {
                    responseModels.message = "Invalid username";
                    responseModels.code = 404;
                    return responseModels;
                }


            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.ErrorDate = DateTime.Now;
                errorLog.ErrorMessage = ex.Message;
                errorLog.ErrorSource = ex.Source;
                errorLog.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(errorLog);
                dbContext.SaveChanges();

                responseModels.message = ex.Message;
                responseModels.code = 404;
                return responseModels;
            }
        }

        public async Task<ResponseModel> OrganizationRegistration(OrganizationProfileViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                //if (ModelState.IsValid)
                //{
                var user = new ApplicationUser
                {
                    UserName = model.email,
                    Email = model.email,
                    FirstName = model.contactFirstName,
                    LastName = model.contactLastName
                };
                if (model.password.Count() < 6)
                {
                    responseModel.message = "Password should not be lesser than 6 characters";
                    responseModel.code = 404;
                    return responseModel;
                }
                var userExist = await userManager.FindByEmailAsync(user.Email);
                if (userExist == null)
                {
                    var result = await userManager.CreateAsync(user, model.password);
                    result = await userManager.AddToRoleAsync(user, "organization");
                    if (result.Succeeded)
                    {
                        var getNewUser = await userManager.FindByEmailAsync(model.email);
                        if (getNewUser != null)
                        {
                            var newOrganization = new OrganizationProfile
                            {
                                UserId = getNewUser.Id,
                                Email = model.email,
                                Abbreviation = model.abbreviation,
                                IndustryId = model.industryId,
                                CompanyName = model.companyName,
                                HeadQuarterAddress = model.headQuarterAddress,
                                ContactEmail = model.contactEmail,
                                ContactPhoneNumber = model.contactPhoneNumber,
                                ContactFirstName = model.contactFirstName,
                                ContactLastName = model.contactLastName,
                                Address = model.address,
                                PhoneNumber = model.phoneNumber,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                IsActive = true,
                            };
                            dbContext.OrganizationProfiles.Add(newOrganization);
                            dbContext.SaveChanges();

                            string confirmationLink = "http://recruitmentapi.ivslng.com/api/user/confirmEmail?email=" + model.email;
                            //string confirmationLink = "https://localhost:44368/api/user/confirmEmail?email=" + model.email;
                            BodyBuilder bodyBuilder = new BodyBuilder();
                            MimeMessage message = new MimeMessage();
                            message.Subject = "Organization Registration";
                            bodyBuilder.HtmlBody = "<h1>Recruitment Email Confirmation</h1><br/><h3>Click <a href=" + confirmationLink + ">here</a> to confirm your company's email</h3>";
                            mailer.mailing(model.contactFirstName, model.email, bodyBuilder, message);
                            //BackgroundJob.Enqueue(()=> helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink));
                            //BackgroundJob.Schedule(() => helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink), TimeSpan.FromSeconds(30));
                            //RecurringJob.AddOrUpdate(() => helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink), Cron.Minutely);
                            responseModel.message = "Organization Created Successfully";
                            responseModel.code = 200;
                        }

                    }
                    else
                    {
                        responseModel.message = "Organization Creation Failed";
                        responseModel.code = 404;
                    }
                }
                else
                {
                    responseModel.message = "Email has been used";
                    responseModel.code = 400;
                }
                //}
                return responseModel;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.ErrorDate = DateTime.Now;
                errorLog.ErrorMessage = ex.Message;
                errorLog.ErrorSource = ex.Source;
                errorLog.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(errorLog);
                dbContext.SaveChanges();
                responseModel.message = ex.Message;
                responseModel.code = 404;
                return responseModel;
            }
        }

        public async Task<ResponseModel> OrganizationUserRegistration(OrganizationUserViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                //if (ModelState.IsValid)
                //{
                var user = new ApplicationUser
                {
                    UserName = model.EmailAddress,
                    Email = model.EmailAddress,
                    FirstName = model.Firstname,
                    LastName = model.Lastname
                };
                if (model.Password.Count() < 6)
                {
                    responseModel.message = "Password should not be lesser than 6 characters";
                    responseModel.code = 404;
                    return responseModel;
                }
                var userExist = await userManager.FindByEmailAsync(user.Email);
                if (userExist == null)
                {
                    var result = await userManager.CreateAsync(user, model.Password);
                    result = await userManager.AddToRoleAsync(user, "organizationuser");
                    if (result.Succeeded)
                    {
                        var getNewUser = await userManager.FindByEmailAsync(model.EmailAddress);
                        if (getNewUser != null)
                        {
                            var newOrganizationUser = new OrganizationUsersInfo
                            {
                                UserId = getNewUser.Id,
                                EmailAddress = model.EmailAddress,
                                Firstname = model.Firstname,
                                Lastname = model.Lastname,
                                OrganisationId = model.OrganisationId,
                                PhoneNumber = model.PhoneNumber,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                IsActive = true,
                            };
                            dbContext.OrganizationUsersInfo.Add(newOrganizationUser);
                            dbContext.SaveChanges();

                            string confirmationLink = "http://recruitmentapi.ivslng.com/api/user/confirmEmail?email=" + model.EmailAddress;
                            //string confirmationLink = "https://localhost:44368/api/user/confirmEmail?email=" + model.email;
                            BodyBuilder bodyBuilder = new BodyBuilder();
                            MimeMessage message = new MimeMessage();
                            message.Subject = "Organization User Registration";
                            bodyBuilder.HtmlBody = "<h1>Recruitment Email Confirmation</h1><br/><h3>Click <a href=" + confirmationLink + ">here</a> to confirm your email</h3>";
                            mailer.mailing(model.Firstname, model.EmailAddress, bodyBuilder, message);
                            //BackgroundJob.Enqueue(()=> helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink));
                            //BackgroundJob.Schedule(() => helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink), TimeSpan.FromSeconds(30));
                            //RecurringJob.AddOrUpdate(() => helperClass.mailing(registrationModel.firstName, registrationModel.email, confirmationLink), Cron.Minutely);
                            responseModel.message = "User Created Successfully";
                            responseModel.code = 200;
                        }

                    }
                    else
                    {
                        responseModel.message = "User Creation Failed";
                        responseModel.code = 404;
                    }
                }
                else
                {
                    responseModel.message = "Email has been used";
                    responseModel.code = 400;
                }
                //}
                return responseModel;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.ErrorDate = DateTime.Now;
                errorLog.ErrorMessage = ex.Message;
                errorLog.ErrorSource = ex.Source;
                errorLog.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(errorLog);
                dbContext.SaveChanges();
                responseModel.message = ex.Message;
                responseModel.code = 404;
                return responseModel;
            }
        }
    }
}
