using Zenject;
using Scripts.AssetManagement;
using Assets.Scripts.Architecture.Services.Save_Service;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AssetProvider>()
                 .FromNew().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<FileDataHandler>()
                 .FromNew().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<SaveLoadService>()
                 .FromNew().AsSingle().NonLazy();
    }
}