using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageAIState", menuName = "AI/Mage/AI State")]
public class MageAIState : ScriptableObject
{
    
    [SerializeField]
    private List<MageAIAction> actions;

    public void UpdateStateGizmos(MageAIController controller)
    {
        foreach (var action in actions)
        {
            action.UpdateActionGizmos(controller);
        }
    }

    public void UpdateState(MageAIController controller)
    {
        foreach (var action in actions)
        {
            action.UpdateAction(controller);
        }
    }

}
