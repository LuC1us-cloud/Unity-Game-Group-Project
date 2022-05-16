using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MageAIAction : ScriptableObject
{
    public abstract void UpdateActionGizmos(MageAIController controller);

    public abstract void UpdateAction(MageAIController controller);
}
