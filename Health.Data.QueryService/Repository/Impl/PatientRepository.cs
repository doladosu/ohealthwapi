using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Health.Models.Output;

namespace Health.Data.QueryService.Repository.Impl
{
    public class PatientRepository : BaseQueryRepository, IPatientRepository
    {
        public async Task<IEnumerable<Patient>> GetAllPatientsTask(string userId)
        {
            var allPatients = await Db.Patients.Where(e => e.UserId == userId).ToListAsync();
            return Mapper.Map<IEnumerable<Patient>>(allPatients);
        }

        public async Task<Patient> GetPatientProfileTask(int id)
        {
            var patient = await Db.Patients.FindAsync(id);
            return Mapper.Map<Patient>(patient);
        }
    }
}