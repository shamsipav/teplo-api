using FluentValidation;

namespace TeploAPI.Models.Validators
{
    public class FurnaceValidator : AbstractValidator<FurnaceBase>
    {
        // TODO: Провалидировать все значения
        public FurnaceValidator()
        {
            RuleFor(x => x.NumberOfFurnace)
                .NotEmpty().WithMessage("'NumberOfFurnace' Номер доменной печи является обязательным")
                .GreaterThan(0).WithMessage("'NumberOfFurnace' Номер доменной печи не может быть отрицательным");

            //RuleFor(x => x.UsefulVolumeOfFurnace)
            //    .NotEmpty().WithMessage("'UsefulVolumeOfFurnace' Полезный объем печи, м3 является обязательным")
            //    .NotNull().WithMessage("'UsefulVolumeOfFurnace' Полезный объем печи, м3 является обязательным")
            //    .GreaterThan(0).WithMessage("'UsefulVolumeOfFurnace' Полезный объем печи, м3 не может быть отрицательным");

            //RuleFor(x => x.UsefulHeightOfFurnace)
            //    .NotEmpty().WithMessage("'UsefulHeightOfFurnace' Полезная высота печи, мм является обязательным")
            //    .NotNull().WithMessage("'UsefulHeightOfFurnace' Полезная высота печи, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'UsefulHeightOfFurnace' Полезная высота печи, мм не может быть отрицательным");

            //RuleFor(x => x.DiameterOfColoshnik)
            //    .NotEmpty().WithMessage("'DiameterOfColoshnik' Диаметр колошника, мм является обязательным")
            //    .NotNull().WithMessage("'DiameterOfColoshnik' Диаметр колошника, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'DiameterOfColoshnik' Диаметр колошника, мм не может быть отрицательным");

            //RuleFor(x => x.DiameterOfRaspar)
            //    .NotEmpty().WithMessage("'DiameterOfRaspar' Диаметр распара, мм является обязательным")
            //    .NotNull().WithMessage("'DiameterOfRaspar' Диаметр распара, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'DiaDiameterOfRasparmeterOfColoshnik' Диаметр распара, мм не может быть отрицательным");

            //RuleFor(x => x.DiameterOfHorn)
            //    .NotEmpty().WithMessage("'DiameterOfHorn' Диаметр горна, мм является обязательным")
            //    .NotNull().WithMessage("'DiameterOfHorn' Диаметр горна, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'DiameterOfHorn' Диаметр горна, мм не может быть отрицательным");

            //RuleFor(x => x.HeightOfHorn)
            //    .NotEmpty().WithMessage("'HeightOfHorn' Высота горна, мм является обязательным")
            //    .NotNull().WithMessage("'HeightOfHorn' Высота горна, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'HeightOfHorn' Высота горна, мм не может быть отрицательным");

            //RuleFor(x => x.HeightOfTuyeres)
            //    .NotEmpty().WithMessage("'HeightOfTuyeres' Высота фурм, мм является обязательным")
            //    .NotNull().WithMessage("'HeightOfTuyeres' Высота фурм, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'HeightOfTuyeres' Высота фурм, мм не может быть отрицательным");

            //RuleFor(x => x.HeightOfZaplechiks)
            //    .NotEmpty().WithMessage("'HeightOfZaplechiks' Высота заплечников, мм является обязательным")
            //    .NotNull().WithMessage("'HeightOfZaplechiks' Высота заплечников, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'HeightOfZaplechiks' Высота заплечников, мм не может быть отрицательным");

            //RuleFor(x => x.HeightOfRaspar)
            //    .NotEmpty().WithMessage("'HeightOfRaspar' Высота распара, мм является обязательным")
            //    .NotNull().WithMessage("'HeightOfRaspar' Высота распара, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'HeightOfRaspar' Высота распара, мм не может быть отрицательным");

            //RuleFor(x => x.HeightOfShaft)
            //    .NotEmpty().WithMessage("'HeightOfShaft' Высота шахты, мм является обязательным")
            //    .NotNull().WithMessage("'HeightOfShaft' Высота шахты, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'HeightOfShaft' Высота шахты, мм не может быть отрицательным");

            //RuleFor(x => x.HeightOfColoshnik)
            //    .NotEmpty().WithMessage("'HeightOfColoshnik' Высота колошника, мм является обязательным")
            //    .NotNull().WithMessage("'HeightOfColoshnik' Высота колошника, мм является обязательным")
            //    .GreaterThan(0).WithMessage("'HeightOfColoshnik' Высота колошника, мм не может быть отрицательным");

            RuleFor(x => x.EstablishedLevelOfEmbankment)
                .NotEmpty().WithMessage("'EstablishedLevelOfEmbankment' Установленный уровень насыпи, мм является обязательным")
                .NotNull().WithMessage("'EstablishedLevelOfEmbankment' Установленный уровень насыпи, мм является обязательным")
                .GreaterThan(0).WithMessage("'EstablishedLevelOfEmbankment' Установленный уровень насыпи, мм не может быть отрицательным");

            RuleFor(x => x.NumberOfTuyeres)
                .NotEmpty().WithMessage("'NumberOfTuyeres' Число фурм, шт является обязательным")
                .NotNull().WithMessage("'NumberOfTuyeres' Число фурм, шт является обязательным")
                .GreaterThan(0).WithMessage("'NumberOfTuyeres' Число фурм, шт не может быть отрицательным");

            RuleFor(x => x.DailyСapacityOfFurnace)
                .NotEmpty().WithMessage("'DailyСapacityOfFurnace' Суточная производительность печи, т чугуна/сутки является обязательным")
                .NotNull().WithMessage("'DailyСapacityOfFurnace' Суточная производительность печи, т чугуна/сутки является обязательным")
                .GreaterThan(0).WithMessage("'DailyСapacityOfFurnace' Суточная производительность печи, т чугуна/сутки не может быть отрицательным");

            RuleFor(x => x.SpecificConsumptionOfCoke)
                .NotEmpty().WithMessage("'SpecificConsumptionOfCoke' Удельный расход кокса, кг/т чугуна является обязательным")
                .NotNull().WithMessage("'SpecificConsumptionOfCoke' Удельный расход кокса, кг/т чугуна является обязательным")
                .GreaterThan(0).WithMessage("'SpecificConsumptionOfCoke' Удельный расход кокса, кг/т чугуна не может быть отрицательным");

            RuleFor(x => x.SpecificConsumptionOfZRM)
                .NotEmpty().WithMessage("'SpecificConsumptionOfZRM' Удельный расход ЖРМ, кг/т чугуна является обязательным")
                .NotNull().WithMessage("'SpecificConsumptionOfZRM' Удельный расход ЖРМ, кг/т чугуна является обязательным")
                .GreaterThan(0).WithMessage("'SpecificConsumptionOfZRM' Удельный расход ЖРМ, кг/т чугуна не может быть отрицательным");

            RuleFor(x => x.ShareOfPelletsInCharge)
                .NotEmpty().WithMessage("'ShareOfPelletsInCharge' Доля окатышей в шихте, доли ед. является обязательным")
                .NotNull().WithMessage("'ShareOfPelletsInCharge' Доля окатышей в шихте, доли ед. является обязательным")
                .GreaterThan(0).WithMessage("'ShareOfPelletsInCharge' Доля окатышей в шихте, доли ед. не может быть отрицательным");

            RuleFor(x => x.AcceptedTemperatureOfBackupZone)
                .NotEmpty().WithMessage("'AcceptedTemperatureOfBackupZone' Принятое значение температуры \"резервной зоны\", С является обязательным")
                .NotNull().WithMessage("'AcceptedTemperatureOfBackupZone' Принятое значение температуры \"резервной зоны\", С является обязательным")
                .GreaterThan(0).WithMessage("'AcceptedTemperatureOfBackupZone' Принятое значение температуры \"резервной зоны\", С не может быть отрицательным");
        }
    }
}
