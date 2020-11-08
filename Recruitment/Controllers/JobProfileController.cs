using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Repository;
using Recruitment.RespondModels;
using Recruitment.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    public class JobProfileController : Controller
    {
        private readonly IJobProfileRepository profileRepository;

        public JobProfileController(IJobProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SaveJobProfile([FromBody]JobProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await profileRepository.SaveAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobProfile(long id, [FromBody]JobProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await profileRepository.UpdateAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobProfile(long id, [FromBody]DeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await profileRepository.DeleteAsync(id,model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllJobProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<JobProfileViewModel> responseModel = await profileRepository.GetAll();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
        [Route("[action]")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<JobProfileViewModel> responseModel = await profileRepository.GetAllByUserId(userId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
        [Route("[action]")]
        [HttpGet("{organizationId}")]
        public async Task<IActionResult> GetAllByOrganizationId(int organizationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<JobProfileViewModel> responseModel = await profileRepository.GetAllByOrgnizationId(organizationId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobProfileById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            JobProfileViewModel responseModel = await profileRepository.GetJobProfileById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
