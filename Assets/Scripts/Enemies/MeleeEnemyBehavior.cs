using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb2d;
    Vector2 moveDirection;

    public float chargeDelayTime;
    private float disToPlayer;
    public float range;

    public bool canCharge;
    public bool isCharging;
    public bool isRetreating;

    public float currentSpeed;

    public Transform player;
    public Transform target;

    public float enemyHealth = 3;

    private UmbrellaBehaviour umbrella;


    private Animator anim;
    public GameObject bashedEffect;
    public GameObject deathEffect;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        canCharge = true;
        isCharging = false;
        umbrella = FindObjectOfType<UmbrellaBehaviour>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        disToPlayer = Vector2.Distance(transform.position, player.position);


        if (target == null)
        {
            target = player;
        }

        if (!isCharging && canCharge)
        {

            if (disToPlayer <= range)
            {
                anim.SetTrigger("begin charge");
            }
        }

        if (enemyHealth <= 0)
        {
            //instantiate death vfx
            Destroy(gameObject);
        }
        if (isCharging)
        {
            MoveEnemy();
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        if (isRetreating)
        {
            //MoveEnemy_Retreat();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        anim.SetTrigger("hurt");
        enemyHealth -= damageAmount;
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
    private void MoveEnemyRetreat()
    {
        //transform.position = Vector3.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void HitWall()
    {
        gameObject.tag = "Enemy";
        anim.SetBool("isDazed", true);
        isCharging = false;
        canCharge = false;
        rb2d.velocity = new Vector2 (0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("melee enemy hit a wall");
            HitWall();
            //more cooldown
        }
        if (collision.gameObject.CompareTag("Crate"))
        {
            Debug.Log("melee enemy hit the player or a crate");
            HitWall();
            //less cooldown
        }
        if (collision.collider.CompareTag("Player") & umbrella.isBashing == false & !collision.collider.CompareTag(("Umbrella")))
        {
            Debug.Log("melee enemy hit the player or a crate");
            HitWall();
            //less cooldown
        }

        if (collision.collider.CompareTag("Umbrella"))
        {
            Debug.Log("melee enemy hit umbrella");

            if (isCharging == true && umbrella.isBashing == true)
            {
                HitWall();
                Instantiate(bashedEffect, gameObject.transform.position, umbrella.gameObject.transform.rotation);
                //stunned by player
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Umbrella"))
        {
            Debug.Log("melee enemy hit umbrella");

            if (isCharging == true && umbrella.isBashing == true)
            {
                HitWall();
                Instantiate(bashedEffect, gameObject.transform.position, umbrella.gameObject.transform.rotation);
                //stunned by player
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Umbrella"))
        {
            if (isCharging == false && umbrella.isPoking == true)
            {
                TakeDamage(1);
                Instantiate(deathEffect, gameObject.transform.position, umbrella.gameObject.transform.rotation);
            }
        }
        if (collision.gameObject.CompareTag("Bullet_Ricochet") || (collision.gameObject.CompareTag("Bullet")))
        {
            TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Bullet_Bash"))
        {
            TakeDamage(3);
        }
    }

    /// <summary>
    /// Draws the range so it can be easily tested in the scene view.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // the following functions are called within animation events
    
    public void Charge()
    {
        isCharging = true;
        gameObject.tag = "Enemy_Charging";
    }
    public void EndDaze()
    {
        rb2d.velocity = new Vector2(0, 0);
        anim.SetBool("isDazed", false);
        canCharge = true;
    }

}
