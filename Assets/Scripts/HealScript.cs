/*****************************************************************************
// File Name :         HealScript.cs
// Author :            Harrison Weber
// Creation Date :     October 31st, 2023
//
// Brief Description : Manages what happens when the player touches a heal object and how to heal the player.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    private CowHealthBehavior cowHealth;
    private Animator anim;
    public AudioSource healSFX;
    public AudioSource grabSFX;

    // Start is called before the first frame update
    void Start()
    {
        cowHealth = FindObjectOfType<CowHealthBehavior>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("touched");
            healSFX.Play();
        }
    }
    public void HealPlayer()
    {
        Debug.Log("Player Healed!");
        cowHealth.playerLives += 1;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
