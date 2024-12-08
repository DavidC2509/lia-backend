namespace Lia.SharedKernel.Exceptions;

public class JsonsException : ServiceException
{
    public JsonsException(string error)
    {
        HttpStatus = 400;
        ErrorMessage = "Error Seriliation Json";
        Errors = new Dictionary<string, List<string>>
        {
            { "Interno", new List<string> { error } }
        };
    }

    public override int HttpStatus { get; }
    public override string ErrorMessage { get; }

    public override Dictionary<string, List<string>> Errors { get; }
}