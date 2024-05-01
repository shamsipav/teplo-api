using FluentValidation;

namespace TeploAPI.Models.Validators
{
    public class FurnaceValidator : AbstractValidator<Furnace.Furnace>
    {
        public FurnaceValidator()
        {
            RuleFor(x => x.NumberOfFurnace)
                .NotEmpty().WithMessage("'NumberOfFurnace' Номер доменной печи является обязательным")
                .GreaterThan(0).WithMessage("'NumberOfFurnace' Номер доменной печи не может быть отрицательным");

            RuleFor(x => x.UsefulVolumeOfFurnace)
                .NotEmpty().WithMessage("'UsefulVolumeOfFurnace' Полезный объем печи, м3 является обязательным")
                .NotNull().WithMessage("'UsefulVolumeOfFurnace' Полезный объем печи, м3 является обязательным")
                .GreaterThan(0).WithMessage("'UsefulVolumeOfFurnace' Полезный объем печи, м3 не может быть отрицательным");

            RuleFor(x => x.UsefulHeightOfFurnace)
                .NotEmpty().WithMessage("'UsefulHeightOfFurnace' Полезная высота печи, мм является обязательным")
                .NotNull().WithMessage("'UsefulHeightOfFurnace' Полезная высота печи, мм является обязательным")
                .GreaterThan(0).WithMessage("'UsefulHeightOfFurnace' Полезная высота печи, мм не может быть отрицательным");

            RuleFor(x => x.DiameterOfColoshnik)
                .NotEmpty().WithMessage("'DiameterOfColoshnik' Диаметр колошника, мм является обязательным")
                .NotNull().WithMessage("'DiameterOfColoshnik' Диаметр колошника, мм является обязательным")
                .GreaterThan(0).WithMessage("'DiameterOfColoshnik' Диаметр колошника, мм не может быть отрицательным");

            RuleFor(x => x.DiameterOfRaspar)
                .NotEmpty().WithMessage("'DiameterOfRaspar' Диаметр распара, мм является обязательным")
                .NotNull().WithMessage("'DiameterOfRaspar' Диаметр распара, мм является обязательным")
                .GreaterThan(0).WithMessage("'DiaDiameterOfRasparmeterOfColoshnik' Диаметр распара, мм не может быть отрицательным");

            RuleFor(x => x.DiameterOfHorn)
                .NotEmpty().WithMessage("'DiameterOfHorn' Диаметр горна, мм является обязательным")
                .NotNull().WithMessage("'DiameterOfHorn' Диаметр горна, мм является обязательным")
                .GreaterThan(0).WithMessage("'DiameterOfHorn' Диаметр горна, мм не может быть отрицательным");

            RuleFor(x => x.HeightOfHorn)
                .NotEmpty().WithMessage("'HeightOfHorn' Высота горна, мм является обязательным")
                .NotNull().WithMessage("'HeightOfHorn' Высота горна, мм является обязательным")
                .GreaterThan(0).WithMessage("'HeightOfHorn' Высота горна, мм не может быть отрицательным");

            RuleFor(x => x.HeightOfTuyeres)
                .NotEmpty().WithMessage("'HeightOfTuyeres' Высота фурм, мм является обязательным")
                .NotNull().WithMessage("'HeightOfTuyeres' Высота фурм, мм является обязательным")
                .GreaterThan(0).WithMessage("'HeightOfTuyeres' Высота фурм, мм не может быть отрицательным");

            RuleFor(x => x.HeightOfZaplechiks)
                .NotEmpty().WithMessage("'HeightOfZaplechiks' Высота заплечников, мм является обязательным")
                .NotNull().WithMessage("'HeightOfZaplechiks' Высота заплечников, мм является обязательным")
                .GreaterThan(0).WithMessage("'HeightOfZaplechiks' Высота заплечников, мм не может быть отрицательным");

            RuleFor(x => x.HeightOfRaspar)
                .NotEmpty().WithMessage("'HeightOfRaspar' Высота распара, мм является обязательным")
                .NotNull().WithMessage("'HeightOfRaspar' Высота распара, мм является обязательным")
                .GreaterThan(0).WithMessage("'HeightOfRaspar' Высота распара, мм не может быть отрицательным");

            RuleFor(x => x.HeightOfShaft)
                .NotEmpty().WithMessage("'HeightOfShaft' Высота шахты, мм является обязательным")
                .NotNull().WithMessage("'HeightOfShaft' Высота шахты, мм является обязательным")
                .GreaterThan(0).WithMessage("'HeightOfShaft' Высота шахты, мм не может быть отрицательным");

            RuleFor(x => x.HeightOfColoshnik)
                .NotEmpty().WithMessage("'HeightOfColoshnik' Высота колошника, мм является обязательным")
                .NotNull().WithMessage("'HeightOfColoshnik' Высота колошника, мм является обязательным")
                .GreaterThan(0).WithMessage("'HeightOfColoshnik' Высота колошника, мм не может быть отрицательным");
        }
    }
}
