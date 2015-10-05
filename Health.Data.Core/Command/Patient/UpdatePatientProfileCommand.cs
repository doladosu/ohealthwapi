using FluentValidation;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.Core.Command.Patient
{
    public class UpdatePatientProfileCommand : ICommand
    {
        public PatientProfile PatientProfile { get; set; }

        public class UpdatePatientProfileCommandValidator : AbstractValidator<UpdatePatientProfileCommand>
        {
            public UpdatePatientProfileCommandValidator()
            {
                RuleFor(x => x.PatientProfile).NotEmpty();
            }
        }
    }
}