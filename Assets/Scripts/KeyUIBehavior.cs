/*****************************************************************************
// File Name :         KeyUIBehaviour.cs
// Author :            Lorien Nergard
// Creation Date :     Novemeber 10th, 2023
//
// Brief Description : Handles how the keys are displayed on the HUD
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIBehavior : MonoBehaviour
{
    public GameObject KeyOne;
    public GameObject KeyTwo;
    public GameObject KeyThree;

    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;

    private GameManager gameManager;
    private CowHealthBehavior cowHealth;

    // Start is called before the first frame update
    void Start()
    {
        KeyThree.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        KeyTwo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        KeyOne.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);

        heartOne.SetActive(true);
        heartTwo.SetActive(true);
        heartThree.SetActive(true);
        gameManager = FindObjectOfType<GameManager>();
        cowHealth = FindObjectOfType<CowHealthBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.keyAmount == 1)
        {
            KeyOne.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else if (gameManager.keyAmount == 2)
        {
            KeyTwo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else if (gameManager.keyAmount == 3)
        {
            KeyThree.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }


        if (cowHealth.playerLives == 3)
        {
            heartOne.SetActive(true);
            heartTwo.SetActive(true);
            heartThree.SetActive(true);
        }
        else if (cowHealth.playerLives == 2)
        {
            Debug.Log("goober");
            heartOne.SetActive(true);
            heartTwo.SetActive(true);
            heartThree.SetActive(false);
        }
        else if (cowHealth.playerLives == 1)
        {
            heartOne.SetActive(true);
            heartTwo.SetActive(false);
            heartThree.SetActive(false);
        }
        else if (cowHealth.playerLives == 0)
        {
            heartOne.SetActive(false);
            heartTwo.SetActive(false);
            heartThree.SetActive(false);
        }
    }
}
