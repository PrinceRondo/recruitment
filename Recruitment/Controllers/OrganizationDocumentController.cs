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
    public class OrganizationDocumentController : Controller
    {
        private readonly IOrganizationDocumentRepository documentRepository;

        public OrganizationDocumentController(IOrganizationDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SaveDocument([FromBody]OrganizationDocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await documentRepository.SaveAsync(model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody]OrganizationDocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await documentRepository.UpdateAsync(id, model);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }
        [Route("[action]")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseModel responseModel = await documentRepository.DeleteAsync(id);
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
            IEnumerable<OrganizationDocumentViewModel> responseModel = await documentRepository.GetAll();
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
            IEnumerable<OrganizationDocumentViewModel> responseModel = await documentRepository.GetAllByOrgnizationId(id);
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }

        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrganizationDocumentViewModel responseModel = await documentRepository.GetDocumentById(id);
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
