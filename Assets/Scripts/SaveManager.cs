using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    int _matches,_turns;
    
    public string[] cardSlot; 
    public string[] cardName;
    public string[] loadedData;

    public bool dataLoaded;
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Saved",1);
        _matches = GameManager.Instance.matches;
        _turns = GameManager.Instance.turns;
        PlayerPrefs.SetInt("Matches", _matches);
        PlayerPrefs.SetInt("Turns", _turns);
        Debug.Log("Game saved!");
        CardManager tempCardManager = GameManager.Instance.cardManager;
        
        for (int i = 0; i < tempCardManager.cards.Count; i++)
        {
            if (tempCardManager.cards[i] != null)
            {
                PlayerPrefs.SetString("CardData_" + i , tempCardManager.cards[i].GetComponent<Card>().cardSlot+ "|"+ tempCardManager.cards[i].GetComponent<Card>().cardName);
                Debug.Log(PlayerPrefs.GetString("CardData_" + i , tempCardManager.cards[i].GetComponent<Card>().cardSlot+ tempCardManager.cards[i].GetComponent<Card>().cardName));
            }
        }
    }

    public void LoadGame()
    {
        int matches = PlayerPrefs.GetInt("Matches");
        int turns = PlayerPrefs.GetInt("Turns");
        LoadSavedData();
        Debug.Log("Game loaded! Matches: " + matches + ", Turns: " + turns);
    }
    
    
    public static string[] LoadCardData()
    {
        string[] cardDataArray = new string[16];
        for (int i = 0; i < 16; i++)
        {
            cardDataArray[i] = PlayerPrefs.GetString("CardData_" + i, "");
        }
        return cardDataArray;
    }

    public void LoadSavedData()
    {
        loadedData = LoadCardData();
        
        // Clear existing arrays or initialize if not done yet
        cardSlot = new string[16];
        cardName = new string[16];

        // Split the loaded data and assign to cardSlot and cardName arrays
        for (int i = 0; i < 16; i++)
        {
            string[] parts = loadedData[i].Split('|');
            if (parts.Length >= 2)
            {
                cardSlot[i] = parts[0];
                cardName[i] = parts[1];
            }
            else
            {
                // Handle if the loaded string doesn't have the correct format
                Debug.LogWarning("Invalid data format for CardData_" + i);
            }
        }

        dataLoaded = true;
    }
}

