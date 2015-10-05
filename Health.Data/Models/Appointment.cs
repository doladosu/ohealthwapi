using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Data.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string UserId { get; set; }
        public int PatientId { get; set; }
        public bool IsCancelled { get; set; }
    }
}
