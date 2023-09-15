using Zenject;
using UnityEngine;
using Scripts.Creatures.Hero;

namespace Scripts.Effects
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField]
        private float effectValue;
        [SerializeField]
        private Transform followTarget;

        private float startX;

        private Hero hero;

        [Inject]
        public void Constructor(Hero hero) =>
            this.hero = hero;

        private void Start() =>
            startX = hero.transform.position.x;

        private void LateUpdate()
        {
            if (hero == null)
                return;

            var currentPosition = transform.position;
            var deltaX = followTarget.position.x * effectValue;
            transform.position = new Vector3(startX + deltaX, currentPosition.y, currentPosition.z);
        }
    }
}