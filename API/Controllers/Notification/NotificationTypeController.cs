/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Notifications;
using Dto.Notifications;

namespace Api.Controllers.Notification
{
    [Route("v1/twittercopy/[controller]")]
    [ApiController]
    public class NotificationTypeController : ControllerBase
    {
        private readonly INotificationTypeService _service;

        public NotificationTypeController(INotificationTypeService service) 
        { 
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public async Task<List<NotificationTypeDto>> Get()
        {
            List<NotificationTypeDto> notificationTypes = await _service.Get();
            return notificationTypes;
        }

        [HttpGet("{id}")]
        public async Task<NotificationTypeDto> Get(int id)
        {
            NotificationTypeDto notificationType = await _service.Get(id);
            return notificationType;
        }

        [HttpPost]
        public async Task<int> Post([FromBody] NotificationTypeDto value)
        {
            var result = await _service.Add(value);

            return result;
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] NotificationTypeDto value)
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