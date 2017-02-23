using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMobAttackState : IMeleeMob
{
    private MeleeMob mob;
    private float attackTimer;
    private float attackCoolDown = 2f;
    private bool canAttack = true;

    public void Enter(MeleeMob mob)
    {
        this.mob = mob;
    }

    public void Execute()
    {
        Attack();
        if (mob.Target == null) { mob.ChangeState(new MeleeMobIdleState()); }
    }

    public void Exit()
    {
        return;
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        return;
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }
        if (canAttack)
        {
            canAttack = false;
            mob.MyAnimator.SetTrigger("attack");
        }
    }
}
