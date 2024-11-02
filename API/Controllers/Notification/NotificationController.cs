/*using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Notifications;
using Dto.Notifications;
using Core.Entities.Notifications;
using Microsoft.AspNetCore.Authorization;
using Core.Autentication;
using Microsoft.AspNetCore.Identity;
using Dto;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.Notification
{
    [Route("v1/twittercopy/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        private readonly UserManager<User> _userManager;
        public NotificationController(INotificationService service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<List<NotificationDto>> Get()
        {
            List<NotificationDto> notifications = await _service.Get();
            return notifications;
        }
        [HttpGet("{id}")]
        public async Task<NotificationDto> Get(int id)
        {
            NotificationDto notification = await _service.Get(id);
            return notification;
        }




        [Authorize]
        [HttpPost(nameof(GetByUserId))]
        public async Task<List<NotificationDto>> GetByUserId([FromBody] IdDto id)
        {
            var iden = (System.Security.Claims.ClaimsIdentity)User.Identity;

            var username = iden.Claims.FirstOrDefault(x => x.Type == "name");

            var user = await _userManager.FindByNameAsync(username.Value);

            var notifications = await _service.GetByUserIdAsync(user.Id, id);

            return notifications;
        }

        [HttpPost]
        public async Task Post([FromBody] NotificationDto value)
        {
             await _service.AddNotifications(value);
        }

        [HttpPost(nameof(SendToRoles))]
        public async Task SendToRoles([FromBody] NotificationRolesDto value)
        {
            await _service.AddNotificationsToRole(value);
        }

        [HttpPost(nameof(SendToAll))]
        public async Task SendToAll([FromBody] NotificationToAllDto value)
        {
            await _service.AddNotificationsToAll(value);
        }


        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] NotificationDto value)
        {
            await _service.Update(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }
    }
}
*/