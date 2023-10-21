using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers.HUDs
{
    internal class VipButtonController : MonoBehaviour
    {
        private readonly IVipService _vipService = ServiceProvider.GetRequiredService<IVipService>();
    }
}
