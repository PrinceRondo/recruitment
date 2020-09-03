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
    public class QualificationController : Controller
    {
        private readonly IQualificationRepository qualificationRepository;

        public QualificationController(IQualificationRepository qualificationRepository)
        {
            this.qualificationRepository = qualificationRepository;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllQualification()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Qualification> responseModel = await qualificationRepository.GetAllQualification();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
