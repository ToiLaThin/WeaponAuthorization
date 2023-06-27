using IdentityJWT.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using IdentityJWT.Helpers;
using WeaponAuthorization.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
        private readonly IConfiguration _configuration;

        public HeroAuthenticationController(
            ILogger<HeroAuthenticationController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            HeroIdentityContext heroIdentityContext,
            IConfiguration configuration   
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _heroIdentityContext = heroIdentityContext;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(HeroDTO heroDTO) 
        {
            //if(AuthenticationHelper.VerifyUser(heroDTO.UserName, heroDTO.Password, _heroIdentityContext))
            if(_signInManager.PasswordSignInAsync(AuthenticationHelper.CreateNewUser(heroDTO), heroDTO.Password, false, false).IsCompleted)
            {
                _logger.Log(LogLevel.Information, "login successfully"); //phai la infomation
                //return Ok(HttpContext.User);
                return CreateToken(heroDTO);
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

        [HttpPost("Logout")]
        [Authorize(Roles = "admin")]
        private string CreateToken(HeroDTO heroDTO)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            if (claims.Count == 0) {
                claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, heroDTO.UserName),
                    new Claim(ClaimTypes.Role, "admin")
                };
            }

            var secretsConfig = _configuration.GetSection("Secrets").Get<SecretsConfiguration>(); //map configuration to a POCO class
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretsConfig.Token));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: credential
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
    }
}
