using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAIController : MonoBehaviour
{
    [SerializeField]
    private MageAIState initialState;

    private MageAIState currentAIState;
    private MainPlayer player;

    private MageAIState CurrentAIState
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

    public mageEnemy mageController;

    public Entity stats;

    public float nextFireTime { get; set; }

    public Transform Target { get; set; }

    public MainPlayer Player => player;

    private void OnDrawGizmos()
    {
        CurrentAIState.UpdateStateGizmos(this);
    }

    private void Awake() {
        player = FindObjectOfType<MainPlayer>();
        mageController = GetComponent<mageEnemy>();
        stats = GetComponent<Entity>();
        Target = this.transform;
    }

    private void FixedUpdate() {
        CurrentAIState.UpdateState(this);
        mageController.Move(Target);
    }

    public void ChangeState(MageAIState newAIState)
    {
        currentAIState = newAIState;
    }

}
