using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMobIdleState : IMeleeMob {

    private MeleeMob mob;
    private float idleTimer;
    private float idleDuration;

    public void Enter(MeleeMob mob)
    {
        idleDuration = UnityEngine.Random.Range(1, 10);  // using UnityEngine's random function
        this.mob = mob;
    }

    public void Execute()
    {
        Idle();
        if (mob.Target != null) { mob.ChangeState(new MeleeMobPatrolState()); }
    }

    public void Exit()
    {
        return;
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        return;
    }

    private void Idle()
    {
        mob.MyAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration) { mob.ChangeState(new MeleeMobPatrolState()); }
    }
}
