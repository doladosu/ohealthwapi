using System.Threading.Tasks;
using Health.Data.CommandService.Repository;
using Health.Data.Core.Command;
using Health.Data.Core.Command.Appointment;
using Health.Setup.Core;

namespace Health.Data.Core.CommandHandler.Appointment
{
    public class UpdatePatientAppointmentCommandHandler : ICommandHandler<UpdatePatientAppointmentCommand>
    {
        private readonly IPatientCommandRepository _patientCommandRepository;

        public UpdatePatientAppointmentCommandHandler(IPatientCommandRepository patientCommandRepository)
        {
            _patientCommandRepository = patientCommandRepository;
        }

        public async Task<CommandResult> Execute(UpdatePatientAppointmentCommand command)
        {
            var commandResult = new CommandResult();
            //Validate
            //await _patientCommandRepository.(command.UserId, command.PatientProfile);
            commandResult.Success = true;
            return commandResult;
        }
    }
}
