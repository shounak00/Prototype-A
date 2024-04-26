using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    public Card[] cards;
    private List<Card> flippedCards = new List<Card>();

    public void SetupCards()
    {
        // Shuffle the cards array
        ShuffleCards();

        foreach (Card card in cards)
        {
            // Logic to instantiate and arrange cards
        }
    }

    public void FlipCard(Card card)
    {
        card.Flip();
        if (card.isFlipped)
        {
            flippedCards.Add(card);
            if (flippedCards.Count == 2)
            {
                GameManager.Instance.CheckMatch(flippedCards[0], flippedCards[1]);
                flippedCards.Clear();
            }
        }
        else
        {
            flippedCards.Remove(card);
        }
    }

    private void ShuffleCards()
    {
        // Fisher-Yates shuffle algorithm
        for (int i = cards.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (cards[i], cards[randomIndex]) = (cards[randomIndex], cards[i]);
        }
    }
}

