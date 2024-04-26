using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int matches = 0;
    public int turns = 0;
    public int totalMatches = 8;
    public bool gameStarted = false;
    
    [SerializeField] public CardManager cardManager;
    
    
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

    public void CountFlippedCards()
    {
        if (cardManager.flippedCards.Count == 2)
        {
            StartCoroutine(delayToShow());
        }
    }

    IEnumerator delayToShow()
    {
        yield return new WaitForSeconds(0.5f);
        CheckMatch(cardManager.flippedCards[0], cardManager.flippedCards[1]);
    }

    public void CheckMatch(Card card1, Card card2)
    {
        if (card1.image == card2.image)
        {
            matches++;
            
            //Todo: add vfx and sfx here
            Destroy(card1.gameObject);
            Destroy(card2.gameObject);
            cardManager.flippedCards.Clear();
            Debug.Log("Match found!");
        }
        if(card1.image != card2.image)
        {
            card1.FlipBack();
            card2.FlipBack();
            
            cardManager.flippedCards.Clear();
            Debug.Log("No match!");
        }

        turns++;

        if (matches == totalMatches)
        {
            EndGame();
        }
    }
}

