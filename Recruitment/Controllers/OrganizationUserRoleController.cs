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
    public class OrganizationUserRoleController : Controller
    {
        private readonly IOrganizationUserRoleRepository userRoleRepository;

        public OrganizationUserRoleController(IOrganizationUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SaveUserRole([FromBody]OrganizationUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await userRoleRepository.SaveAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUserRole([FromBody]RemoveUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await userRoleRepository.RemoveUserRoleAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        //[Route("[action]")]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateRole(int id, [FromBody]OrganizationUserRoleViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    ResponseModel responseModel = await userRoleRepository.UpdateAsync(id, model);
        //    if (responseModel != null)
        //    {
        //        return Ok(responseModel);
        //    }
        //    return BadRequest();
        //}
        [Route("[action]")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await userRoleRepository.DeleteAsync(id);
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
            IEnumerable<OrganizationUserRoleViewModel> responseModel = await userRoleRepository.GetAll();
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
            IEnumerable<OrganizationUserRoleViewModel> responseModel = await userRoleRepository.GetAllByOrgnizationId(id);
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
            IEnumerable<OrganizationUserRoleViewModel> responseModel = await userRoleRepository.GetAllByUserId(userId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRoleById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrganizationUserRoleViewModel responseModel = await userRoleRepository.GetUserRoleById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
