using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitSearchPlayerAIAction", menuName = "AI/Split/Search Player AI Action")]
public class SplitSearchPlayerAIAction : SplitAIAction
{
    
    [Min(0f)]
    [SerializeField]
    private float searchDistance = 10f;

    [SerializeField]
    private SplitAIState playerFoundState;

    public override void UpdateActionGizmos(SplitAIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, searchDistance);
    }

    public override void UpdateAction(SplitAIController controller)
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

    private static MainPlayer GetPlayer(SplitAIController controller)
    {
        return controller.Player;
    }

}
