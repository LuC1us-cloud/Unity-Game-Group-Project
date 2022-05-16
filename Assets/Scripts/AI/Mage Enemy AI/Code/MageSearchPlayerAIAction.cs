using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageSearchPlayerAIAction", menuName = "AI/Mage/Search Player AI Action")]
public class MageSearchPlayerAIAction : MageAIAction
{
    
    [Min(0f)]
    [SerializeField]
    private float searchDistance = 10f;

    [SerializeField]
    private MageAIState playerFoundState;

    public override void UpdateActionGizmos(MageAIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, searchDistance);
    }

    public override void UpdateAction(MageAIController controller)
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

    private static MainPlayer GetPlayer(MageAIController controller)
    {
        return controller.Player;
    }

}