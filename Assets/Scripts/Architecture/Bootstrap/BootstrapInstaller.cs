using Scripts.AssetManagement;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AssetProvider>()
                 .FromNew().AsSingle().NonLazy();
    }
}