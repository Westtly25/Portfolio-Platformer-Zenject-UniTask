using Zenject;
using Scripts.AssetManagement;
using Assets.Code.Scripts.Runtime.Save_system.Interface;

namespace Assets.Scripts.Architecture.Bootstrap
{
    public sealed class BootstrapFlow : IInitializable
    {
        private readonly IAssetProvider assetProvider;
        private readonly ISaveLoadService saveLoadService;

        [Inject]
        public BootstrapFlow(IAssetProvider assetProvider, ISaveLoadService saveLoadService)
        {
            this.assetProvider = assetProvider;
            this.saveLoadService = saveLoadService;
        }

        public async void Initialize()
        {
            await assetProvider.Initialize();
            await saveLoadService.Initialize();
        }
    }
}
