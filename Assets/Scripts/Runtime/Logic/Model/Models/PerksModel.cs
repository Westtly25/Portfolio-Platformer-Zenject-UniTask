using System;
using Scripts.Model.Data.Properties;
using Scripts.Model.Definitions;
using Scripts.Utilities;
using Scripts.Utilities.Disposables;

namespace Scripts.Model.Data
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData data;
        public readonly StringProperty InterfaceSelection = new();

        public readonly Cooldown Cooldown = new();
        private readonly CompositeDisposable trash = new();
        public event Action OnChanged;

        public PerksModel(PlayerData data)
        {
            this.data = data;
            InterfaceSelection.Value = CoreGameConfigs.ConfigsInstance.Perks.All[0].Id;

            trash.Retain(this.data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));

            if (!string.IsNullOrEmpty(this.data.Perks.Used.Value))
                SelectPerk(this.data.Perks.Used.Value);
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public string Used => data.Perks.Used.Value;
        public bool IsSuperThrowSupported => data.Perks.Used.Value == "super-throw" && Cooldown.IsReady;
        public bool IsDoubleJumpSupported => data.Perks.Used.Value == "double-jump" && Cooldown.IsReady;
        public bool IsShieldSupported => data.Perks.Used.Value == "shield" && Cooldown.IsReady;

        public void Unlock(string id)
        {
            var def = CoreGameConfigs.ConfigsInstance.Perks.Get(id);
            var isEnoughResources = data.Inventory.IsEnough(def.Price);

            if (isEnoughResources)
            {
                data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                data.Perks.AddPerk(id);

                OnChanged?.Invoke();
            }
        }

        public void SelectPerk(string selected)
        {
            var perkDef = CoreGameConfigs.ConfigsInstance.Perks.Get(selected);
            Cooldown.Value = perkDef.Cooldown;
            data.Perks.Used.Value = selected;
        }

        public bool IsUsed(string perkId) =>
            data.Perks.Used.Value == perkId;

        public bool IsUnlocked(string perkId) =>
            data.Perks.IsUnlocked(perkId);

        public bool CanBuy(string perkId)
        {
            var def = CoreGameConfigs.ConfigsInstance.Perks.Get(perkId);
            return data.Inventory.IsEnough(def.Price);
        }

        public void Dispose() =>
            trash.Dispose();
    }
}