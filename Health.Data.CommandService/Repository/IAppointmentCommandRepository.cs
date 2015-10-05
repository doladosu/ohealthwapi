using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.CommandService.Repository
{
    public interface IAppointmentCommandRepository : IRepository
    {
        Task CreatePatientAppointment(Appointment appointment);
        Task UpdatePatientAppointment(Appointment appointment);
    }
}
