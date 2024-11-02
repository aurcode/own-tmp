using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Autentication;
using Core.Entities.Notifications;
using Dto;
using Dto.Notifications;
using EntityFrameworkCore.Repository;
using EntityFrameworkCore.Repository.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<int, Notification> _repository;
        private readonly INotificationRepository _repositoryN;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public NotificationService(IRepository<int, Notification> repository, IMapper mapper,
            INotificationRepository repositoryN, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryN = repositoryN;
            _userManager = userManager;
        }

        public async Task AddNotifications(NotificationDto notification)
        {
            foreach (var userName in notification.UserId)
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user != null)
                {
                    var n = new Notification
                    {
                        Content = notification.Content,
                        Created = notification.Created,
                        NotificationTypeId = notification.NotificationTypeId,
                        UserId = user.Id,
                        Seen = notification.Seen,
                    };

                    var result = await _repository.AddAsync(n);
                }
            }
        }

        public async Task AddNotificationsToRole(NotificationRolesDto notification)
        {
            foreach (var roleName in notification.Roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(roleName);

                foreach (var user in users)
                {
                    if (user != null)
                    {
                        var n = new Notification
                        {
                            Content = notification.Content,
                            Created = notification.Created,
                            NotificationTypeId = notification.NotificationTypeId,
                            UserId = user.Id,
                            Seen = notification.Seen,
                        };

                        var result = await _repository.AddAsync(n);
                    }
                }


            }
        }

        public async Task AddNotificationsToAll(NotificationToAllDto notification)
        {
            var users = _userManager.Users;
            foreach (var user in users)
            {

                var n = new Notification
                {
                    Content = notification.Content,
                    Created = notification.Created,
                    NotificationTypeId = notification.NotificationTypeId,
                    UserId = user.Id,
                    Seen = notification.Seen,
                };

                var result = await _repository.AddAsync(n);

            }
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);

        }

        public async Task<List<NotificationDto>> Get()
        {
            var l = await _repository.GetAll().ToListAsync();
            List<NotificationDto> notifications = _mapper.Map<List<NotificationDto>>(l);
            return notifications;
        }

        public async Task<NotificationDto> Get(int id)
        {
            var notification = await _repository.GetAsync(id);
            NotificationDto n = _mapper.Map<NotificationDto>(notification);
            return n;
        }

        public async Task<List<NotificationDto>> GetByUserIdAsync(int userId, IdDto totalNotifications)
        {

            //totalNotifications.Id is the filtered notifications that wanna be shown by the user
            var promise = await _repositoryN.GetByUserIdAsync(userId, totalNotifications.Id);
            var values = _mapper.Map<List<NotificationDto>>(promise);

            return values;
        }

        public async Task Update(NotificationDto notification)
        {
            var n = _mapper.Map<Notification>(notification);
            await _repository.UpdateAsync(n);

        }
    }
}
