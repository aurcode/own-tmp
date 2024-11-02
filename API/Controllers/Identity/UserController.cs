using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dto.Users;
using Core.Autentication;
using Api.Auth;
using Dto;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Dto.Catalog;
using AutoMapper;
using System.Data;
using ApplicationServices.Catalogs.Other;
//using ApplicationServices.Email;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Api.Controllers.Identity
{
    [Route("v1/twittercopy/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IOtherService _othersService;
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly RoleManager<Role> RoleManager;
        private readonly IJwtIssuerOptions JwtOptions;
        private readonly IMapper _mapper;
        //private EmailSender _sender;

        public UserController(
            IOtherService othersService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtIssuerOptions jwtOptions,
            RoleManager<Role> roleManager,
            IMapper mapper
            )
        {
            _othersService = othersService;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            JwtOptions = jwtOptions;
            _mapper = mapper;
        }

        ////[Authorize]
        [HttpPost(nameof(GetUsers))]
        public async Task<DtResult<User>> GetUsers(DtParameters input)
        {

            var searchBy = input.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (input.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = input.Columns[input.Order[0].Column].Data;
                orderAscendingDirection = input.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = (await UserManager.Users.ToListAsync()).AsQueryable();

            var totalResultsCount = result.Count();


            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.UserName != null && r.UserName.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.LastName != null && r.LastName.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.MiddleName != null && r.MiddleName.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()));
            }

            //result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            var filteredResultsCount = result.Count();


            return new DtResult<User>
            {
                Draw = input.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = result
                    .Skip(input.Start)
                    .Take(input.Length).ToList()
            };
        }

        [HttpGet(nameof(GetAllUsers))]
        public async Task<DtResult<User>> GetAllUsers()
        {
            var result = (await UserManager.Users.ToListAsync()).AsQueryable();

            var totalResultsCount = result.Count();

            return new DtResult<User>
            {
                RecordsTotal = totalResultsCount,
                Data = result.ToList()
            };
        }


        //[Authorize]
        [HttpPost(nameof(GetUser))]
        public async Task<CreateOrEditUserDto> GetUser(IdDto idDto)
        {
            var user = await UserManager.FindByIdAsync(idDto.Id.ToString());
            /*UserManager.Users.
            Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
            .AsNoTracking()
            .FirstOrDefault(u => u.Id == idDto.Id);

            */
            if (user == null) return null;

            var result = _mapper.Map<CreateOrEditUserDto>(user);

            var roles = await UserManager.GetRolesAsync(user);
            var res = RoleManager.Roles.Where(r => roles.AsEnumerable().Contains(r.Name)).Select(r => r.Id).ToList();

            result.RolId = res.First();

            return result;
        }

        //[Authorize]
        [HttpPost(nameof(CreateOrEditUser))]
        public async Task<User> CreateOrEditUser([FromBody] CreateOrEditUserDto userDto)
        {
            User usuarioInfo = null;

            if (userDto.Id == 0)
            {
                usuarioInfo = await CreateUser(userDto);
            }
            else
            {
                usuarioInfo = await EditUser(userDto);
            }

            return usuarioInfo;
        }

        private async Task<User> CreateUser(CreateOrEditUserDto userDto)
        {
            var user = new User()
            {
                Name = userDto.Name.ToUpper(),
                //AreaId = userDto.AreaId,
                //EmployeeId = userDto.EmployeeId,
                CreateDate = DateTime.UtcNow,
                LastName = userDto.LastName.ToUpper(),
                MiddleName = userDto.MiddleName.ToUpper(),
                UpdateDate = DateTime.UtcNow,
                UpdateUserId = userDto.UpdateUserId,
                StatusId = true,
                //UserType = userDto.UserType,
                UserName = userDto.Email.ToUpper(),
                Email = userDto.Email.ToUpper(),
                PhoneNumber = "",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            string tempPassword = GenerateRandomPassword();

            var u = await UserManager.CreateAsync(user, tempPassword);

            var role = await RoleManager.FindByIdAsync(userDto.RolId.ToString());

            await UserManager.AddToRoleAsync(user, role.Name);
            return user;
        }

        private async Task<User> EditUser(CreateOrEditUserDto userDto)
        {

            var user = await UserManager.FindByIdAsync(userDto.Id.ToString());

            if (user == null) return null;

            user.Name = userDto.Name.ToUpper();
            user.LastName = userDto.LastName.ToUpper();
            user.MiddleName = userDto.MiddleName.ToUpper();
            user.UpdateDate = DateTime.UtcNow;
            user.UserName = userDto.Email.ToUpper();
            user.Email = userDto.Email.ToUpper();
            user.StatusId = userDto.StatusId;

            //var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var userLog = await UserManager.FindByEmailAsync(currentUser);
            //user.UpdateUserId = userLog.Id;

            var result = await UserManager.UpdateAsync(user);
            return user;
        }

        [HttpGet("GetUserTypes")]
        public async Task<List<UserTypeDto>> GetUserType()
        {
            var result = await _othersService.getUserTypes();
            return result;
        }
        
        [HttpGet("GetRoles")]
        public async Task<List<Role>> GetRoles()
        {
            var result = await RoleManager.Roles.ToListAsync();
            return result;
        }

        //[Authorize]
        [HttpPost("ResetPassword")]
        public async Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordDto userPassword)
        {
            //var user = await UserManager.FindByIdAsync(userPassword.Id.ToString());
            var user = await UserManager.FindByEmailAsync(userPassword.Email);

            if (user == null) return new ResetPasswordResponseDto { Message = "User not found", Status =  "404" };

            await UserManager.ChangePasswordAsync(user, userPassword.OldPassword, userPassword.NewPassword);

            // Save when User was updated
            var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userLog = await UserManager.FindByEmailAsync(currentUser);
            user.UpdateUserId = userLog.Id;
            user.UpdateDate = DateTime.Now;
            await UserManager.UpdateAsync(user);


            return new ResetPasswordResponseDto { Message = "Password updated", Status =  "200" };
        }

        //[Authorize]
        [HttpPost("ChangeUserStatus")]
        public async Task<IActionResult> ChangeUserStatus(UserStatusDto userStatus)
        {
            var user = await UserManager.FindByIdAsync(userStatus.Id.ToString());

            if (user == null) return NotFound();

            user.StatusId = userStatus.Name;

            // Save when User was updated
            var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userLog = await UserManager.FindByEmailAsync(currentUser);
            user.UpdateUserId = userLog.Id;
            user.UpdateDate = DateTime.Now;

            // If user was deleted change date on DeleteDate column
            if (userStatus.Name == false)
            {
                user.DeleteDate = DateTime.Now;
            }

            await UserManager.UpdateAsync(user);

            return Ok();
        }


        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 7,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}