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
    public GameObject optionsScreen;
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject exitButton;
    public GameObject backButton;
    public GameObject controlText;

    private void Start()
    {
        optionsScreen.SetActive(false);
        backButton.SetActive(false);
        controlText.SetActive(false);
    }


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

    /// <summary>
    /// Opens the options menu
    /// </summary>
    public void options()
    {
        playButton.SetActive(false);
        optionsButton.SetActive(false);
        exitButton.SetActive(false);

        optionsScreen.SetActive(true);
        backButton.SetActive(true);
        controlText.SetActive(true);
    }

    public void back()
    {
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        exitButton.SetActive(true);

        optionsScreen.SetActive(false);
        backButton.SetActive(false);
        controlText.SetActive(false);
    }
}
