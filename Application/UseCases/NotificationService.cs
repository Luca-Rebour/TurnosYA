using Application.DTOs.Notification;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class NotificationService : INotificationService
    {
        private INotificationRepository _repository;
        private IMapper _mapper;
        public NotificationService(INotificationRepository reposoitory, IMapper mapper)
        {
            _repository = reposoitory;
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
