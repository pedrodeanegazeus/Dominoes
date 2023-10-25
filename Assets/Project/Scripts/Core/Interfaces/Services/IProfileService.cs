using System.Threading.Tasks;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IProfileService
    {
        bool IsGuest { get; }

        Task<string> GetProfileNameAsync();

        Task InitializeAsync();
    }
}
