namespace Lia.SharedKernel.Exceptions;

public abstract class ServiceException : Exception
{
    public abstract int HttpStatus { get; }
    public abstract string ErrorMessage { get; }

    public abstract Dictionary<string, List<string>> Errors { get; }
}