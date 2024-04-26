using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveGame(int matches, int turns)
    {
        PlayerPrefs.SetInt("Matches", matches);
        PlayerPrefs.SetInt("Turns", turns);
        Debug.Log("Game saved!");
    }

    public void LoadGame()
    {
        int matches = PlayerPrefs.GetInt("Matches");
        int turns = PlayerPrefs.GetInt("Turns");
        Debug.Log("Game loaded! Matches: " + matches + ", Turns: " + turns);
    }
}

