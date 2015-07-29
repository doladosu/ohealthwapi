using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.CommandService.Repository
{
    public interface IPhysicianCommandRepository : IRepository
    {
        Task CreatePatientProfile(string userId, PatientProfile patientProfile);
    }
}
