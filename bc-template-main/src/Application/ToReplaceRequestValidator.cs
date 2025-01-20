using FluentValidation;

namespace NGrid.Customer.ToReplace.Application;

public class ToReplaceRequestValidator : AbstractValidator<Domain.ToReplace>
{
    public ToReplaceRequestValidator()
    {
        RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required and can not be empty");
        RuleFor(r => r.Key).GreaterThan(0).WithMessage("Key is required and can not be empty");
    }
}