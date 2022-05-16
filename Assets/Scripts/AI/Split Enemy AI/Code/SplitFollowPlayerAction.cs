using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitFollowPlayerAIAction", menuName = "AI/Split/Follow Player AI Action")]
public class SplitFollowPlayerAction : SplitAIAction
{
    [Min(0f)]
    [SerializeField]
    private float followDistance = 10f;

    [SerializeField]
    private SplitAIState playerMissingState;

    [SerializeField]
    private SplitAIState lowHealthState;

    public override void UpdateActionGizmos(SplitAIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, followDistance);
    }

    public override void UpdateAction(SplitAIController controller)
    {

         if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth < 0.4f)
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

    private static MainPlayer GetPlayer(SplitAIController controller)
    {
        return controller.Player;
    }
}
