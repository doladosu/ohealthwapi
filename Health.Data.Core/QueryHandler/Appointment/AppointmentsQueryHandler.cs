using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Patient;
using Health.Data.QueryService.Repository;
using Health.Setup.Core;

namespace Health.Data.Core.QueryHandler.Appointment
{
    public class AppointmentsQueryHandler : IQueryHandler<BaseByIdQuery, PatientProfilesQueryResult>
    {
        private readonly IPatientRepository _patientRepository;

        public AppointmentsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientProfilesQueryResult> Retrieve(BaseByIdQuery query)
        {
            var allData = await _patientRepository.GetAllPatientsTask(query.UserId);
            var result = new PatientProfilesQueryResult
            {
                Patients = allData,
            };
            return result;
        }
    }
}