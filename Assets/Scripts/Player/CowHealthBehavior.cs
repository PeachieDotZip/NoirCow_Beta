/*****************************************************************************
// File Name :         CowHealthBehavior.cs
// Author :            Lorien Nergard
// Creation Date :     October 16th, 2023
//
// Brief Description : Controls the health, lives, and respawn of the cow
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CowHealthBehavior : MonoBehaviour
{
    public float playerLives;
    public bool isInvulnerable;
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject mainMenu;
    public UmbrellaBehaviour umbrella;
    public Animator cowAnim;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        playerLives = 3;
        restartButton.SetActive(false);
        exitButton.SetActive(false);
        mainMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLives > 0)
        {
            livesText.text = "Lives : " + playerLives.ToString();
        }      
        else 
        {
            restartButton.SetActive(true);
            exitButton.SetActive(true);
            mainMenu.SetActive(true);

            Destroy(gameObject);

            livesText.text = "Lives : 0";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && umbrella.isBashing == false)
        {
            TakeDamage(1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Enemy_Charging")) && umbrella.isBashing == false)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isInvulnerable == false)
        {
            cowAnim.SetTrigger("hurt");
            playerLives -= damageAmount;
            Debug.Log("Cow Noir took damage!");
        }
    }
}
