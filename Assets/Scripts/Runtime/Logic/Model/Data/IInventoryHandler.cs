using Cysharp.Threading.Tasks;
using Scripts.Model.Definitions.Repositories;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Model.Data
{
    public interface IInventoryHandler
    {
        void Add(string id, int value);
        int Count(string id);
        InventoryItemData[] GetAll(params ItemTag[] tags);
        UniTaskVoid Initialize();
        bool IsEnough(params ItemWithCount[] items);
        void Remove(string id, int value);
    }
}