using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Patient;
using Health.Data.QueryService.Repository;
using Health.Setup.Core;

namespace Health.Data.Core.QueryHandler.Patient
{
    public class PatientProfilesQueryHandler : IQueryHandler<BaseByIdQuery, PatientProfilesQueryResult>
    {
        private readonly IPatientRepository _patientRepository;

        public PatientProfilesQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientProfilesQueryResult> Retrieve(BaseByIdQuery query)
        {
            var allData = await _patientRepository.GetAllPatientProfilesTask();
            var result = new PatientProfilesQueryResult
            {
                PatientProfiles = allData,
            };
            return result;
        }
    }
}