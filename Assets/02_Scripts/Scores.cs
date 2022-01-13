using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestText;

    public static int currentScore;
    public static int bestScore;


    private void Start()
    {

        bestScore = PlayerPrefs.GetInt("Highscore");
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
        if (bestText != null)
        {
            bestText.text = "Best: " + bestScore;
        }

    }

    public static void AddToScore(int addedValue)
    {
        currentScore += addedValue;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("Highscore", bestScore);
        }
    }
}
