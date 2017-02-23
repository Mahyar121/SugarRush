using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class Player : MonoBehaviour {

    public event DeadEventHandler Dead;

    // SerializeField lets the game designer edit values in the editor
    [SerializeField] private Stats healthStat;
    [SerializeField] private Text textLives;
    [SerializeField] private EdgeCollider2D swordCollider;
    [SerializeField] private Transform[] groundPoints;
    [SerializeField] private List<string> damageSources;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundRadius;
    [SerializeField] private float movementspeed;
    [SerializeField] private float immortalTime;
    [SerializeField] private float YFallingPoint;
    [SerializeField] private float jumpForce;
    [SerializeField] private float climbSpeed;
    

    // private variables
    private Vector3 startPos;
    private SpriteRenderer spriteRenderer;
    private IUseable useable;
    private Transform currentLocation;
    private bool isDead;
    private bool immortal = false;
    private bool facingRight;
    private int lives = 3;
    private static Player instance;

    // Properties
    public Rigidbody2D MyRigidbody { get; set; }
    public Animator MyAnimator { get; set; }
    public EdgeCollider2D SwordCollider { get { return swordCollider; } }
    public bool Immortal { get { return immortal; } set { this.immortal = value; } }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    public bool Jump { get; set; }
    public bool OnStripes { get; set; }
    public bool OnHeart { get; set; }
    public bool OnLadder { get; set; }
    public bool OnGround { get; set; }
    public bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0) { OnDead(); }
            return healthStat.CurrentVal <= 0;
        }
    }
    public static Player Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<Player>(); }
            return instance;
        }
    }

    // this gets called before Start Function
    private void Awake()
    {
        healthStat.Initialize();
    }

    // Use this for initialization
    private void Start()
    {
        InitializingStartValues();

    }
	
	// Update is called once per frame
	private void Update()
    {
        FallingOffMapHandler();
        HandleInput();
        
	}

    // better FPS for physics
    private void FixedUpdate()
    {
        MovingFlippingPlayer();
    }

    private void InitializingStartValues()
    {
        // Get Component finds a componenent on the Player
        MyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        textLives.text = lives.ToString();
        facingRight = true;
        OnHeart = false;
        OnLadder = false;
        OnStripes = false;
       
        startPos = transform.position;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !OnLadder)
        {
            MyAnimator.SetTrigger("jump");
            Jump = true;
        }
        if (Input.GetKeyDown(KeyCode.Z)) { MyAnimator.SetTrigger("attack"); }
        if (Input.GetKeyDown(KeyCode.X)) { Use(); }
        if (Input.GetKeyDown(KeyCode.O)) { healthStat.CurrentVal -= 10; }
        if (Input.GetKeyDown(KeyCode.P)) { healthStat.CurrentVal += 10; }

    }

    private void MovingFlippingPlayer()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            OnGround = IsGrounded();
            HandleMovement(horizontal, vertical);
            Flip(horizontal);
        }
    }

    private void OnDead()
    {
        if (Dead != null) { Dead(); }
    }

    private void Death()
    {
        lives--;
        textLives.text = lives.ToString();
        if (lives > 0)
        {
            Jump = false;
            MyRigidbody.velocity = Vector3.zero;
            MyAnimator.SetTrigger("idle");
            healthStat.CurrentVal = healthStat.MaxVal;
            transform.position = startPos;
        }
        else // load game over page :(  (T-T)
        {
            SceneManager.LoadScene(10);
        }
    }

    private IEnumerator TakeDamage()
    {
        if(healthStat.CurrentVal > 0 && !immortal)
        {
            healthStat.CurrentVal -= 10;
            if(!IsDead)
            {
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else if (healthStat.CurrentVal <= 0)
            {
                MyAnimator.SetTrigger("dead");
                Death();
            }
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    private void Use()
    {
        if(useable != null) { useable.Use(); }
    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight) { ChangeDirection(); }
    }

    private void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void FallingOffMapHandler()
    {
        if(!TakingDamage && !IsDead)
        {
            Vector3 fallingLimit = new Vector3(0, YFallingPoint, 0);
            if(transform.position.y <= fallingLimit.y) { Death(); }
        }
    }

    private void HandleMovement(float horizontal, float vertical)
    {
        if (MyRigidbody.velocity.y < 0) { MyAnimator.SetBool("land", true); }
        if(!Attack && !OnStripes) { MyRigidbody.velocity = new Vector3(horizontal * movementspeed, MyRigidbody.velocity.y, 0); }
        if (!Attack && OnStripes) { MyRigidbody.velocity = new Vector3(horizontal * movementspeed * 3.2f, MyRigidbody.velocity.y, 0); }
        if (Jump && MyRigidbody.velocity.y == 0 && !OnLadder) { MyRigidbody.AddForce(new Vector3(0, jumpForce, 0)); }
        if (OnLadder)
        {
            MyAnimator.speed = vertical != 0 ? Mathf.Abs(vertical) : Mathf.Abs(horizontal);
            MyRigidbody.velocity = new Vector3(horizontal * climbSpeed, vertical * climbSpeed, 0);
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0) // Player is not in the air
        {
            foreach (Transform point in groundPoints) // search through players ground points
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i=0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Useable") { useable = collider.GetComponent<IUseable>(); }
        if (collider.tag == "Heart"){ healthStat.CurrentVal = healthStat.MaxVal; }
        if (collider.tag == "CheckPoint") { startPos = collider.transform.position; }
        if (collider.tag == "Stripes") { OnStripes = true; }
        if (collider.tag == "Bouncy") { MyRigidbody.AddForce(new Vector3(0, 700, 0)); }
        if (damageSources.Contains(collider.tag)) { StartCoroutine(TakeDamage()); }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Useable") { useable = null; }
        if (collider.tag == "Stripes") { OnStripes = false; }
    }

}
