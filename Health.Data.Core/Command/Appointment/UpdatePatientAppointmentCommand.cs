﻿using FluentValidation;
using Health.Setup.Core;

namespace Health.Data.Core.Command.Appointment
{
    public class UpdatePatientAppointmentCommand : ICommand
    {
        public Health.Models.Output.Appointment Appointment { get; set; }

        public class UpdatePatientAppointmentCommandValidator : AbstractValidator<UpdatePatientAppointmentCommand>
        {
            public UpdatePatientAppointmentCommandValidator()
            {
                RuleFor(x => x.Appointment).NotEmpty();
            }
        }
    }
}