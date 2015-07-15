using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Health.Models.Output;

namespace Health.Data.QueryService.Repository.Impl
{
    public class PatientRepository : BaseQueryRepository, IPatientRepository
    {
        public Task<IEnumerable<PatientProfile>> GetAllPatientProfilesTask()
        {
            throw new NotImplementedException();
        }
    }
}