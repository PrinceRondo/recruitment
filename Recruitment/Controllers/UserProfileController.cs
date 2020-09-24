using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Models;
using Recruitment.Repository;
using Recruitment.RespondModels;
using Recruitment.ViewModel;
using Recruitment.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository profileRepository;

        public UserProfileController(IUserProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicantProfile(long id, [FromBody]UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await profileRepository.UpdateApplicantProfileAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizationProfile(long id, [FromBody]UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await profileRepository.UpdateOrganizationProfileAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizationUserProfile(long id, [FromBody]UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await profileRepository.UpdateOrganizationUserProfileAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        public async Task<IActionResult> GetAllApplicantProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<ApplicantProfile> responseModel = await profileRepository.GetAllApplicantProfile();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        public async Task<IActionResult> GetAllOrganizationProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<OrganizationProfile> responseModel = await profileRepository.GetAllOrganizationProfile();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{organizationId}")]
        public async Task<IActionResult> GetAllOrganizationUserProfile(long organizationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<OrganizationUsersInfo> responseModel = await profileRepository.GetAllOrganizationUserProfile(organizationId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicantProfileById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicantProfile responseModel = await profileRepository.GetApplicantProfileById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationProfileById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrganizationProfile responseModel = await profileRepository.GetOrganizationProfileById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationUserProfileById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrganizationUsersInfo responseModel = await profileRepository.GetOrganizationUserProfileById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
