using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitAIState", menuName = "AI/Split/AI State")]
public class SplitAIState : ScriptableObject
{
    
    [SerializeField]
    private List<SplitAIAction> actions;

    public void UpdateStateGizmos(SplitAIController controller)
    {
        foreach (var action in actions)
        {
            action.UpdateActionGizmos(controller);
        }
    }

    public void UpdateState(SplitAIController controller)
    {
        foreach (var action in actions)
        {
            action.UpdateAction(controller);
        }
    }

}
