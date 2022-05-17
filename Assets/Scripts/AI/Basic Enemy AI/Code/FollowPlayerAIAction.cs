using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FollowPlayerAIAction", menuName = "AI/Basic/Follow Player AI Action")]
public class FollowPlayerAIAction : AIAction
{
    [Min(0f)]
    [SerializeField]
    private float followDistance = 10f;

    [SerializeField]
    private AIState playerMissingState;

    [SerializeField]
    private AIState lowHealthState;

    public override void UpdateActionGizmos(AIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, followDistance);
    }

    public override void UpdateAction(AIController controller)
    {
        if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth < 0.4f && controller.unitController.safeSpot != null)
        {
            controller.ChangeState(lowHealthState);
            return;
        }

        var player = GetPlayer(controller);
        if (player == null)
        {
            controller.Target = controller.unitController.patrolPoints[Random.Range(0, controller.unitController.patrolPoints.Length)];
            controller.ChangeState(playerMissingState);
            return;
        }

        var playerPosition = player.transform.position;
        var distance = Vector2.Distance(controller.transform.position, playerPosition);

        if (distance > followDistance)
        {
            controller.Target = controller.unitController.patrolPoints[Random.Range(0, controller.unitController.patrolPoints.Length)];
            controller.ChangeState(playerMissingState);
            return;
        }

        controller.Target = player.transform;
    }

    private static MainPlayer GetPlayer(AIController controller)
    {
        return controller.Player;
    }
}
