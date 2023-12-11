using System.Threading.Tasks;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IProfileService
    {
        bool IsGuest { get; }
        bool IsLoggedIn { get; }

        Task<string> GetProfileNameAsync();

        void Initialize();
    }
}
