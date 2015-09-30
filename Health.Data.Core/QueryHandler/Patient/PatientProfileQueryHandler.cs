using System.Threading.Tasks;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Patient;
using Health.Data.QueryService.Repository;
using Health.Models.Output;
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
            var data = await _patientRepository.GetPatientProfileTask(query.Id);
            var result = new PatientProfileQueryResult
            {
                Patient = data
            };
            return result;
        }
    }
}