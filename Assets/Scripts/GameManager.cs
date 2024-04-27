using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int matches = 0;
    public int turns = 0;
    public int totalMatches = 8;
    public int score = 0;
    public bool gameStarted = false;
    
    [HideInInspector] public CardManager cardManager;
    UIManager uiManager;
    [SerializeField] private SaveManager saveManager;
    
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        cardManager = FindObjectOfType<CardManager>();
        uiManager = FindObjectOfType<UIManager>();
        saveManager = FindObjectOfType<SaveManager>();
    }
    
    public void StartGame()
    {
        gameStarted = true;
        
        if (PlayerPrefs.GetInt("Saved") == 1)
        {
            cardManager.ResetCardLists();
            saveManager.LoadGame();
        }

        else
        {
            turns = 0;
            matches = 0;
            score = 0;
        }
        
        
        uiManager.startPopOff();
        cardManager.SetupCards();
        
        Debug.Log("Game started!");
    }

    public void EndGame()
    {
        gameStarted = false;
        cardManager.ResetCardLists();
        
        uiManager.FinalPopUp();
        SoundManager.Instance.PlayGameEndClip();
        PlayerPrefs.SetInt("Saved",0);
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
            score += 10;
            
            //Todo: add vfx and sfx here
            SoundManager.Instance.PlaySound(SoundManager.Instance.matchingSound);
            
            Destroy(card1.gameObject);
            Destroy(card2.gameObject);
            cardManager.flippedCards.Clear();
            Debug.Log("Match found!");
        }
        if(card1.image != card2.image)
        {
            card1.FlipBack();
            card2.FlipBack();
            
            score--;
            
            //Todo: add vfx and sfx here
            SoundManager.Instance.PlaySound(SoundManager.Instance.mismatchingSound);
            
            cardManager.flippedCards.Clear();
            Debug.Log("No match!");
        }

        turns++;
        
        //Update Score here
        uiManager.UpdateText();

        if (matches == totalMatches)
        {
            EndGame();
        }
    }
}

