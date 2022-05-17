using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageReatreatAIAction", menuName = "AI/Mage/Reatreat AI Action")]
public class MageRetreatAIAction : MageAIAction
{
    [SerializeField]
    private MageAIState fullHealthState;

    public override void UpdateActionGizmos(MageAIController controller)
    {
        
    }

    public override void UpdateAction(MageAIController controller)
    {        
        controller.Target = controller.mageController.safeSpot;
        if ((float)controller.stats.CurrentHealth / (float)controller.stats.MaxHealth > 0.9f)
        {
            controller.ChangeState(fullHealthState);
        }
        
    }
}
