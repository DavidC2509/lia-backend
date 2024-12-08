namespace Lia.SharedKernel.Exceptions;

public class CustomException : ServiceException
{
    public CustomException(int statusCode, string message, Dictionary<string, List<string>> error)
    {
        HttpStatus = statusCode;
        ErrorMessage = message;
        Errors = error;
    }

    public CustomException(int statusCode, string message, string error)
    {
        HttpStatus = statusCode;
        ErrorMessage = message;
        Errors = new Dictionary<string, List<string>>
        {
            { "Interno", new List<string> { error } }
        };
    }

    public CustomException(string error)
    {
        HttpStatus = (int)System.Net.HttpStatusCode.BadRequest;
        ErrorMessage = "CustomError";
        Errors = new Dictionary<string, List<string>>
        {
            { "error", new List<string> { error } }
        };
    }

    public override int HttpStatus { get; }
    public override string ErrorMessage { get; }

    public override Dictionary<string, List<string>> Errors { get; }
}