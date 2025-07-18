﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class AvailabilitySlot
    {
        public Guid Id { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public DateTime Date { get; set; } 
        public Guid ProfessionalId { get; private set; }
        public Professional Professional { get; private set; }
        public AvailabilityStatus SlotStatus { get; private set; }
        public AvailabilitySlot() 
        { 
            Id = Guid.NewGuid();
        }

        public AvailabilitySlot(DateTime date, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, AvailabilityStatus availabilityStatus)
        {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            SlotStatus = availabilityStatus;
            Date = date;
        }
        public void MarkAsUnavailable() => SlotStatus = AvailabilityStatus.Booked;

        public void Update(AvailabilitySlot other)
        {
            SlotStatus = other.SlotStatus;
        }

        public void SetProfessional(Professional professional)
        {
            Professional = professional;
        }


    }
}
