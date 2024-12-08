using Lia.Core.Models.TravelC;

namespace Lia.Core.Interface
{
    public interface ITravelCServices
    {
        Task<string> AuthenticateAsync(CancellationToken cancellationToken);
        Task<ResponseThemeTravelC> GetTheme(CancellationToken cancellationToken);
        Task<ResponseTravelIdea> GetTravellIdea(int themes, CancellationToken cancellationToken);

    }
}
