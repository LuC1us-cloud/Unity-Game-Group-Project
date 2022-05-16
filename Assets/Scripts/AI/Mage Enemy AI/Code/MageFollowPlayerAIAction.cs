using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageFollowPlayerAIAction", menuName = "AI/Mage/Follow Player AI Action")]
public class MageFollowPlayerAIAction : MageAIAction
{
    [Min(0f)]
    [SerializeField]
    private float followDistance = 10f;

    [Min(0f)]
    [SerializeField]
    private float stoppingDistance;

    [Min(0f)]
    [SerializeField]
    private float retreatDistance;

    [SerializeField]
    private MageAIState playerMissingState;

    [SerializeField]
    private MageAIState lowHealthState;

    public override void UpdateActionGizmos(MageAIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, followDistance);
    }

    public override void UpdateAction(MageAIController controller)
    {
        if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth < 0.4f && controller.mageController.safeSpot != null)
        {
            controller.ChangeState(lowHealthState);
            return;
        }

        var player = GetPlayer(controller);
        if (player == null)
        {
            controller.Target = controller.mageController.patrolPoints[Random.Range(0, controller.mageController.patrolPoints.Length)];
            controller.ChangeState(playerMissingState);
            return;
        }

        var playerPosition = player.transform.position;
        var distance = Vector2.Distance(controller.transform.position, playerPosition);

        if (distance > followDistance)
        {
            controller.Target = controller.mageController.patrolPoints[Random.Range(0, controller.mageController.patrolPoints.Length)];
            controller.ChangeState(playerMissingState);
            return;
        }

        if (distance > stoppingDistance)
        {
            controller.Target = controller.transform;
        }
        else if (distance > retreatDistance)
        {
            controller.Target = controller.transform;
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, player.transform.position, -(controller.mageController.speed) * Time.deltaTime);
        }
        else controller.Target = player.transform;

    }

    private static MainPlayer GetPlayer(MageAIController controller)
    {
        return controller.Player;
    }
}
