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
    public class JobProfileDetailController : Controller
    {
        private readonly IJobProfileDetailRepository detailRepository;

        public JobProfileDetailController(IJobProfileDetailRepository detailRepository)
        {
            this.detailRepository = detailRepository;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]JobProfileDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await detailRepository.SaveAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]JobProfileDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await detailRepository.UpdateAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, [FromBody]DeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await detailRepository.DeleteAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<JobProfileDetailViewModel> responseModel = await detailRepository.GetAll();
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
            IEnumerable<JobProfileDetailViewModel> responseModel = await detailRepository.GetAllByUserId(userId);
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
            IEnumerable<JobProfileDetailViewModel> responseModel = await detailRepository.GetAllByOrgnizationId(organizationId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
        [Route("[action]")]
        [HttpGet("{jobProfileId}")]
        public async Task<IActionResult> GetAllByJobProfileId(int jobProfileId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<JobProfileDetailViewModel> responseModel = await detailRepository.GetAllByJobProfileId(jobProfileId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
        [Route("[action]")]
        [HttpGet("{elementId}")]
        public async Task<IActionResult> GetAllByElementId(int elementId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<JobProfileDetailViewModel> responseModel = await detailRepository.GetAllByOrgnizationId(elementId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobProfileDetailById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            JobProfileDetailViewModel responseModel = await detailRepository.GetJobProfileById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
