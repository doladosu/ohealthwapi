using System.Threading.Tasks;
using AutoMapper;
using Health.Models.Output;

namespace Health.Data.CommandService.Repository.Impl
{
    public class AppointmentCommandRepository : BaseCommandRepository, IAppointmentCommandRepository
    {
        public async Task CreatePatientAppointment(Appointment appointment)
        {
            var data = Mapper.Map<Models.Appointment>(appointment);
            Db.Appointments.Add(data);
            await Db.SaveChangesAsync();
        }

        public async Task UpdatePatientAppointment(Appointment appointment)
        {
            var data = Db.Appointments.Find(appointment.AppointmentId);
            if (data != null)
            {
                data.IsCancelled = appointment.IsCancelled;
            }
            await Db.SaveChangesAsync();
        }
    }
}