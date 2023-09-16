using Zenject;
using Scripts.Creatures.Hero;
using Assets.Code.Scripts.Runtime.Save_system.Interface;

namespace Assets.Scripts.Architecture.Core
{
    public class CoreFlow : IInitializable
    {
        private readonly IHeroFactory heroFactory;
        private readonly ISaveLoadService saveLoadService;

        public CoreFlow(IHeroFactory heroFactory,
                        ISaveLoadService saveLoadService)
        {
            this.heroFactory = heroFactory;
            this.saveLoadService = saveLoadService;
        }

        public async void Initialize()
        {
            await heroFactory.CreateAsync();
        }
    }
}
