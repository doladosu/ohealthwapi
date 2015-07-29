using System.Collections.Generic;

namespace Health.Data.Core.QueryResult.Patient
{
    public class PatientProfilesQueryResult : BaseQueryResult
    {
        public IEnumerable<Health.Models.Output.Patient> Patients { get; set; }
    }
}
