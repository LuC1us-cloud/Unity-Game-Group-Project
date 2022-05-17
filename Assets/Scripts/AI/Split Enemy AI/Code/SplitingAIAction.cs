using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitingAIAction", menuName = "AI/Split/Spliting AI Action")]
public class SplitingAIAction : SplitAIAction
{
    [Min(0f)]
    [SerializeField]
    private float setWaitTime;

    private float waitTime;

    public override void UpdateActionGizmos(SplitAIController controller)
    {

    }

    public override void UpdateAction(SplitAIController controller)
    {
        controller.Target = controller.transform;
        if (waitTime >= setWaitTime)
        {
            controller.Split();
        }
        waitTime += Time.deltaTime;

    }

}
