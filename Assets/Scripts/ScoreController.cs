using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public float currentScore;
    public TextMeshProUGUI scoreUI;

    private void Start()
    {
        currentScore = 1000;
    }

    private void Update()
    {
        scoreUI.text = "Score : " + currentScore.ToString();
    }

    public void AddScore (int scoreDecrease)
    {
        Debug.Log(currentScore);
        currentScore -= scoreDecrease;
    }

    
}
