using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Patient;
using Health.Data.QueryService.Repository;
using Health.Setup.Core;

namespace Health.Data.Core.QueryHandler.Appointment
{
    public class AppointmentQueryHandler : IQueryHandler<BaseByIdQuery, PatientProfileQueryResult>
    {
        private readonly IPatientRepository _patientRepository;

        public AppointmentQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientProfileQueryResult> Retrieve(BaseByIdQuery query)
        {
            var data = await _patientRepository.GetPatientProfileTask(query.Id);
            var result = new PatientProfileQueryResult
            {
                PatientProfile = data
            };
            return result;
        }
    }
}