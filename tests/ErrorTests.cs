using ErrorOr;
using FluentAssertions;

namespace Tests;

public class ErrorTests
{
    private const string ErrorCode = "ErrorCode";
    private const string ErrorDescription = "ErrorDescription";
    private static readonly Dictionary<string, object> Dictionary = new()
    {
        { "key1", "value1" },
        { "key2", 21 },
    };

    [Fact]
    public void CreateError_WhenFailureError_ShouldHaveErrorTypeFailure()
    {
        // Act
        var error = Error.Failure(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        ValidateError(error, expectedErrorType: ErrorType.Failure);
    }

    [Fact]
    public void CreateError_WhenUnexpectedError_ShouldHaveErrorTypeFailure()
    {
        // Act
        var error = Error.Unexpected(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        ValidateError(error, expectedErrorType: ErrorType.Unexpected);
    }

    [Fact]
    public void CreateError_WhenValidationError_ShouldHaveErrorTypeValidation()
    {
        // Act
        var error = Error.Validation(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        ValidateError(error, expectedErrorType: ErrorType.Validation);
    }

    [Fact]
    public void CreateError_WhenConflictError_ShouldHaveErrorTypeConflict()
    {
        // Act
        var error = Error.Conflict(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        ValidateError(error, expectedErrorType: ErrorType.Conflict);
    }

    [Fact]
    public void CreateError_WhenNotFoundError_ShouldHaveErrorTypeNotFound()
    {
        // Act
        var error = Error.NotFound(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        ValidateError(error, expectedErrorType: ErrorType.NotFound);
    }

    [Fact]
    public void CreateError_WhenNotAuthorizedError_ShouldHaveErrorTypeUnauthorized()
    {
        // Act
        var error = Error.Unauthorized(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        ValidateError(error, expectedErrorType: ErrorType.Unauthorized);
    }

    [Fact]
    public void CreateError_WhenCustomType_ShouldHaveCustomErrorType()
    {
        // Act
        var error = Error.Custom(1232, ErrorCode, ErrorDescription, Dictionary);

        // Assert
        ValidateError(error, expectedErrorType: (ErrorType)1232);
    }

    private static void ValidateError(Error error, ErrorType expectedErrorType)
    {
        error.Code.Should().Be(ErrorCode);
        error.Description.Should().Be(ErrorDescription);
        error.Type.Should().Be(expectedErrorType);
        error.NumericType.Should().Be((int)expectedErrorType);
        error.Dictionary.Should().BeEquivalentTo(Dictionary);
    }
}
