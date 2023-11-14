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

    public int key;

    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;

    public int playerLives;

    //private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        KeyThree.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        KeyTwo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        KeyOne.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);

        heartOne.SetActive(true);
        heartTwo.SetActive(true);
        heartThree.SetActive(true);
        playerLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (key == 1)//gameManager.keyAmount == 1)
        {
            KeyOne.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else if (key == 2)//gameManager.keyAmount == 2)
        {
            KeyTwo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else if (key == 3)//gameManager.keyAmount == 3)
        {
            KeyThree.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }


        if (playerLives == 3)
        {
            heartOne.SetActive(true);
            heartTwo.SetActive(true);
            heartThree.SetActive(true);
        }
        else if (playerLives == 2)
        {
            heartOne.SetActive(true);
            heartTwo.SetActive(true);
            heartThree.SetActive(false);
        }
        else if (playerLives == 1)
        {
            heartOne.SetActive(true);
            heartTwo.SetActive(false);
            heartThree.SetActive(false);
        }
        else if (playerLives == 0)
        {
            heartOne.SetActive(false);
            heartTwo.SetActive(false);
            heartThree.SetActive(false);
        }
    }
}
