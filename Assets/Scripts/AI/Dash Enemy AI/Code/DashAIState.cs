using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashAIState", menuName = "AI/Dash/AI State")]
public class DashAIState : ScriptableObject
{
    
    [SerializeField]
    private List<DashAIAction> actions;

    public void UpdateStateGizmos(DashAIController controller)
    {
        foreach (var action in actions)
        {
            action.UpdateActionGizmos(controller);
        }
    }

    public void UpdateState(DashAIController controller)
    {
        foreach (var action in actions)
        {
            action.UpdateAction(controller);
        }
    }
}
