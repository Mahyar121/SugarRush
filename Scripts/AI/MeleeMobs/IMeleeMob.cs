using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeMob {

    void Execute();
    void Enter(MeleeMob mob);
    void Exit();
    void OnTriggerEnter(Collider2D collider);
	
}
