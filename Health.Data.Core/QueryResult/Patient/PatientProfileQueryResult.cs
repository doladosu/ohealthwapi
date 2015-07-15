using System.Collections.Generic;
using Health.Models.Output;

namespace Health.Data.Core.QueryResult.Patient
{
    public class PatientProfileQueryResult : BaseQueryResult
    {
        public IEnumerable<PatientProfile> PatientProfiles { get; set; }
    }
}
