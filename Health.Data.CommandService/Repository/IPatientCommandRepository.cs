using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.CommandService.Repository
{
    public interface IPatientCommandRepository : IRepository
    {
        Task CreatePatientProfile(string userId, PatientProfile patientProfile);
    }
}
