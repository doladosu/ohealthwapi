using System.Threading.Tasks;
using Health.Data.CommandService.Repository;
using Health.Data.Core.Command;
using Health.Data.Core.Command.Patient;
using Health.Setup.Core;

namespace Health.Data.Core.CommandHandler.Patient
{
    public class CreatePatientProfileCommandHandler : ICommandHandler<CreatePatientProfileCommand>
    {
        private readonly IPatientCommandRepository _patientCommandRepository;

        public CreatePatientProfileCommandHandler(IPatientCommandRepository patientCommandRepository)
        {
            _patientCommandRepository = patientCommandRepository;
        }

        public async Task<CommandResult> Execute(CreatePatientProfileCommand command)
        {
            var commandResult = new CommandResult();
            //Validate
            await _patientCommandRepository.CreatePatientProfile(command.UserId, command.PatientProfile);
            commandResult.Success = true;
            return commandResult;
        }
    }
}
