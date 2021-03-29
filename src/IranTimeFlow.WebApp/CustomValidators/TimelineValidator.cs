using FluentValidation;
using IranTimeFlow.WebApp.Helpers;
using IranTimeFlow.WebApp.ViewModels;

namespace IranTimeFlow.WebApp.CustomValidators
{
    public class TimelineValidator : AbstractValidator<TimelineAddViewModel>
    {
        public TimelineValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithName("عنوان")
                .WithMessage(Consts.Required)
                .NotNull()
                .WithName("عنوان")
                .WithMessage(Consts.Required)
                .Length(8, 128)
                .WithName("عنوان")
                .WithMessage(Consts.LengthRange);

            RuleFor(a => a.Content)
                .NotEmpty()
                .WithName("متن و محتوای خبر")
                .WithMessage(Consts.Required)
                .NotNull()
                .WithName("متن و محتوای خبر")
                .WithMessage(Consts.Required)
                .Length(32, 4096)
                .WithName("متن و محتوای خبر")
                .WithMessage(Consts.LengthRange);

            RuleFor(a => a.Tags)
                .NotEmpty()
                .WithName("برچسب‌ها")
                .WithMessage(Consts.Required)
                .NotNull()
                .WithName("برچسب‌ها")
                .WithMessage(Consts.Required)
                .Length(4, 1024)
                .WithName("برچسب‌ها")
                .WithMessage(Consts.LengthRange);

            RuleFor(a => a.Resources)
                .NotEmpty()
                .WithName("لینک منابع")
                .WithMessage(Consts.Required)
                .NotNull()
                .WithName("لینک منابع")
                .WithMessage(Consts.Required)
                .Length(8, 4096)
                .WithName("لینک منابع")
                .WithMessage(Consts.LengthRange);
        }
    }
}
