using FluentValidation.TestHelper;
using WordSequenceFinder.Core.FindSequence;
using Xunit;

namespace WordSequenceFinder.Test.Unit.FindSequence
{
    public class FindSequenceCommandTests
    {
        private FindSequenceCommandValidator _validator;

        public FindSequenceCommandTests()
        {
            _validator = new FindSequenceCommandValidator();
        }

        #region DictionaryLocation
        [Fact]
        public void GivenDictionaryLocation_WhenValidateCommand_ThenValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.DictionaryLocation, "Some/Potential/Location");
        }

        [Fact]
        public void GivenMinLengthDictionaryLocation_WhenValidateCommand_ThenValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.DictionaryLocation, "1");
        }

        [Fact]
        public void GivenNullDictionaryLocation_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.DictionaryLocation, null as string);
        }

        [Fact]
        public void GivenEmptyDictionaryLocation_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.DictionaryLocation, "");
        }
        #endregion

        #region StartWord
        [Fact]
        public void GivenCorrectLengthStartWord_WhenValidateCommand_ThenValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.StartWord, "Test");
        }

        [Fact]
        public void GivenNullStartWord_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.StartWord, null as string);
        }

        [Fact]
        public void GivenIncorrectLengthStartWord_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.StartWord, "");
            _validator.ShouldHaveValidationErrorFor(command => command.StartWord, "_3_");
            _validator.ShouldHaveValidationErrorFor(command => command.StartWord, "2long");
            _validator.ShouldHaveValidationErrorFor(command => command.StartWord, "waaaaaaaaaaaaaaaaaaaaaaay Too Long");
        }
        #endregion

        #region EndWord
        [Fact]
        public void GivenCorrectLengthEndWord_WhenValidateCommand_ThenValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.EndWord, "Test");
        }

        [Fact]
        public void GivenNullEndWord_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.EndWord, null as string);
        }

        [Fact]
        public void GivenIncorrectLengthEndWord_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.EndWord, "");
            _validator.ShouldHaveValidationErrorFor(command => command.EndWord, "_3_");
            _validator.ShouldHaveValidationErrorFor(command => command.EndWord, "2long");
            _validator.ShouldHaveValidationErrorFor(command => command.EndWord, "waaaaaaaaaaaaaaaaaaaaaaay Too Long");
        }
        #endregion

        #region ResultLocation
        [Fact]
        public void GivenResultLocation_WhenValidateCommand_ThenValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.ResultLocation, "Some/Potential/Location");
        }

        [Fact]
        public void GivenMinLengthResultLocation_WhenValidateCommand_ThenValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.ResultLocation, "1");
        }

        [Fact]
        public void GivenNullResultLocation_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.ResultLocation, null as string);
        }

        [Fact]
        public void GivenEmptyResultLocation_WhenValidateCommand_ThenValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.ResultLocation, "");
        }
        #endregion
    }
}
