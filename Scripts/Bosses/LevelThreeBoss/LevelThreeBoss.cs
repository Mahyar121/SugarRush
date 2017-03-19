using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelThreeBoss : MonoBehaviour {

    [SerializeField] private Stats healthStat;
    [SerializeField] private float movementSpeed;

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

    private void Initialization()
    {
        time = 0f;
        startPosition = transform.position;
        facingRight = false;
        MyAnimator = GetComponent<Animator>();
        healthStat.Initialize();
    }
          
    private bool IsDead()
    {
        return healthStat.CurrentHp <= 0f ? true : false;
    }
}
