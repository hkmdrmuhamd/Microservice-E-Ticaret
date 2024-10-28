﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub); // Bu işlem Sub değeri üzerinden kullanıcının id'sini alır.
            var user = await _userManager.FindByIdAsync(userClaim.Value); // Kullanıcı id'si ile kullanıcı bilgileri çekilir.
            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Email = user.Email,
            });
        }
    }
}
