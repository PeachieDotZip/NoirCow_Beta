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
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject mainMenu;
    public GameObject backButton;
    public GameObject controlText;
    public GameObject howToPlayButton;

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

    public void howToPlay()
    {
        restartButton.SetActive(false);
        exitButton.SetActive(false);
        mainMenu.SetActive(false);
        howToPlayButton.SetActive(false);

        backButton.SetActive(true);
        controlText.SetActive(true);
    }

    public void back()
    {
        restartButton.SetActive(true);
        exitButton.SetActive(true);
        mainMenu.SetActive(true);
        howToPlayButton.SetActive(true);

        backButton.SetActive(false);
        controlText.SetActive(false);
    }
}
