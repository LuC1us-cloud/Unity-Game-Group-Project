using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAIController : MonoBehaviour
{
    [SerializeField]
    private DashAIState initialState;

    private DashAIState currentAIState;
    private MainPlayer player;

    private DashAIState CurrentAIState
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

    public DashEnemy DashController;

    public Entity stats;

    public int dashDamage = 50;

    public int originalDamage;

    public float nextFireTime { get; set; }

    public Transform Target { get; set; }

    public MainPlayer Player => player;

    private void OnDrawGizmos()
    {
        CurrentAIState.UpdateStateGizmos(this);
    }

    private void Awake() {
        player = FindObjectOfType<MainPlayer>();
        DashController = GetComponent<DashEnemy>();
        stats = GetComponent<Entity>();
        originalDamage = stats.Damage;
        Target = this.transform;
    }

    private void FixedUpdate() {
        CurrentAIState.UpdateState(this);
        DashController.Move(Target);
    }

    public void ChangeState(DashAIState newAIState)
    {
        currentAIState = newAIState;
    }
}
