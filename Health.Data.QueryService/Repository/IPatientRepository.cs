using System.Collections.Generic;
using System.Threading.Tasks;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.QueryService.Repository
{
    public interface IPatientRepository : IRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsTask(string userId);
        Task<Patient> GetPatientProfileTask(int id);
    }
}