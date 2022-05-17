using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SplitAIAction : ScriptableObject
{
    public abstract void UpdateActionGizmos(SplitAIController controller);

    public abstract void UpdateAction(SplitAIController controller);
}
