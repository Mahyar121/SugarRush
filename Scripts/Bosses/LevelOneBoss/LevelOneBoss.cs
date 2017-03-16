using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelOneBoss : MonoBehaviour {

    [SerializeField] private List<string> damageSources;
    [SerializeField] private Stats healthStat;
    [SerializeField] private float movementSpeed;

    private Vector3 startPosition;
    private Player player;
    private Text distance;
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

    private void FixedUpdate()
    {
        if (!IsDead)
        {
            if (SceneManager.GetActiveScene().buildIndex == 8) // LEVEL 1 BOSS
            {
                if (player.transform.position.x >= transform.position.x) { Move(); }
                else { ResetPosition(); }
            }
            else if (SceneManager.GetActiveScene().buildIndex == 12) // LEVEL 2 BOSS
            {
                if (player.transform.position.y <= transform.position.y) { Move(); }
                else { ResetPosition(); }
            }
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
        if(SceneManager.GetActiveScene().buildIndex == 12)
        {
            distance = GameObject.Find("distance value").GetComponent<Text>();
        }
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
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            
            if (!IsPlayerOnFlag) { transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f); }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            distance.text = (Mathf.RoundToInt(transform.position.y - player.transform.position.y)).ToString();
            if (transform.position.y >= -30f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, -450f, 0f), (1f * movementSpeed * Time.deltaTime));
            }
            else if (transform.position.y >= -90f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, -450f, 0f), (1.6f * movementSpeed * Time.deltaTime));
            }
            else if (transform.position.y >= -120f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, -450f, 0f), (1.9f * movementSpeed * Time.deltaTime));
            }
            else if (transform.position.y >= -160f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, -450f, 0f), (2.1f * movementSpeed * Time.deltaTime));
            }
            else if (transform.position.y >= -400f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, -450f, 0f), (2.2f * movementSpeed * Time.deltaTime));
            }
            else if (transform.position.y >= -500f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, -450f, 0f), (2.3f * movementSpeed * Time.deltaTime));
            }

        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }



}
