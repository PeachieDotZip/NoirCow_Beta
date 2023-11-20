/*****************************************************************************
// File Name :         DeathScreenBehavior.cs
// Author :            Lorien Nergard
// Creation Date :     October 16th, 2023
//
// Brief Description : Controls the buttons that appear when the player dies
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenBehavior : MonoBehaviour
{
    public GameManager gameManager;
    /// <summary>
    /// Quits the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Goes to main menu
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary>
    /// Restarts game
    /// </summary>
    public void Restart()
    {
        switch (gameManager.keyAmount)
        {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 1:
                SceneManager.LoadScene("Checkpoint1");
                break;
            case 2:
                SceneManager.LoadScene("Checkpoint2");
                break;
            case 3:
                SceneManager.LoadScene("Checkpoint3");
                break;
            default:
                Debug.Log("Invalid amount of keys!!!");
                break;
        }
    }
}
