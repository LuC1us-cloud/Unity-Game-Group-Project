using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageRoamAIAction", menuName = "AI/Mage/Roam AI Action")]
public class MageRoamAIAction : MageAIAction
{
    [Min(0f)]
    [SerializeField]
    private float newDestinationDistance = 1.1f;

    private float waitTime;

    [Min(0f)]
    [SerializeField]
    private float setWaitTime;

    [SerializeField]
    private MageAIState lowHealthState;

    public override void UpdateActionGizmos(MageAIController controller)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position, newDestinationDistance);
    }

    public override void UpdateAction(MageAIController controller)
    {        
        if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth < 0.4f && controller.mageController.safeSpot != null)
        {
            controller.ChangeState(lowHealthState);
            return;
        }

        if (Vector2.Distance(controller.Target.position, controller.transform.position) <= newDestinationDistance)
        {
            if (waitTime <= 0)
            {
                controller.Target = controller.mageController.patrolPoints[Random.Range(0, controller.mageController.patrolPoints.Length)];
                waitTime = setWaitTime;
            }
           waitTime -= Time.deltaTime;
        }
        
    }

}
