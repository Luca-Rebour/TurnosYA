using Application.DTOs.Notification;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Notifications;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Notifications
{
    public class CreateNotification: ICreateNotification
    {
        private INotificationRepository _repository;
        private IMapper _mapper;
        public CreateNotification(INotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<NotificationDTO> Create(NotificationDTO notificationDTO)
        {
            Notification notification = _mapper.Map<Notification>(notificationDTO);
            await _repository.AddAsync(notification);
            return _mapper.Map<NotificationDTO>(notification);

        }
    }
}
