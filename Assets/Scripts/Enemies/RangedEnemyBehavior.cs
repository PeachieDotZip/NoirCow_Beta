/*****************************************************************************
// File Name :         RangedEnemyBehavior.cs
// Author :            Lorien Nergard
// Creation Date :     October 11th, 2023
//
// Brief Description : Controls the ranged enemy
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehavior : MonoBehaviour
{
    private float disToPlayer;
    public float range;
    CowController player;

    public Transform shootPos;
    public GameObject bulletPrefab;
    public float shootTimer;
    public bool isShooting;
    private AudioSource hurtSFX;
    public AudioSource shootSFX;

    public float enemyHealth = 3;

    private Animator anim;

    /// <summary>
    /// Grabs the player and sets health
    /// </summary>
    void Start()
    {
        isShooting = false;
        player = FindObjectOfType<CowController>();
        anim = GetComponent<Animator>();
        hurtSFX = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Starts & stops the coroutine
    /// </summary>
    void Update()
    {
        disToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (disToPlayer <= range & isShooting == false)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            StopCoroutine(Shoot());
        }

        if (enemyHealth <= 0)
        {
            //instantiate death vfx
            anim.SetBool("isDead", true);
        }
    }

    /// <summary>
    /// Coroutine to instantiate the bullets
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        isShooting = true;
        anim.SetTrigger("shoot");
        yield return new WaitForSeconds(shootTimer);
        anim.ResetTrigger("shoot");
        isShooting = false;
    }
    public void PlayShootSFX()
    {
        shootSFX.pitch = Random.Range(0.85f, 1.15f);
        shootSFX.Play();
    }

    public void TakeDamage(float damageAmount)
    {
        anim.SetTrigger("hurt");
        hurtSFX.Play();
        enemyHealth -= damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet_Ricochet") || (collision.gameObject.CompareTag("Bullet")))
        {
            TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Bullet_Bash"))
        {
            TakeDamage(3);
        }
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    public void Fire()
    {
        Instantiate(bulletPrefab, shootPos.position, transform.rotation);
    }
    /// <summary>
    /// Draws the range so it can be easily tested in the scene view.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}