using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public RectTransform[] cardPosition;
    public List<Card> flippedCards = new List<Card>();
    public Sprite[] images;
    public GameObject gameBoard;

    public List<GameObject> cards = new List<GameObject>();

    public SaveManager svManager;

    [SerializeField] private GameObject[] cardsArray;
    public int[] indexArray;

    private void Start()
    {
        svManager = FindObjectOfType<SaveManager>();
    }

    public void SetupCards()
    {
        Debug.Log("Setup Cards");
        SpawnCards();
    }

    public void ResetCardLists()
    {
        foreach (GameObject card in cards)
        {
            Destroy(card);
        }

        cards.Clear();
    }

    void SpawnCardsFromSavedData(string[] cardSlots, string[] cardNames)
    {
        if (cardSlots != null || cardNames != null)
        {
            if (cardSlots != null)
                for (int i = 0; i < cardSlots.Length; i++)
                {
                    GameObject newCard = Instantiate(cardPrefab);
                    newCard.transform.localPosition = Vector3.zero;
                    newCard.transform.localScale = Vector3.one;
                    cards.Add(newCard);

                    Image cardImage = newCard.GetComponent<Image>();
                    if (cardImage != null)
                    {
                        // Find the index of the card name in the images array
                        int imageIndex = Array.FindIndex(images, img => img.name == cardNames[i]);
                        if (imageIndex != -1)
                        {
                            cardImage.sprite = images[imageIndex];
                            newCard.GetComponent<Card>().image = cardImage.sprite;
                            newCard.GetComponent<Card>().name = cardNames[i];
                            newCard.GetComponent<Card>().cardName = cardNames[i];
                        }
                        else
                        {
                            Debug.LogWarning("Card name not found in images array: " + cardNames[i]);
                        }
                    }
                }
        }

        StartCoroutine(waitTillSpawn());
    }


    IEnumerator waitTillSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        FixPositions(svManager.cardSlot);
    }

    void FixPositions(string[] cardSlots)
    {
        indexArray = new int[cardSlots.Length];
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i] != null)
            {
                int posIndex = Array.FindIndex(cardPosition, img => img.name == cardSlots[i]);
                cards[i].transform.SetParent(cardPosition[posIndex]);
                cards[i].transform.localPosition = Vector3.zero;
                cards[i].transform.localScale = Vector3.one;
                cards[i].GetComponent<Card>().cardSlot = cardPosition[posIndex].name;
            }

            else
            {
                Destroy(cards[i]);
            }
            
        }
    }


    void SpawnCards()
    {
        if (svManager.dataLoaded)
        {
            SpawnCardsFromSavedData(svManager.cardSlot, svManager.cardName);
        }
        else
        {
            SpawnForAllGrids();
            ShuffleCards();
        }
    }

    void SpawnForAllGrids()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardPosition[i], true);
            newCard.transform.localPosition = Vector3.zero;
            newCard.transform.localScale = Vector3.one;
            cards.Add(newCard);

            Image cardImage = newCard.GetComponent<Image>();
            if (cardImage != null)
            {
                int imageIndex = i % images.Length;
                cardImage.sprite = images[imageIndex];
                newCard.GetComponent<Card>().image = cardImage.sprite;
                newCard.GetComponent<Card>().name = images[imageIndex].name;
                newCard.GetComponent<Card>().cardName = images[imageIndex].name;
            }
        }

        int initialCount = cards.Count;
        for (int i = 0; i < initialCount; i++)
        {
            GameObject duplicateCard = Instantiate(cards[i], cardPosition[i + initialCount], true);
            duplicateCard.transform.localPosition = Vector3.zero;
            duplicateCard.transform.localScale = Vector3.one;
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
            cards[i].GetComponent<Card>().cardSlot = cards[i].transform.parent.name;
            //PlayerPrefs.SetString("CardData_" + i , cards[i].GetComponent<Card>().cardSlot+ "|"+ cards[i].GetComponent<Card>().cardName);
            //Debug.Log(PlayerPrefs.GetString("CardData_" + i , cards[i].GetComponent<Card>().cardSlot+ cards[i].GetComponent<Card>().cardName));
        }
    }
}