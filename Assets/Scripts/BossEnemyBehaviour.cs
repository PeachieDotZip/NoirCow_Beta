/*****************************************************************************
// File Name :         BossEnemyBehavior.cs
// Author :            Harrison Weber
// Creation Date :     October 30th, 2023
//
// Brief Description : Controls all boss behaviors.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBehaviour : MonoBehaviour
{
    private float disToPlayer;
    public float range;
    CowController player;
    private Transform target;
    private UmbrellaBehaviour umbrella;
    public Transform shootPos0;
    public Transform shootPos1;
    public Transform shootPos2;
    public GameObject bulletPrefab;
    public float shootTimer;
    private bool isShooting;
    public bool charging;
    public bool retreating;
    private bool shootingAllowed;
    public int attackState = 0;
    private Animator anim;
    private bool inRadius;
    public float bossHealth = 500;
    public GameObject bashedEffect;
    private GameManager gameManager;

    /// <summary>
    /// Grabs the player and sets health
    /// </summary>
    void Start()
    {
        isShooting = false;
        //enemyHealth = 3;
        player = FindObjectOfType<CowController>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("CowNoir").transform;
        umbrella = FindObjectOfType<UmbrellaBehaviour>();
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Starts & stops the coroutine
    /// </summary>
    void Update()
    {
        disToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (disToPlayer <= range)
        {
            inRadius = true;
        }
        if (inRadius & isShooting == false & attackState == 1)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            StopCoroutine(Shoot());
        }
        if (inRadius & isShooting == false & attackState == 2)
        {
            if (retreating)
            {
                RetreatEnemy();
            }
            if (charging)
            {
                MoveEnemy();
            }
        }
        if (bossHealth <= 0)
        {
            anim.SetBool("dead", true);
        }


        anim.SetInteger("attackState", attackState);
        anim.SetBool("inRadius", inRadius);
    }


    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, target.position, 10f * Time.deltaTime);
    }
    private void RetreatEnemy()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, target.position, -1f * Time.deltaTime);
    }
    public void HoldUp()
    {
        retreating = true;
        charging = false;
        gameObject.tag = "Enemy";
    }
    public void ChargeEm()
    {
        retreating = false;
        charging = true;
        gameObject.tag = "Enemy_Charging";
    }

    public void Stunned()
    {
        //change whatever needed
        retreating = false;
        charging = false;
        gameObject.tag = "Enemy";
    }
    public void SemiStunned()
    {
        retreating = false;
        charging = false;
        gameObject.tag = "Enemy";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Umbrella"))
        {
            if (charging && umbrella.isBashing)
            {
                anim.SetTrigger("stun");
                TakeDamage_Stun(10f);
                Debug.Log("Boss Stunned!");
            }
            if (umbrella.isPoking)
            {
                TakeDamage(1f);
                Debug.Log("Boss got poked!");
            }
        }
        if (collision.gameObject.CompareTag("Bullet_Ricochet") || (collision.gameObject.CompareTag("Bullet")))
        {
            TakeDamage(0.25f);
        }
        if (collision.gameObject.CompareTag("Bullet_Bash"))
        {
            TakeDamage(1f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Umbrella"))
        {
            if (charging && umbrella.isBashing)
            {
                anim.SetTrigger("stun");
                TakeDamage_Stun(10f);
                Debug.Log("Boss Stunned!");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && charging)
        {
            anim.SetTrigger("semistun");
            Debug.Log("Boss semistun!");
        }
        if (collision.collider.CompareTag("Wall") && charging)
        {
            anim.SetTrigger("stun");
            TakeDamage_Stun(5f);
            Debug.Log("Boss Stunned!");
        }
    }

    /// <summary>
    /// Coroutine to instantiate the bullets
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        isShooting = true;
        if (shootingAllowed == true)
        {
            Instantiate(bulletPrefab, shootPos0.position, transform.rotation);
            Instantiate(bulletPrefab, shootPos1.position, transform.rotation);
            //Instantiate(bulletPrefab, shootPos2.position, transform.rotation);
        }
        yield return new WaitForSeconds(shootTimer);

        isShooting = false;
    }



    public void TakeDamage(float damageAmount)
    {
        anim.SetTrigger("hurt");
        bossHealth -= damageAmount;
    }
    public void TakeDamage_Stun(float damageAmount)
    {
        Instantiate(bashedEffect, gameObject.transform.position, umbrella.gameObject.transform.rotation);
        bossHealth -= damageAmount;
    }


    public void JumbleState()
    {
        Debug.Log("Jumbling attack state...");
        attackState = Random.Range(1, 3);
    }
    public void ReturnToIdleState()
    {
        Debug.Log("Returning to idle state...");
        shootingAllowed = false;
        attackState = 0;
    }
    public void AllowShooting()
    {
        shootingAllowed = true;
    }
    public void ActivateDeath()
    {
        shootingAllowed = false;
        attackState = 3;
        Debug.Log("Boss Defeated!");
    }
    public void EndGame()
    {
        Instantiate(bashedEffect, gameObject.transform.position, umbrella.gameObject.transform.rotation);
        gameManager.canvasAnim.SetBool("End game", true);
        //Destroy(gameObject);
    }
    /// <summary>
    /// Draws the range so it can be easily tested in the scene view.
    /// </summary>
    /// 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}