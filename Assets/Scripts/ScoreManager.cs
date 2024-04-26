using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public void CalculateScore(int matches, int turns)
    {
        float tempScore = (float)matches / (float)turns;

        if (tempScore >= 1)
        {
            Debug.Log("Highest score! Congratulations!");
        }
        else
        {
            float scorePercentage = tempScore * 1000;
            Debug.Log("Score: " + scorePercentage);
        }
    }
}

