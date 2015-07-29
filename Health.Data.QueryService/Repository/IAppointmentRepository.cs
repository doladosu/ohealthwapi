using System.Collections.Generic;
using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.QueryService.Repository
{
    public interface IAppointmentRepository : IRepository
    {
        Task<IEnumerable<Appointment>> GetAllPatientAppointmentsTask(string userId);
        Task<Appointment> GetPatientAppointmentTask(int id);
        Task<Appointment> CreateAppointmentTask(Appointment appointment);
        Task UpdateAppointment(int id);
        Task CancelAppointmentTask(int id);
    }
}