using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IUseable {

    [SerializeField] private Collider2D platformCollider;

	public void Use()
    {
        if (Player.Instance.OnLadder)
        {
            UseLadder(false, 1, 1, false); // stop climbing
        }
        else
        {
            UseLadder(true, 0, 0, true); // start climbing
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, true); // ignore the collision
        }
    }


    private void UseLadder(bool onLadder, int gravity, int animSpeed, bool isClimbing)
    {
        Player.Instance.OnLadder = onLadder;
        Player.Instance.MyRigidbody.gravityScale = gravity;
        Player.Instance.MyAnimator.speed = animSpeed;
        Player.Instance.MyAnimator.SetBool("climbing", isClimbing);
        Player.Instance.MyAnimator.ResetTrigger("jump");
        Player.Instance.Jump = false;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            UseLadder(false, 1, 1, false); // stop climbing
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, false); // ignore the collision
        }
    }
}
