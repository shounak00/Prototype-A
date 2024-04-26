using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public string[] cardSlot; 
    public string[] cardName;
    public string[] loadedData;
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
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

