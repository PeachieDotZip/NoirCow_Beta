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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
