namespace Dominoes.Core.Interfaces.Services
{
    internal interface IVipService
    {
        bool IsVip { get; }

        void Initialize();
    }
}
