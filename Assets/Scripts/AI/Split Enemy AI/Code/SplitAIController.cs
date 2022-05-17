using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitAIController : MonoBehaviour
{
    [SerializeField]
    private SplitAIState initialState;

    private SplitAIState currentAIState;
    private MainPlayer player;

    private SplitAIState CurrentAIState
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

    public GameObject split1;
    public GameObject split2;

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

    public void ChangeState(SplitAIState newAIState)
    {
        currentAIState = newAIState;
    }

    public void Split()
    {
        Destroy(this.gameObject);
        GameObject Temp1 = Instantiate(split1, transform.position, Quaternion.identity);
        GameObject Temp2 = Instantiate(split2, transform.position, Quaternion.identity);
        if (split1.name == "Entity" || split1.name == "SplitEnemy")
        {
            Temp1.GetComponent<Unit>().patrolPoints = this.unitController.patrolPoints;
            Temp1.GetComponent<Unit>().safeSpot = this.unitController.safeSpot;
        }
        if (split2.name == "Entity" || split2.name == "SplitEnemy")
        {
            Temp2.GetComponent<Unit>().patrolPoints = this.unitController.patrolPoints;
            Temp2.GetComponent<Unit>().safeSpot = this.unitController.safeSpot;
        }
         if (split1.name == "MageEnemy")
        {
            Temp1.GetComponent<mageEnemy>().patrolPoints = this.unitController.patrolPoints;
            Temp1.GetComponent<mageEnemy>().safeSpot = this.unitController.safeSpot;
        }
        if (split2.name == "MageEnemy")
        {
            Temp2.GetComponent<mageEnemy>().patrolPoints = this.unitController.patrolPoints;
            Temp2.GetComponent<mageEnemy>().safeSpot = this.unitController.safeSpot;
        }
        if (split1.name == "DashEnemy")
        {
            Temp1.GetComponent<DashEnemy>().patrolPoints = this.unitController.patrolPoints;
        }
        if (split2.name == "DashEnemy")
        {
            Temp2.GetComponent<DashEnemy>().patrolPoints = this.unitController.patrolPoints;
        }
    }

}
