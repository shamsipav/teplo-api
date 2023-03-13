using FluentValidation;

namespace TeploAPI.Models.Validators
{
    public class FurnaceValidator : AbstractValidator<Furnace>
    {
        // TODO: Провалидировать все значения
        public FurnaceValidator()
        {
            RuleFor(x => x.AcceptedTemperatureOfBackupZone)
                .NotEmpty().WithMessage("'AcceptedTemperatureOfBackupZone' Принятое значение температуры \"резервной зоны\", С является обязательным")
                .NotNull().WithMessage("'AcceptedTemperatureOfBackupZone' Принятое значение температуры \"резервной зоны\", С является обязательным")
                .GreaterThan(0).WithMessage("'AcceptedTemperatureOfBackupZone' Принятое значение температуры \"резервной зоны\", С не может быть отрицательным");
        }
    }
}
