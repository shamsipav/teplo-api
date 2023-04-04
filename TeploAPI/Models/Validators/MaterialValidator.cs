using FluentValidation;

namespace TeploAPI.Models.Validators
{
    public class MaterialValidator : AbstractValidator<Material>
    {
        public MaterialValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("'Name' Название материала является обязательным")
                .NotNull().WithMessage("'Name' Название материала является обязательным");

            RuleFor(x => x.Moisture)
                .NotEmpty().WithMessage("'Moisture' Содержание влаги, % является обязательным")
                .GreaterThan(0).WithMessage("'Moisture' Содержание влаги, % должно быть больше 0");

            RuleFor(x => x.Fe2O3)
                .NotEmpty().WithMessage("'Fe2O3' Содержание Fe2O3, % является обязательным")
                .GreaterThan(0).WithMessage("'Fe2O3' Содержание Fe2O3, % должно быть больше 0");
            RuleFor(x => x.Fe)
                .NotEmpty().WithMessage("'Fe' Содержание Fe, % является обязательным")
                .GreaterThan(0).WithMessage("'Fe' Содержание Fe, % должно быть больше 0");

            RuleFor(x => x.FeO)
                .NotEmpty().WithMessage("'FeO' Содержание FeO, % является обязательным")
                .GreaterThan(0).WithMessage("'FeO' Содержание FeO, %  должно быть больше 0");

            RuleFor(x => x.CaO)
                .NotEmpty().WithMessage("'CaO' Содержание CaO, % является обязательным")
                .GreaterThan(0).WithMessage("'CaO' Содержание CaO, % должно быть больше 0");

            RuleFor(x => x.SiO2)
                .NotEmpty().WithMessage("'SiO2' Содержание SiO2, % является обязательным")
                .GreaterThan(0).WithMessage("'SiO2' Содержание SiO2, % должно быть больше 0");

            RuleFor(x => x.MgO)
                .NotEmpty().WithMessage("'MgO' Содержание MgO, % является обязательным")
                .GreaterThan(0).WithMessage("'MgO' Содержание MgO, % должно быть больше 0");

            RuleFor(x => x.Al2O3)
                .NotEmpty().WithMessage("'Al2O3' Содержание Al2O3, % является обязательным")
                .GreaterThan(0).WithMessage("'Al2O3' Содержание Al2O3, % должно быть больше 0");

            RuleFor(x => x.TiO2)
                .NotEmpty().WithMessage("'TiO2' Содержание TiO2, % является обязательным")
                .GreaterThan(0).WithMessage("'TiO2' Содержание TiO2, % должно быть больше 0");

            RuleFor(x => x.MnO)
                .NotEmpty().WithMessage("'MnO' Содержание MnO, % является обязательным")
                .GreaterThan(0).WithMessage("'MnO' Содержание MnO, % должно быть больше 0");

            RuleFor(x => x.P)
                .NotEmpty().WithMessage("'P' Содержание P, % является обязательным")
                .GreaterThan(0).WithMessage("'P' Содержание P, % должно быть больше 0");

            RuleFor(x => x.S)
                .NotEmpty().WithMessage("'S' Содержание S, % является обязательным")
                .GreaterThan(0).WithMessage("'S' Содержание S, % должно быть больше 0");

            RuleFor(x => x.Zn)
                .NotEmpty().WithMessage("'Zn' Содержание Zn, % является обязательным")
                .GreaterThan(0).WithMessage("'Zn' Содержание Zn, % должно быть больше 0");

            RuleFor(x => x.Mn)
                .NotEmpty().WithMessage("'Mn' Содержание Mn, % является обязательным")
                .GreaterThan(0).WithMessage("'Mn' Содержание Mn, % должно быть больше 0");

            RuleFor(x => x.Cr)
                .NotEmpty().WithMessage("'Cr' Содержание Cr, % является обязательным")
                .GreaterThan(0).WithMessage("'Cr' Содержание Cr, % должно быть больше 0");

            RuleFor(x => x.FiveZero)
                .NotEmpty().WithMessage("'FiveZero' Содержание 5-0мм, % является обязательным")
                .GreaterThan(0).WithMessage("'FiveZero' Содержание 5-0мм, % должно быть больше 0");

            //RuleFor(x => x.BaseOne)
            //    .NotEmpty().WithMessage("'BaseOne' Содержание Осн1, % является обязательным")
            //    .GreaterThan(0).WithMessage("'BaseOne' Содержание Осн1, % должно быть больше 0");
        }
    }
}
