using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Health.Data.Models;
using Health.Models.Output;
using Address = Health.Data.Models.Address;
using Patient = Health.Data.Models.Patient;

namespace Health.Data.CommandService.Repository.Impl
{
    public class PhysicianCommandRepository : BaseCommandRepository, IPhysicianCommandRepository
    {
        public  async Task CreatePatientProfile(string userId, PatientProfile patientProfile)
        {
            var patientData = patientProfile.Patient;
            patientData.ProxyId = userId;
            var patient = Mapper.Map<Patient>(patientData);
            Db.Patients.Add(patient);

            if (patientProfile.Addresses.Any())
            {
                foreach (var addressData in patientProfile.Addresses)
                {
                    var address = Mapper.Map<Address>(addressData);
                    Db.Addresses.Add(address);
                }
            }

            if (patientProfile.Insurance != null)
            {
                var insuranceData = patientProfile.Insurance;
                // TODO
            }
            await Db.SaveChangesAsync();
        }
    }
}
