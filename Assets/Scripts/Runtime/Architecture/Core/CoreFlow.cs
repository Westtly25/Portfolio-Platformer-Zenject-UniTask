using Zenject;
using Scripts.Creatures.Hero;
using Scripts.Model.Definitions;
using Assets.Code.Scripts.Runtime.Save_system.Interface;

namespace Assets.Scripts.Architecture.Core
{
    public class CoreFlow : IInitializable
    {
        private readonly IHeroFactory heroFactory;
        private readonly ISaveLoadService saveLoadService;
        private readonly GameConfigsProvider gameConfigsProvider;

        public CoreFlow(IHeroFactory heroFactory,
                        ISaveLoadService saveLoadService,
                        GameConfigsProvider gameConfigsProvider)
        {
            this.heroFactory = heroFactory;
            this.saveLoadService = saveLoadService;
            this.gameConfigsProvider = gameConfigsProvider;
        }

        public async void Initialize()
        {
            await gameConfigsProvider.Initialize();
            await heroFactory.CreateAsync();
        }
    }
}
