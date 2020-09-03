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
    public class GradeController : Controller
    {
        private readonly IGradeRepository gradeRepository;

        public GradeController(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllGrade()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Grade> responseModel = await gradeRepository.GetAllGrade();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
