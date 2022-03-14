using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class StreamerCommandValidator : AbstractValidator<StreamerCommand>
    {
        public StreamerCommandValidator()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
                    .NotNull().WithMessage("{Nombre} no puede ser nulo")
                    .MaximumLength(50).WithMessage("{Nombre} no puede ecceder los 50 caracteres");
            RuleFor(p => p.url)
                    .NotEmpty().WithMessage("{Url} no puede estar en blanco");
        }
    }
}