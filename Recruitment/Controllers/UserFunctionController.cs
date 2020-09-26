using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Models;
using Recruitment.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    public class UserFunctionController : Controller
    {
        private readonly IUserFunctionRepository functionRepository;

        public UserFunctionController(IUserFunctionRepository functionRepository)
        {
            this.functionRepository = functionRepository;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<UserFunction> responseModel = await functionRepository.GetAllFunction();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
