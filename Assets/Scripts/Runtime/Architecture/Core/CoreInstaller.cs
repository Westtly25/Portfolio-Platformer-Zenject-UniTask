using Zenject;
using Scripts.Model.Data;
using Scripts.Creatures.Hero;
using Scripts.Model.Definitions;

public class CoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameConfigsProvider>()
                 .FromNew().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<InventoryHandler>()
                 .FromNew().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<HeroFactory>()
                 .FromNew().AsSingle();
    }
}