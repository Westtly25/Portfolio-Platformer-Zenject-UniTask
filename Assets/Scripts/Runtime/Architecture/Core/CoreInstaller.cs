using Zenject;
using Scripts.Model.Definitions;

public class CoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameConfigsProvider>()
                 .FromNew().AsSingle().NonLazy();
    }
}