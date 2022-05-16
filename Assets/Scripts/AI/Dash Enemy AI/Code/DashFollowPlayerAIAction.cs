using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashFollowPlayerAIAction", menuName = "AI/Dash/Follow Player AI Action")]
public class DashFollowPlayerAIAction : DashAIAction
{
    [Min(0f)]
    [SerializeField]
    private float followDistance = 10f;

    [Min(0f)]
    [SerializeField]
    private float dashDistance = 8f;

    private Vector3 dashTarget;
    private float dashCooldown;
    [Min(0f)]
    [SerializeField]
    public float timeBetweenDashes = 0.5f;
    [Min(0f)]
    [SerializeField]
    public float dashSpeed = 16f;
    private bool isDashing = false;
    private float dashTime;
    [Min(0f)]
    [SerializeField]
    public float setDashTime = 1f;
    private float waitTime;
    [Min(0f)]
    [SerializeField]
    public float setWaitTime = 0.7f;

    [SerializeField]
    private DashAIState playerMissingState;

    public override void UpdateActionGizmos(DashAIController controller)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controller.transform.position, followDistance);
    }

    public override void UpdateAction(DashAIController controller)
    {
        var player = GetPlayer(controller);
        if (player == null)
        {
            controller.Target = controller.DashController.patrolPoints[Random.Range(0, controller.DashController.patrolPoints.Length)];
            controller.ChangeState(playerMissingState);
            return;
        }

        var playerPosition = player.transform.position;
        var distance = Vector2.Distance(controller.transform.position, playerPosition);

        if (distance > followDistance)
        {
            controller.Target = controller.DashController.patrolPoints[Random.Range(0, controller.DashController.patrolPoints.Length)];
            controller.ChangeState(playerMissingState);
            return;
        }

        //If the enemy found a player and isn't dashing, move towards the palyer 
        if (!isDashing)
        {
            controller.Target = player.transform;
            controller.DashController.LookAtTarget(controller.Target);
        }

        //if dash cooldown is over and the palyer is in range for the charge, start charging phase
        if (dashCooldown <= 0 && Vector2.Distance(controller.transform.position, player.transform.position) < dashDistance)
        {
            controller.Target = controller.transform;
            isDashing = true;
        }
        //set the point to dash to, so it doesnt heat seek the player
        if (waitTime >= setWaitTime - 0.2) dashTarget = player.transform.position;
        //look at the point to dash to and wait 
        if (dashCooldown <= 0 && isDashing)
        {
            controller.DashController.LookAtTarget(dashTarget);
            waitTime -= Time.deltaTime;
        }
        //after waiting a bit dash
        if (dashTime >= 0 && waitTime <= 0)
        {
            controller.stats.Damage = controller.dashDamage;
            float step = dashSpeed * Time.deltaTime;
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, dashTarget, step);
            dashTime -= Time.deltaTime;
        }
        //finish the dashing phase and reset all the timers
        if (dashTime < 0 && waitTime <= 0)
        {
            isDashing = false;
            dashCooldown = timeBetweenDashes;
            dashTime = setDashTime;
            waitTime = setWaitTime;
        }
        //if not dashing, decrease the dashes cooldown
        if(dashCooldown >= 0)
        {
            controller.stats.Damage = controller.originalDamage;
            dashCooldown -= Time.deltaTime;
        }


    }

    private static MainPlayer GetPlayer(DashAIController controller)
    {
        return controller.Player;
    }
}
