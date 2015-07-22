using AutoMapper;
using Health.Data.Models;

namespace Health
{
    /// <summary>
    /// Auto mapper config
    /// </summary>
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Auto mapper setup
        /// </summary>
        public static void Setup()
        {
            Mapper.CreateMap<Patient, Models.Output.PatientProfile>();
            Mapper.CreateMap<State, Models.Output.State>();
        }
    }
}