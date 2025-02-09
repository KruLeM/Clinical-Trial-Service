
using API.Validation.RequestModels;
using API.Validation.Validators;
using FluentValidation.TestHelper;

namespace API.Test.Validation.Validators
{
    public class GetTrialByStatusValidatorTests
    {
        private readonly GetTrialByStatusValidator _validator;

        public GetTrialByStatusValidatorTests()
        {
            _validator = new GetTrialByStatusValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Page_Is_Less_Than_One()
        {
            // Arrange
            var model = new GetTrialByStatusRequestModel { Page = 0 };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Page)
                  .WithErrorMessage("Page number must be greater than 0.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Page_Is_Greater_Than_Zero()
        {
            // Arrange
            var model = new GetTrialByStatusRequestModel { Page = 1 };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Page);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(101)]
        public void Should_Have_Error_When_Size_Is_Invalid(int size)
        {
            // Arrange
            var model = new GetTrialByStatusRequestModel { Size = size };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Size)
                  .WithErrorMessage("Size must be greather then 0 and less or equal to 100.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Should_Not_Have_Error_When_Size_Is_Valid(int size)
        {
            // Arrange
            var model = new GetTrialByStatusRequestModel { Size = size };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Size);
        }

        [Fact]
        public void Should_Have_Error_When_Status_Is_Null()
        {
            // Arrange
            var model = new GetTrialByStatusRequestModel { Status = null };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Status)
                  .WithErrorMessage("Status must be populated.");
        }

        [Theory]
        [InlineData("NotStarted")]
        [InlineData("Not Started")]
        [InlineData("Ongoing")]
        [InlineData("Completed")]
        public void Should_Not_Have_Error_When_Status_Is_Valid(string status)
        {
            // Arrange
            var model = new GetTrialByStatusRequestModel { Status = status };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData("InvalidStatus")]
        [InlineData("Unknown Status")]
        public void Should_Have_Error_When_Status_Is_Invalid(string status)
        {
            // Arrange
            var model = new GetTrialByStatusRequestModel { Status = status };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Status)
                  .WithErrorMessage("Invalid status.");
        }
    }
}
