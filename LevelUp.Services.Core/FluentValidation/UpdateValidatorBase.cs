using FluentValidation;

namespace LevelUp.Services.Core.FluentValidation;

public abstract class UpdateValidatorBase<TUpdateModel, TEntityId> : AbstractValidator<TUpdateModel>
{
    public TEntityId Id { get; set; }
}