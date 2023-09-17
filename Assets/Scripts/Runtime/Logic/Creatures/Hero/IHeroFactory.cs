using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.Creatures.Hero
{
    public interface IHeroFactory
    {
        UniTask<Hero> CreateAsync();
    }
}