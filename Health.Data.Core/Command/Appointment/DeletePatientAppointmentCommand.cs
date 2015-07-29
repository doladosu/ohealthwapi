using Health.Setup.Core;

namespace Health.Data.Core.Command.Appointment
{
    public class DeletePatientAppointmentCommand : ICommand
    {
        public int Id { get; set; }
    }
}
