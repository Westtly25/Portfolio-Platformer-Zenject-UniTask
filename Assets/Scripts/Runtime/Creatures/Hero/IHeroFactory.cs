using Cysharp.Threading.Tasks;

namespace Scripts.Creatures.Hero
{
    public interface IHeroFactory
    {
        UniTask<Hero> CreateAsync();
    }
}