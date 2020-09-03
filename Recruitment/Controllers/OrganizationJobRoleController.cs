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
    public class OrganizationJobRoleController : Controller
    {
        private readonly IOrganizationJobRoleRepository roleRepository;

        public OrganizationJobRoleController(IOrganizationJobRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SaveRole([FromBody]OrganizationJobeRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await roleRepository.SaveAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody]OrganizationJobeRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await roleRepository.UpdateAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await roleRepository.DeleteAsync(id);
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
            IEnumerable<OrganizationJobeRoleViewModel> responseModel = await roleRepository.GetAll();
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
            IEnumerable<OrganizationJobeRoleViewModel> responseModel = await roleRepository.GetAllByOrgnizationId(id);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByRecruitmentLocationId(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<OrganizationJobeRoleViewModel> responseModel = await roleRepository.GetAllByRecruitmentLocationId(id);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByDepartmentId(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<OrganizationJobeRoleViewModel> responseModel = await roleRepository.GetAllByDepartmentId(id);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobRoleById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrganizationJobeRoleViewModel responseModel = await roleRepository.GetJobRoleById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
