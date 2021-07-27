using FluentValidation;
using FluentValidation.Validators;

namespace SMS.Application.SMSFeature.Command.SendSMS
{
    public class SendSMSCommandValidator : AbstractValidator<SendSMSCommand>
    {
        public SendSMSCommandValidator()
        {
            RuleFor(v => v.SmsModel.PhoneNumber).NotEmpty().MatchPhoneNumberRule().WithMessage("Please provide valid phone number");
            RuleFor(v => v.SmsModel.SMSText).NotEmpty().WithMessage("Please provide sms content");
        }

    }

    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> MatchPhoneNumberRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new RegularExpressionValidator<T>(@"^[0-9]"));
        }
    }

}
