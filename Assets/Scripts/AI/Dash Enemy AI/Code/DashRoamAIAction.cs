using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashRoamAIAction", menuName = "AI/Dash/Roam AI Action")]
public class DashRoamAIAction : DashAIAction
{
    [Min(0f)]
    [SerializeField]
    private float newDestinationDistance = 1.1f;

    private float waitTime;

    [Min(0f)]
    [SerializeField]
    private float setWaitTime;


    public override void UpdateActionGizmos(DashAIController controller)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position, newDestinationDistance);
    }

    public override void UpdateAction(DashAIController controller)
    {

        if (Vector2.Distance(controller.Target.position, controller.transform.position) <= newDestinationDistance)
        {
            if (waitTime <= 0)
            {
                controller.Target = controller.DashController.patrolPoints[Random.Range(0, controller.DashController.patrolPoints.Length)];
                controller.DashController.LookAtTarget(controller.Target);
                waitTime = setWaitTime;
            }
           waitTime -= Time.deltaTime;
        }
        
    }

}
