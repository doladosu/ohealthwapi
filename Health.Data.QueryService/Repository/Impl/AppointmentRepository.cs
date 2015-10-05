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
        public async Task<IEnumerable<Appointment>> GetAllPatientAppointmentsTask(string userId, int patientId)
        {
            if (patientId == 0)
            {
                var allData = await Db.Appointments.ToListAsync();
                return Mapper.Map<IEnumerable<Appointment>>(allData);
            }
            var data = await Db.Appointments.Where(e => e.PatientId == patientId && e.UserId == userId).ToListAsync();
            return Mapper.Map<IEnumerable<Appointment>>(data);
        }

        public async Task<Appointment> GetPatientAppointmentTask(int appointmentId)
        {
            var data = await Db.Appointments.Where(e => e.AppointmentId == appointmentId).ToListAsync();
            return Mapper.Map<Appointment>(data);
        }
    }
}