using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchPlayerAIAction", menuName = "AI/Basic/Search Player AI Action")]
public class SearchPlayerAIAction : AIAction
{
    
    [Min(0f)]
    [SerializeField]
    private float searchDistance = 10f;

    [SerializeField]
    private AIState playerFoundState;

    public override void UpdateActionGizmos(AIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, searchDistance);
    }

    public override void UpdateAction(AIController controller)
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

    private static MainPlayer GetPlayer(AIController controller)
    {
        return controller.Player;
    }

}
