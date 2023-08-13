using Zenject;
using Scripts.AssetManagement;
using System.Numerics;
using Cysharp.Threading.Tasks;

namespace Scripts.Creatures.Hero
{
    public class HeroFactory
    {
        private readonly DiContainer container;
        private readonly IAssetProvider provider;

        public HeroFactory(DiContainer container, IAssetProvider provider)
        {
            this.container = container;
            this.provider = provider;
        }

        public async UniTask<Hero> CreateAsync()
        {
            Hero asset = await provider.Load<Hero>(AssetAddress.Hero);
            return container.InstantiateComponent<Hero>(asset.gameObject);
        }
    }
}