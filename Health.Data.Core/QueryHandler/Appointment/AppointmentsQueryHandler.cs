using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Appointment;
using Health.Data.QueryService.Repository;
using Health.Setup.Core;

namespace Health.Data.Core.QueryHandler.Appointment
{
    public class AppointmentsQueryHandler : IQueryHandler<BaseByIdQuery, AppointmentsQueryResult>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentsQueryResult> Retrieve(BaseByIdQuery query)
        {
            var allData = await _appointmentRepository.GetAllPatientAppointmentsTask(query.UserId);
            var result = new AppointmentsQueryResult
            {
                Appointments = allData,
            };
            return result;
        }
    }
}