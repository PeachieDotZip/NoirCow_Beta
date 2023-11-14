/*****************************************************************************
// File Name :         MainMenur.cs
// Author :            Lorien Nergard
// Creation Date :     October 16th, 2023
//
// Brief Description : Controls the buttons on the main menu
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Quits the game
    /// </summary>
    public void exitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Goes to the first playable
    /// </summary>
    public void playGame()
    {
        SceneManager.LoadScene("First Playable Scene");
    }
}
