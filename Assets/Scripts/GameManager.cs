/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Harrison Weber
// Creation Date :     October 10th, 2023
//
// Brief Description : Controls many interactions in the game and keeps track of global variables.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerPos;
    public Animator canvasAnim;
    public int keyAmount;
    public GameObject[] endgameObjects;





    public void CheckKeyAmount()
    {
        if (keyAmount == 3)
        {
            Destroy(endgameObjects[0]);
            endgameObjects[1].SetActive(true);
            endgameObjects[2].SetActive(true);
        }
    }
}
