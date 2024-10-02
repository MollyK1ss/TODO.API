using FluentValidation;
using TODO.API.Controllers.ViewModels;

namespace TODO.API.Validators
{
    public class ToDoValidator : AbstractValidator<ToDoVm>
    {
        public ToDoValidator()
        {
            RuleFor(x => x.TaskName)
                .NotEmpty().WithMessage($"Поле {nameof(ToDoVm.TaskName)} обязательное!")
                .MaximumLength(10).WithMessage($"Поле {nameof(ToDoVm.TaskName)} не может быть больше 10");

        }
    }
}
