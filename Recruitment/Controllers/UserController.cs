using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Repository;
using Recruitment.RespondModels;
using Recruitment.ViewModel;
using Recruitment.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    public class userController : Controller
    {
        private readonly IUserRepository userRepository;

        public userController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> applicantRegistration([FromBody]ApplicantProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await userRepository.ApplicantRegistration(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> organizationRegistration([FromBody]OrganizationProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await userRepository.OrganizationRegistration(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> organizationUserRegistration([FromBody]OrganizationUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await userRepository.OrganizationUserRegistration(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            object responseModel = await userRepository.Login(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        [HttpPut("{email}", Name = "confirmEmail")]
        public async Task<IActionResult> confirmEmail(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel user = await userRepository.ConfirmEmail(email);
            //
            ViewBag.Message = user.message;
            ViewBag.Code = user.code;
            return View();
            //}
            //return NotFound("No Data Available!");
        }
    }
}
