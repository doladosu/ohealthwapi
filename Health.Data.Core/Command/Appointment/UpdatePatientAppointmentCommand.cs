using Health.Setup.Core;

namespace Health.Data.Core.Command.Appointment
{
    public class UpdatePatientAppointmentCommand : ICommand
    {
        public int Id { get; set; }
        public Health.Models.Output.Appointment Appointment { get; set; }
    }
}
