﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorResources.Contracts;

namespace YotorResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IVerifyRepository _verifyRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public UsersController(IVerifyRepository verifyRepository)
        {
            _verifyRepository = verifyRepository;
        }
        [HttpGet("Info")]
        public async Task<IActionResult> GetCustomerByIdAsync()
        {
            try
            {
                return Ok(await _verifyRepository.GetCustomerByIdAsync(UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("IsAdmin")]
        public async Task<IActionResult> IsAdminAsync()
        {
            try
            {
                return Ok(await _verifyRepository.IsAdminAsync(UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
