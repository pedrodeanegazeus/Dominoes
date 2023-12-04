using System;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;

namespace Dominoes.Core.Services
{
    internal class ChatService : IChatService
    {
        public event Action ChatReceived;

        private readonly IGzLogger<ChatService> _logger;

        public ChatService(IGzLogger<ChatService> logger)
        {
            _logger = logger;
        }
    }
}
