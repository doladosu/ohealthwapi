using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Health.Models.Output;

namespace Health.Data.QueryService.Repository.Impl
{
    public class AppointmentRepository : BaseQueryRepository, IAppointmentRepository
    {
        public Task<IEnumerable<Appointment>> GetAllPatientAppointmentsTask(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Appointment> GetPatientAppointmentTask(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Appointment> CreateAppointmentTask(Appointment appointment)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAppointment(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task CancelAppointmentTask(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}