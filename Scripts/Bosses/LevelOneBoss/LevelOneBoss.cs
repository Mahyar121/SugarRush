using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBoss : MonoBehaviour {

    [SerializeField] private List<string> damageSources;
    [SerializeField] private Stats healthStat;
    [SerializeField] private float movementSpeed;

    private Vector3 startPosition;
    private Player player;
    private static LevelOneBoss instance;

    public Animator MyAnimator { get; set; }
    public bool IsPlayerOnFlag { get; set; }
    public bool TakingDamage { get; set; }
    public bool IsDead { get { return healthStat.CurrentHp <= 0; } }

    public static LevelOneBoss Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<LevelOneBoss>(); }
            return instance;
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
            if (player.transform.position.x >= transform.position.x) { Move(); }
            else { ResetPosition(); }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (damageSources.Contains(collider.tag)) { StartCoroutine(TakeDamage()); }
        if (collider.tag == "Player") { ResetPosition(); }
    }

    private void Initialization()
    {
        player = FindObjectOfType<Player>();
        startPosition = transform.position;
        MyAnimator = GetComponent<Animator>();
        IsPlayerOnFlag = false;
        healthStat.Initialize();   
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }


    public IEnumerator TakeDamage()
    {
            MyAnimator.SetTrigger("death");
            yield return new WaitForSeconds(3);
            Death();
            yield return null;
    }


    public void Move()
    {
        if (!IsPlayerOnFlag) { transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f); }
    }

    public void Death()
    {
        Destroy(gameObject);
    }



}
