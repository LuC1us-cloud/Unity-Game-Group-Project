using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DashAIAction : ScriptableObject
{
    public abstract void UpdateActionGizmos(DashAIController controller);

    public abstract void UpdateAction(DashAIController controller);
}