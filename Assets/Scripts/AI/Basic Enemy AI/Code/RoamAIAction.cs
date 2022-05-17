using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoamAIAction", menuName = "AI/Basic/Roam AI Action")]
public class RoamAIAction : AIAction
{
    [Min(0f)]
    [SerializeField]
    private float newDestinationDistance = 0.1f;

    private float waitTime;

    [Min(0f)]
    [SerializeField]
    private float setWaitTime;

    [SerializeField]
    private AIState lowHealthState;

    public override void UpdateActionGizmos(AIController controller)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position, newDestinationDistance);
    }

    public override void UpdateAction(AIController controller)
    {        
        if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth < 0.4f && controller.unitController.safeSpot != null)
        {
            controller.ChangeState(lowHealthState);
            return;
        }

        if (Vector2.Distance(controller.Target.position, controller.transform.position) <= newDestinationDistance)
        {
            if (waitTime <= 0)
            {
                controller.Target = controller.unitController.patrolPoints[Random.Range(0, controller.unitController.patrolPoints.Length)];
                waitTime = setWaitTime;
            }
           waitTime -= Time.deltaTime;
        }
        
    }

}
