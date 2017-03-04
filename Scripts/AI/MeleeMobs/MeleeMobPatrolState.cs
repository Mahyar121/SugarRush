using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMobPatrolState : IMeleeMob
{
    private MeleeMob mob;
    private float patrolTimer;
    private float patrolDuration;


    public void Enter(MeleeMob mob)
    {
        patrolDuration = UnityEngine.Random.Range(1, 10);
        this.mob = mob;
    }

    public void Execute()
    {
        Patrol();
        mob.Move();
        if (mob.Target != null && mob.InMeleeRange) { mob.ChangeState(new MeleeMobAttackState()); }
    }

    public void Exit()
    {
        return;
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        if (collider.tag == "Player") { mob.Target = Player.Instance.gameObject; }
        if (collider.tag == "Edge") { mob.ChangeDirection(); }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration) { mob.ChangeState(new MeleeMobPatrolState()); }
    }
}
