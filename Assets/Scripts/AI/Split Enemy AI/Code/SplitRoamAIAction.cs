using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitRoamAIAction", menuName = "AI/Split/Roam AI Action")]
public class SplitRoamAIAction : SplitAIAction
{
    [Min(0f)]
    [SerializeField]
    private float newDestinationDistance = 0.1f;

    private float waitTime;

    [Min(0f)]
    [SerializeField]
    private float setWaitTime;

    [SerializeField]
    private SplitAIState lowHealthState;

    public override void UpdateActionGizmos(SplitAIController controller)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position, newDestinationDistance);
    }

    public override void UpdateAction(SplitAIController controller)
    {
        if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth < 0.4f)
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
