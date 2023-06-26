using IdentityJWT.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using IdentityJWT.Helpers;
using WeaponAuthorization.Data;

namespace IdentityJWT.Controllers
{
    [ApiController]
    [Route("HeroAuthentication")]
    public class HeroAuthenticationController : ControllerBase
    {
        private readonly ILogger<HeroAuthenticationController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly HeroIdentityContext _heroIdentityContext;

        public HeroAuthenticationController(
            ILogger<HeroAuthenticationController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            HeroIdentityContext heroIdentityContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _heroIdentityContext = heroIdentityContext;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(HeroDTO heroDTO) 
        {
            //if(AuthenticationHelper.VerifyUser(heroDTO.UserName, heroDTO.Password, _heroIdentityContext))
            if(_signInManager.PasswordSignInAsync(AuthenticationHelper.CreateNewUser(heroDTO), heroDTO.Password, false, false).IsCompleted)
            {
                _logger.Log(LogLevel.Information, "login successfully"); //phai la infomation
                return Ok(HttpContext.User);
            }
            return NotFound("Not found account");
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(HeroDTO heroDTO)           
        {
            if (!heroDTO.ConfirmPassword.Equals(heroDTO.Password))
            {
                _logger.Log(LogLevel.Information, "password mismatch");
                return BadRequest("password mismatch");
            }
            await _userManager.CreateAsync(AuthenticationHelper.CreateNewUser(heroDTO), heroDTO.Password);
            return Ok(heroDTO);
        }
    }
}
