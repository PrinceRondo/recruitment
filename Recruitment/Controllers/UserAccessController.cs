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
    public class UserAccessController : Controller
    {
        private readonly IUserRoleAccessRepository accessRepository;

        public UserAccessController(IUserRoleAccessRepository accessRepository)
        {
            this.accessRepository = accessRepository;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SaveAccess([FromBody]RoleFuctionAccessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await accessRepository.SaveAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }

        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccess(int id, [FromBody]RoleFuctionAccessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await accessRepository.UpdateAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccess(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await accessRepository.DeleteAsync(id);
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
            IEnumerable<RoleFuctionAccessViewModel> responseModel = await accessRepository.GetAll();
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
            IEnumerable<RoleFuctionAccessViewModel> responseModel = await accessRepository.GetAllByOrgnizationId(id);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetAllByRoleId(int roleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<RoleFuctionAccessViewModel> responseModel = await accessRepository.GetAllByRoleId(roleId);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RoleFuctionAccessViewModel responseModel = await accessRepository.GetUserRoleFuntionAccessById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
