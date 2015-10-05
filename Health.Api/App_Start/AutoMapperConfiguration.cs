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
            Mapper.CreateMap<Patient, Models.Output.Patient>()
                .ForMember(dest => dest.ProxyId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.Race, opt => opt.MapFrom(src => src.RaceId));
            Mapper.CreateMap<State, Models.Output.State>();
            Mapper.CreateMap<Models.Output.Patient, Patient>();
            Mapper.CreateMap<Models.Output.Appointment, Appointment>();
            Mapper.CreateMap<Appointment, Models.Output.Appointment>();
        }
    }
}