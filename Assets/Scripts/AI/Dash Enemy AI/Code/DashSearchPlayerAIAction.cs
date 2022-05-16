using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSearchPlayerAIAction", menuName = "AI/Dash/Search Player AI Action")]
public class DashSearchPlayerAIAction : DashAIAction
{
    
    [Min(0f)]
    [SerializeField]
    private float searchDistance = 10f;

    [SerializeField]
    private DashAIState playerFoundState;

    public override void UpdateActionGizmos(DashAIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, searchDistance);
    }

    public override void UpdateAction(DashAIController controller)
    {
        var player = GetPlayer(controller);
        if (player == null)
        {
            return;
        }

        var playerPosition = player.transform.position;
        var distance = Vector2.Distance(controller.transform.position, playerPosition);

        if (distance > searchDistance)
        {
            return;
        }

        controller.ChangeState(playerFoundState);
    }

    private static MainPlayer GetPlayer(DashAIController controller)
    {
        return controller.Player;
    }

}
