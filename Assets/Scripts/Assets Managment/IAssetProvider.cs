using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Scripts.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask Initialize();
        Task<GameObject> Instantiate(string path, Vector3 at);
        Task<GameObject> Instantiate(string path);
        UniTask<T> Load<T>(AssetReference monsterDataPrefabReference) where T : class;
        UniTask<T> Load<T>(string address) where T : class;
        void Cleanup();
    }
}