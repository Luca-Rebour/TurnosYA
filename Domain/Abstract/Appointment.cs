using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public abstract class Appointment
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public int DurationMinutes { get; private set; }
        public Status Status { get; private set; }
        public CancelledBy CanceledBy { get; private set; }

        public void Update(Appointment other)
        {
            if (other == null) return;

            if (other.Date != default)
                Date = other.Date;

            if (other.DurationMinutes > 0)
                DurationMinutes = other.DurationMinutes;

            if (Status != other.Status)
                Status = other.Status;
        }

        public void SetStatusCancelled()
        {
            Status = Status.Canceled;
        }

        public void SetStatusConfirmed()
        {
            Status = Status.Confirmed;
        }

    }
}
