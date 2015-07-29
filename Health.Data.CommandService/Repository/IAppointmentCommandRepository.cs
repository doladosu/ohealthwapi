using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.CommandService.Repository
{
    public interface IAppointmentCommandRepository : IRepository
    {
        Task CreatePatientAppointment(string userId, Appointment appointment);
        Task UpdatePatientAppointment(int id, Appointment appointment);
        Task DeletePatientAppointment(int id);
    }
}
