using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelThreeBoss : MonoBehaviour {

    [SerializeField] private Stats healthStat;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private Vector3 startPosition;
    private bool facingRight;
    private float time;
    private static LevelThreeBoss instance;
  
    public Stats HealthStat { get { return healthStat; } }
    public Animator MyAnimator { get; set; }
    public static LevelThreeBoss Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<LevelThreeBoss>(); }
            return instance;
        }
    }


    // Use this for initialization
    private void Start ()
    {
        Initialization();
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (IsDead())
        {
            SceneManager.LoadScene(4);
        }
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Edge") { ChangeDirection(); }
    }

    private void Initialization()
    {
        time = 0f;
        startPosition = transform.position;
        facingRight = false;
        MyAnimator = GetComponent<Animator>();
        healthStat.Initialize();
    }
          
    private void ChangeDirection()
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
    
    private bool IsDead()
    {
        return healthStat.CurrentHp <= 0f ? true : false;
    }
}
