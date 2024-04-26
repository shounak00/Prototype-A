using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    int _matches,_turns;
    public bool isSaved;
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Saved",1);
        _matches = GameManager.Instance.matches;
        _turns = GameManager.Instance.turns;
        PlayerPrefs.SetInt("Matches", _matches);
        PlayerPrefs.SetInt("Turns", _turns);
        Debug.Log("Game saved!");
        CardManager tempCardManager = GameManager.Instance.cardManager;
    }

    public void LoadGame()
    {
        int matches = PlayerPrefs.GetInt("Matches");
        int turns = PlayerPrefs.GetInt("Turns");
        Debug.Log("Game loaded! Matches: " + matches + ", Turns: " + turns);
    }
}

