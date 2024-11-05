namespace Lia.SharedKernel.Interface;

public interface IErrorPropertiesFactory
{
    IDictionary<string, object?> CreateCommonProperties(Dictionary<string, List<string>>? error);

    IDictionary<string, object?> CreateCommonProperties(string error);

}