using Zenject;
using Scripts.AssetManagement;

namespace Assets.Scripts.Architecture.Bootstrap
{
    public sealed class BootstrapFlow : IInitializable
    {
        private readonly IAssetProvider assetProvider;

        [Inject]
        public BootstrapFlow(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async void Initialize()
        {
            await assetProvider.Initialize();
        }
    }
}
