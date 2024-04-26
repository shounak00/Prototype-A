using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public List<RectTransform> cardPosition = new List<RectTransform>();
    public List<Card> flippedCards = new List<Card>();
    public Sprite[] images;
    public GameObject gameBoard;
    
    public List<GameObject> cards = new List<GameObject>();
    public void SetupCards()
    {
        SpawnCards();
        ShuffleCards();
    }

    public void ResetCardLists()
    {
        foreach (GameObject card in cards)
        {
            Destroy(card);
        }
        cards.Clear();
    }

    void SpawnCards()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardPosition[i], true);
            newCard.transform.localPosition = Vector3.zero;
            newCard.transform.localScale= Vector3.one;
            cards.Add(newCard);

            
            Image cardImage = newCard.GetComponent<Image>();
            if (cardImage != null)
            {
                int imageIndex = i % images.Length;
                cardImage.sprite = images[imageIndex];
                newCard.GetComponent<Card>().image = cardImage.sprite;
                newCard.GetComponent<Card>().name = images[imageIndex].name;
            }
        }

        int initialCount = cards.Count;
        for (int i = 0; i < initialCount; i++)
        {
            GameObject duplicateCard = Instantiate(cards[i], cardPosition[i + initialCount], true);
            duplicateCard.transform.localPosition = Vector3.zero;
            duplicateCard.transform.localScale= Vector3.one;
            cards.Add(duplicateCard);
        }
    }


    public void FlipCard(Card card)
    {
        if (flippedCards.Count < 2)
        {
            card.Flip();
        }
        
        if (card.isFlipped)
        {
            flippedCards.Add(card);
        }
        else
        {
            flippedCards.Remove(card);
        }
    }
    

    private void ShuffleCards()
    {
        
        List<Transform> cardParents = new List<Transform>();

        // Get the parent transforms of all cards
        foreach (GameObject card in cards)
        {
            cardParents.Add(card.transform.parent);
        }

        // Shuffle the parent transforms
        for (int i = 0; i < cardParents.Count; i++)
        {
            int randomIndex = Random.Range(i, cardParents.Count);
            (cardParents[i], cardParents[randomIndex]) = (cardParents[randomIndex], cardParents[i]);
        }
        
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.SetParent(cardParents[i]);
            cards[i].transform.localPosition = Vector3.zero;
            cards[i].transform.localScale = Vector3.one;
        }
    }
}

