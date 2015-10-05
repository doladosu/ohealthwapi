﻿using System.Linq;
using System.Threading.Tasks;
using Health.Data.CommandService.Repository;
using Health.Data.Core.Command.Appointment;
using Health.Setup.Core;

namespace Health.Data.Core.CommandHandler.Appointment
{
    public class CreatePatientAppointmentCommandHandler : ICommandHandler<CreatePatientAppointmentCommand>
    {
        private readonly IAppointmentCommandRepository _appointmentCommandRepository;

        public CreatePatientAppointmentCommandHandler(IAppointmentCommandRepository appointmentCommandRepository)
        {
            _appointmentCommandRepository = appointmentCommandRepository;
        }

        public async Task<CommandResult> Execute(CreatePatientAppointmentCommand command)
        {
            var commandResult = new CommandResult();
            var commandValidator = new CreatePatientAppointmentCommand.CreatePatientAppointmentCommandValidator();
            var result = commandValidator.Validate(command);
            if (result.IsValid)
            {
                await _appointmentCommandRepository.CreatePatientAppointment(command.Appointment);
                commandResult.Success = true;
                if (!commandResult.Success)
                {
                    commandResult.Message = "Error occured creating appointment!";
                }
            }
            else
            {
                commandResult.Success = false;
                var error = result.Errors.FirstOrDefault();
                commandResult.Message = error != null ? error.ErrorMessage : "Error occured creating appointment!";
            }
            return commandResult;
        }
    }
}