using UnityEngine;

namespace Assets.Scripts.Architecture.Services.Save_Service.Interface
{
    public interface IPersistentDataListener
    {
        public void LoadData(GameData gameData);
        public void SaveData(ref GameData gameData);
    }
}