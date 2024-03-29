﻿using System;
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
    public class LevelController : Controller
    {
        private readonly ILevelRepository levelRepository;

        public LevelController(ILevelRepository levelRepository)
        {
            this.levelRepository = levelRepository;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllLevel()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<ApplicantLevel> responseModel = await levelRepository.GetAllLevel();
            if (responseModel.Count() > 0)
            {
                return Ok(responseModel);
            }
            return Ok("No Data Available");
        }
    }
}
