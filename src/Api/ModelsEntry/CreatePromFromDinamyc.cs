namespace Lia.Api.ModelsEntry
{
    public class CreatePromFromDinamyc
    {
        public required Guid PromID { get; set; }

    }

    public class UpAssistantProm
    {
        public required string NameFile { get; set; }
        public required string ModelIa { get; set; }
        public required string ToolsIa { get; set; }

    }
}
