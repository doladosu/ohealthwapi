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
            if (string.IsNullOrEmpty(userId))
            {
                var allPatients = await Db.Patients.ToListAsync();
                return Mapper.Map<IEnumerable<Patient>>(allPatients);
            }
            var patients = await Db.Patients.Where(e => e.UserId == userId).ToListAsync();
            return Mapper.Map<IEnumerable<Patient>>(patients);
        }

        public async Task<Patient> GetPatientProfileTask(int id)
        {
            var patient = await Db.Patients.FindAsync(id);
            return Mapper.Map<Patient>(patient);
        }
    }
}