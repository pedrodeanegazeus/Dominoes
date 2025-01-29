using System.Threading.Tasks;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace Gazeus.Mobile.Domino.Infrastructure.Clients
{
    public class GenericClient
    {
        private readonly IGzLogger<GenericClient> _logger;

        public int Timeout { get; set; }

        public GenericClient(IGzLogger<GenericClient> logger)
        {
            _logger = logger;

            Timeout = 5;
        }

        public async Task<Texture2D> GetTextureAsync(string uri)
        {
            _logger.LogMethodCall(nameof(GetTextureAsync),
                                  uri);

            TaskCompletionSource<bool> taskCompletionSource = new();

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
            UnityWebRequestAsyncOperation asyncOperation = request.SendWebRequest();

            asyncOperation.completed += AsyncOperation_Completed;

            Task requestTask = taskCompletionSource.Task;
            Task timeoutTask = Task.Delay(Timeout * 1000);

            await Task.WhenAny(requestTask, timeoutTask);

            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            return texture;

            void AsyncOperation_Completed(AsyncOperation asyncOperation)
            {
                taskCompletionSource.SetResult(true);
            }
        }
    }
}
