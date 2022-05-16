using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private AIState initialState;

    private AIState currentAIState;
    private MainPlayer player;

    private AIState CurrentAIState
    {
        get
        {
            if (currentAIState == null)
            {
                currentAIState = initialState;
            }

            return currentAIState;
        }
    }

    public Unit unitController;

    public Entity stats;

    public Transform Target { get; set; }

    public MainPlayer Player => player;

    private void OnDrawGizmos()
    {
        CurrentAIState.UpdateStateGizmos(this);
    }

    private void Awake() {
        player = FindObjectOfType<MainPlayer>();
        unitController = GetComponent<Unit>();
        stats = GetComponent<Entity>();
        Target = this.transform;
    }

    private void FixedUpdate() {
        CurrentAIState.UpdateState(this);
        unitController.Move(Target);
    }

    public void ChangeState(AIState newAIState)
    {
        currentAIState = newAIState;
    }

}
