using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Notifications;
using Dto.Notifications;
using EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Notifications
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly IRepository<int, NotificationType> _repository;
        private readonly IMapper _mapper;

        public NotificationTypeService(IRepository<int, NotificationType> repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Add(NotificationTypeDto notificationType)
        {
            var nt = _mapper.Map<NotificationType>(notificationType);
            await _repository.AddAsync(nt);
            return notificationType.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<NotificationTypeDto>> Get()
        {
            var l = await _repository.GetAll().ToListAsync();
            List<NotificationTypeDto> notificationTypes = _mapper.Map<List<NotificationTypeDto>>(l);
            return notificationTypes;
        }

        public async Task<NotificationTypeDto> Get(int id)
        {
            var notificationType = await _repository.GetAsync(id);
            NotificationTypeDto nt = _mapper.Map<NotificationTypeDto>(notificationType);
            return nt;
        }

        public async Task Update(NotificationTypeDto notificationType)
        {
            var nt = _mapper.Map<NotificationType>(notificationType);
            await _repository.UpdateAsync(nt);
        }
    }
}
