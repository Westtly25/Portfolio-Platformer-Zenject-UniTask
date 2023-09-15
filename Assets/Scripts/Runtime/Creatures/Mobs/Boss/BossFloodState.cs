using UnityEngine;

namespace Scripts.Creatures.Mobs.Boss
{
    public class BossFloodState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            FloodController spawner = animator.GetComponent<FloodController>();
            spawner.StartFlooding();
        }
    }
}