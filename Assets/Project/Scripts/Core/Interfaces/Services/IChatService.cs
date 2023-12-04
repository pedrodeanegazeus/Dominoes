using System;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IChatService
    {
        event Action ChatReceived;
    }
}
