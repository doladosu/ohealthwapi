using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Appointment;
using Health.Data.QueryService.Repository;
using Health.Setup.Core;

namespace Health.Data.Core.QueryHandler.Appointment
{
    public class AppointmentQueryHandler : IQueryHandler<BaseByIdQuery, AppointmentQueryResult>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentQueryResult> Retrieve(BaseByIdQuery query)
        {
            var data = await _appointmentRepository.GetPatientAppointmentTask(query.Id);
            var result = new AppointmentQueryResult
            {
                Appointment = data
            };
            return result;
        }
    }
}