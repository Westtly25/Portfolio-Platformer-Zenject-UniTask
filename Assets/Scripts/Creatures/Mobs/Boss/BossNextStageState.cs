using UnityEngine;
using Scripts.Components.GoBased;
using Scripts.Creatures.Mobs.Boss;

public class BossNextStageState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CircularProjectileSpawner spawner = animator.GetComponent<CircularProjectileSpawner>();
        spawner.Stage++;

        ChangeLightsComponent changeLight = animator.GetComponent<ChangeLightsComponent>();
        changeLight.SetColor();
    }
}