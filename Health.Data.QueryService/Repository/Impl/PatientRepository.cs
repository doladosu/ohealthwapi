using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Health.Models.Output;

namespace Health.Data.QueryService.Repository.Impl
{
    public class PatientRepository : BaseQueryRepository, IPatientRepository
    {
        public async Task<IEnumerable<PatientProfile>> GetAllPatientProfilesTask()
        {
            var allPatients = await Db.Patients.ToListAsync();
            return Mapper.Map<IEnumerable<PatientProfile>>(allPatients);
        }

        public async Task<PatientProfile> GetPatientProfileTask(int id)
        {
            var patient = await Db.Patients.FindAsync(id);
            return Mapper.Map<PatientProfile>(patient);
        }
    }
}