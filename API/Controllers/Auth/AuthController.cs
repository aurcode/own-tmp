using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Api.Auth;
using Core.Autentication;
using Core.Config;
using Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers.Auth
{
    [Route("v1/twittercopy/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtTokenValidationSettings _jwtSettings;
        private readonly IJwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<User> userManager,
          SignInManager<User> signInManager,
          IJwtIssuerOptions jwtOptions,
          RoleManager<Role> roleManager,
          IOptions<JwtTokenValidationSettings> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = jwtConfig.Value;
            _jwtOptions = jwtOptions;
        }
        #endregion


        #region Login/Logout
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            var resuslt = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (user == null || !(await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                return Unauthorized();

            var tokenString = await CreateJwtTokenAsync(user);

            var result = new ContentResult() { Content = tokenString, ContentType = "application/text" };

            return result;

        }
           
        //[HttpPost]
        //public async Task<IActionResult> Logout(string returnUrl = "")
        //{
        //    await _signInManager.SignOutAsync();
            
        //    return Redirect(returnUrl);
        //}
        #endregion



        #region Jwt
        private async Task<string> CreateJwtTokenAsync(User user)
        {


            // Create JWT claims
            var claims = new List<Claim>(new[]
            {
                
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),   

                // UserName
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),       

                // Email is unique
                new Claim(JwtRegisteredClaimNames.Email, user.Email),        

                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()), 

                // Issued at
                // this hv error, fix in the future
                //new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64)
              });

            // Add userclaims from storage
            claims.AddRange(await _userManager.GetClaimsAsync(user));

            // Add user role, they are converted to claims
            var roleNames = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                // Find IdentityRole by name
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    // Convert Identity to claim and add 
                    var roleClaim = new Claim(ClaimTypes.Role, role.Name, ClaimValueTypes.String, _jwtOptions.Issuer);
                    claims.Add(roleClaim);

                    // Add claims belonging to the role
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Email));

            // Prepare Jwt Token
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expires,
                signingCredentials: _jwtOptions.SigningCredentials);

            // Serialize token
            var result = new JwtSecurityTokenHandler().WriteToken(jwt);

            return result;
        }
        #endregion
    }
}
