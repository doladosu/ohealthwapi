using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Health.Data.Core.Query;
using Health.Data.Core.QueryResult.Patient;
using Health.Data.Models;
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
                //PatientProfiles = Mapper.Map<IEnumerable<AspNetUser>>(allData),
            };
            return result;
        }
    }
}