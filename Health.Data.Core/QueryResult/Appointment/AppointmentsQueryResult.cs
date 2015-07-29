using System.Collections.Generic;

namespace Health.Data.Core.QueryResult.Appointment
{
    public class AppointmentsQueryResult : BaseQueryResult
    {
        public IEnumerable<Health.Models.Output.Appointment> Appointments { get; set; }
    }
}
