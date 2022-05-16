using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReatreatAIAction", menuName = "AI/Basic/Reatreat AI Action")]
public class RetreatAIAction : AIAction
{
    [SerializeField]
    private AIState fullHealthState;

    public override void UpdateActionGizmos(AIController controller)
    {
        
    }

    public override void UpdateAction(AIController controller)
    {        
        controller.Target = controller.unitController.safeSpot;
        if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth > 0.9f)
        {
            controller.ChangeState(fullHealthState);
        }
        
    }
}
