using Cysharp.Threading.Tasks;

namespace Assets.Code.Scripts.Runtime.Save_system.Interface
{
    public interface ISaveLoadService
    {
        UniTask Initialize();
        UniTask LoadAsync();
        UniTask SaveAsync();
        void Delete(int id);
        void CreateNewSave();
    }
}