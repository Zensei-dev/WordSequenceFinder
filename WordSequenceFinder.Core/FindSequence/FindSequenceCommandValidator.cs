using FluentValidation;

namespace WordSequenceFinder.Core.FindSequence
{
    public class FindSequenceCommandValidator : AbstractValidator<FindSequenceCommand>
    {
        public FindSequenceCommandValidator()
        {
            RuleFor(cmd => cmd.DictionaryLocation)
                                .NotNull()
                                .MinimumLength(1);
            RuleFor(cmd => cmd.StartWord)
                                .NotNull()
                                .Length(4);
            RuleFor(cmd => cmd.EndWord)
                                .NotNull()
                                .Length(4);
            RuleFor(cmd => cmd.ResultLocation)
                                .NotNull()
                                .MinimumLength(1);
        }
    }
}
