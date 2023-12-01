using System;
using System.Threading.Tasks;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IGameplayService
    {
        event Action ChatReceived;

        Task InitializeAsync();
    }
}
