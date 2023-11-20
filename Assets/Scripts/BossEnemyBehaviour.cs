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
    public Transform[] shotgunPos;
    public GameObject bulletPrefabRed;
    public GameObject bulletPrefabGray;
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
    public GameObject popEffect;
    private GameManager gameManager;
    private AudioSource hurtSFX;
    public AudioSource deathSFX;
    public AudioSource RoarSFX;
    public AudioSource laughSFX;
    public AudioSource shootSFX;
    public AudioSource shotgunSFX;
    public AudioSource shotgunReloadSFX;
    public AudioSource popSFX;

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
        hurtSFX = GetComponent<AudioSource>();
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
        if (bossHealth <= 90 && bossHealth > 65)
        {
            anim.SetFloat("betweenTime", 1.25f);
            anim.SetFloat("stunTime", 1.25f);
        }
        if (bossHealth <= 65 && bossHealth > 40)
        {
            anim.SetFloat("betweenTime", 2f);
            anim.SetFloat("stunTime", 1.50f);
        }
        if (bossHealth <= 40 && bossHealth > 20)
        {
            anim.SetFloat("betweenTime", 2.5f);
            anim.SetFloat("stunTime", 1.75f);
        }
        if (bossHealth <= 20 && bossHealth > 0)
        {
            //final phase
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
        laughSFX.Play();
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
            TakeDamage(3f);
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
            Instantiate(bulletPrefabGray, shootPos0.position, transform.rotation);
            Instantiate(bulletPrefabGray, shootPos1.position, transform.rotation);
        }
        yield return new WaitForSeconds(shootTimer);

        isShooting = false;
    }

    public void ShotgunReload()
    {
        shotgunReloadSFX.Play();
    }
    public void ShotgunShot()
    {
      shotgunSFX.Play();
      Instantiate(bulletPrefabRed, shotgunPos[0].position, shotgunPos[0].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[1].position, shotgunPos[1].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[2].position, shotgunPos[2].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[3].position, shotgunPos[3].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[4].position, shotgunPos[4].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[5].position, shotgunPos[5].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[6].position, shotgunPos[6].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[7].position, shotgunPos[7].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[8].position, shotgunPos[8].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[9].position, shotgunPos[9].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[10].position, shotgunPos[10].rotation);
      Instantiate(bulletPrefabRed, shotgunPos[11].position, shotgunPos[11].rotation);
    }


    public void TakeDamage(float damageAmount)
    {
        anim.SetTrigger("hurt");
        hurtSFX.Play();
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
        attackState = Random.Range(1, 4);
    }
    public void ReturnToIdleState()
    {
        Debug.Log("Returning to idle state...");
        shootingAllowed = false;
        shootSFX.Stop();
        attackState = 0;
    }
    public void AllowShooting()
    {
        shootingAllowed = true;
        shootSFX.Play();
    }
    public void DisallowShooting()
    {
        shootingAllowed = false;
    }
    public void Roar()
    {
        RoarSFX.Play();
        gameManager.bgMusic.Stop();
        gameManager.bossMusic.SetActive(true);
    }
    public void ActivateDeath()
    {
        RoarSFX.pitch = 0.77f;
        RoarSFX.Play();
        shootSFX.Stop();
        shootingAllowed = false;
        attackState = 3;
        Debug.Log("Boss Defeated!");
    }
    public void EndGame()
    {
        popSFX.Play();
        RoarSFX.Stop();
        Instantiate(popEffect, gameObject.transform.position, umbrella.gameObject.transform.rotation);
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