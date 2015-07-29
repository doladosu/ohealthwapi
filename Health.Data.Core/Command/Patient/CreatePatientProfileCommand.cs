using FluentValidation;
using Health.Models.Output;
using Health.Setup.Core;

namespace Health.Data.Core.Command.Patient
{
    public class CreatePatientProfileCommand : ICommand
    {
        public string UserId { get; set; }
        public PatientProfile PatientProfile { get; set; }

        public class CreatePatientProfileCommandValidator : AbstractValidator<CreatePatientProfileCommand>
        {
            public CreatePatientProfileCommandValidator()
            {
                RuleFor(x => x.UserId).NotEmpty();
                RuleFor(x => x.PatientProfile).NotEmpty();
            }
        }
    }
}
