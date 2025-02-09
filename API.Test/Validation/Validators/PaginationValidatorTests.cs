using API.Validation.RequestModels;
using API.Validation.Validators;
using FluentValidation.TestHelper;

namespace API.Test.Validation.Validators
{
    public class PaginationValidatorTests
    {
        private readonly PaginationValidator _validator;

        public PaginationValidatorTests()
        {
            _validator = new PaginationValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(null)]
        public void Page_ValidValues_ShouldNotHaveValidationError(int? page)
        {
            // Arrange
            var model = new PaginationQueryRequestModel { Page = page };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Page);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Page_InvalidValues_ShouldHaveValidationError(int page)
        {
            // Arrange
            var model = new PaginationQueryRequestModel { Page = page };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Page)
                  .WithErrorMessage("Page number must be greater than 0.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(null)]
        public void Size_ValidValues_ShouldNotHaveValidationError(int? size)
        {
            // Arrange
            var model = new PaginationQueryRequestModel { Size = size };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Size);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(101)]
        public void Size_InvalidValues_ShouldHaveValidationError(int size)
        {
            // Arrange
            var model = new PaginationQueryRequestModel { Size = size };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Size)
                  .WithErrorMessage("Size must be greather then 0 and less or equal to 100.");
        }
    }
}
