using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Models;
using Recruitment.Repository;
using Recruitment.RespondModels;
using Recruitment.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    public class RecruitmentLocationController : Controller
    {
        private readonly IRecruitmentLocationRepository locationRepository;

        public RecruitmentLocationController(IRecruitmentLocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SaveRecruitmentLocation([FromBody]RecruitmentLocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await locationRepository.SaveAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecruitmentLocation(long id, [FromBody]RecruitmentLocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await locationRepository.UpdateAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruitmentLocation(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await locationRepository.DeleteAsync(id);
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
            IEnumerable<RecruitmentLocationViewModel> responseModel = await locationRepository.GetAll();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByOrganizationId(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<RecruitmentLocationViewModel> responseModel = await locationRepository.GetAllByOrgnizationId(id);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecruitmentLocationById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RecruitmentLocationViewModel responseModel = await locationRepository.GetRecruitmentLocationById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
