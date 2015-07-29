using System;
using System.Threading.Tasks;
using Health.Models.Output;

namespace Health.Data.CommandService.Repository.Impl
{
    public class AppointmentCommandRepository : BaseCommandRepository, IAppointmentCommandRepository
    {
        public Task CreatePatientAppointment(string userId, Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePatientAppointment(int id, Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task DeletePatientAppointment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
