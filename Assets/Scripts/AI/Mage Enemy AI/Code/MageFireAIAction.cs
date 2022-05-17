using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageFirePlayerAIAction", menuName = "AI/Mage/Fire Player AI Action")]
public class MageFireAIAction : MageAIAction
{
    [Min(0f)]
    [SerializeField]
    private float firingCooldown = 5f;

    public override void UpdateActionGizmos(MageAIController controller)
    {
        
        var player = GetPlayer(controller);
        if (player == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(controller.transform.position, player.transform.position);

    }

    public override void UpdateAction(MageAIController controller)
    {
        var player = GetPlayer(controller);
        if (player == null)
        {
            return;
        }

        if (IsFire(controller))
        {
            Fire(controller);
        }
        

    }

    private static MainPlayer GetPlayer(MageAIController controller)
    {
        return controller.Player;
    }

    private bool IsFire(MageAIController controller)
    {
        return controller.nextFireTime < Time.time;
    }

    private void Fire(MageAIController controller)
    {
        controller.mageController.Fire(controller.Player.transform);
        controller.nextFireTime = Time.time + firingCooldown;
    }

}
