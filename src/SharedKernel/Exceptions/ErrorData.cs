namespace Lia.SharedKernel.Exceptions;

public class ErrorData
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }

    public ErrorData(string type, string title, int status, string traceId, Dictionary<string, List<string>> errors)
    {
        Type = type;
        Title = title;
        Status = status;
        TraceId = traceId;
        Errors = errors;
    }
}