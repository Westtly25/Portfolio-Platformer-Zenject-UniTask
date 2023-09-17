using Scripts.Model.Definitions.Player;
using Scripts.Model.Definitions.Repositories;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Model.Definitions
{
    public interface ICoreGameConfigs
    {
        ItemsRepository Items { get; }
        ThrowableRepository Throwable { get; }
        PotionRepository Potions { get; }
        PerkRepository Perks { get; }
        PlayerConfigs Player { get; }
    }
}