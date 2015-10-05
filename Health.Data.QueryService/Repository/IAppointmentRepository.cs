using System.Collections.Generic;
using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.QueryService.Repository
{
    public interface IAppointmentRepository : IRepository
    {
        Task<IEnumerable<Appointment>> GetAllPatientAppointmentsTask(string userId, int patientId);
        Task<Appointment> GetPatientAppointmentTask(int appointmentId);
    }
}