using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int matches = 0;
    private int turns = 0;
    private int totalMatches = 8;
    public bool gameStarted = false;
    
    [SerializeField] private CardManager cardManager;
    
    
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        cardManager = FindObjectOfType<CardManager>();
    }
    
    public void StartGame()
    {
        gameStarted = true;
        cardManager.SetupCards();
        
        Debug.Log("Game started!");
    }

    public void EndGame()
    {
        gameStarted = false;
        cardManager.ResetCardLists();
        Debug.Log("Game ended!");
    }

    public void CheckMatch(Card card1, Card card2)
    {
        if (card1.image == card2.image)
        {
            card1.isMatched = true;
            card2.isMatched = true;
            matches++;
            Debug.Log("Match found!");
        }
        else
        {
            card1.FlipBack();
            card2.FlipBack();
            Debug.Log("No match!");
        }

        turns++;

        if (matches == totalMatches)
        {
            EndGame();
        }
    }
}

