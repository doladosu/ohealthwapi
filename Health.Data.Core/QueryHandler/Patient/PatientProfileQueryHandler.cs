using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Patient;
using Health.Data.QueryService.Repository;
using Health.Setup.Core;

namespace Health.Data.Core.QueryHandler.Patient
{
    public class PatientProfileQueryHandler : IQueryHandler<BaseByIdQuery, PatientProfileQueryResult>
    {
        private readonly IPatientRepository _patientRepository;

        public PatientProfileQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientProfileQueryResult> Retrieve(BaseByIdQuery query)
        {
            var allData = await _patientRepository.GetAllPatientProfilesTask();
            var result = new PatientProfileQueryResult
            {
                PatientProfiles = allData,
            };
            return result;
        }
    }
}