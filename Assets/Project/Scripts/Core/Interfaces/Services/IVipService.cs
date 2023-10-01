using System.Threading.Tasks;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IVipService
    {
        bool IsVip { get; }

        Task InitializeAsync();
    }
}
