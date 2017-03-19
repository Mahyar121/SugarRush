using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMob : MonoBehaviour {

    [SerializeField] private List<string> damageSources;
    [SerializeField] private Stats healthStat;
    [SerializeField] private EdgeCollider2D meleeAttackCollider;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float meleeRange = 50f;
    [SerializeField] private float movementSpeed;

    private IMeleeMob currentState;
    private Canvas healthCanvas;
    private SpriteRenderer spriteRenderer;
    private bool facingRight;

    public GameObject Target { get; set; }
    public Rigidbody2D MyRigidbody { get; set; }
    public Animator MyAnimator { get; set; }
    public EdgeCollider2D MeleeAttackCollider { get { return meleeAttackCollider; } }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    public bool IsDead { get { return healthStat.CurrentHp <= 0; } }
    public bool InMeleeRange
    {
        get
        {
            if (Target != null) { return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange; }
            return false;
        }
    }

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        if (!IsDead)
        {
            if(!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (damageSources.Contains(collider.tag)) { StartCoroutine(TakeDamage()); }
        currentState.OnTriggerEnter(collider);
    }

    private void Initialization()
    {
        facingRight = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyAnimator = GetComponent<Animator>();
        healthStat.Initialize();
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new MeleeMobIdleState());
        healthCanvas = transform.GetComponentInChildren<Canvas>();
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDirection = Target.transform.position.x - transform.position.x;
            if (xDirection > 0 && !facingRight || xDirection < 0 && facingRight) { ChangeDirection(); }
        }
    }

    public IEnumerator TakeDamage()
    {
        healthStat.CurrentHp -= 10;
        if(!IsDead)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        else
        {
            MyAnimator.SetTrigger("death");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(3);
            Death();
            yield return null;
        }
    }

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new MeleeMobPatrolState());
    }

    public void ChangeState(IMeleeMob newState)
    {
        if(currentState != null) { currentState.Exit(); }
        currentState = newState;
        currentState.Enter(this);
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void Move()
    { 
        if (!Attack)
        {
            // Checks what direction the mob is facing, and moves the mob that direction as long as the mob is not at the edge of the patrol designation
            if((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);
                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            // if the mob hits the edge of the patrol designation, flip to the other side
            else if (currentState is MeleeMobPatrolState)
            {
                ChangeDirection();
            }
            else if (currentState is MeleeMobAttackState)
            {
                Target = null;
                ChangeState(new MeleeMobIdleState());
            }
        }
    }

    public void Death()
    {
        healthCanvas.enabled = false;
        Destroy(gameObject);
    }

    public void ChangeDirection()
    {
        // save current mob's canvas before the mob changes direction, and then turn it off
        Transform temp = transform.FindChild("MeleeMobCanvas").transform;
        Vector3 after = temp.position;
        temp.SetParent(null);
        // once the mob changes direction
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        // once the mob does change direction, lets turn back on the canvas
        temp.SetParent(transform);
        temp.position = after;
    }




}
