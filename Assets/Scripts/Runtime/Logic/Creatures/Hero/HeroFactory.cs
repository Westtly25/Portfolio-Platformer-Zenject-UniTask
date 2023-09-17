using Zenject;
using UnityEngine;
using Scripts.AssetManagement;
using Cysharp.Threading.Tasks;

namespace Scripts.Creatures.Hero
{
    public class HeroFactory : IHeroFactory
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
            GameObject asset = await provider.Load<GameObject>(AssetAddress.Hero);
            return container.InstantiateComponent<Hero>(asset);
        }
    }
}