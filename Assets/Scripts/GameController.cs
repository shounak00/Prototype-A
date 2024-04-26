using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CardManager cardManager;
    private ScoreManager scoreManager;
    private SaveManager saveManager;

    void Start()
    {
        cardManager = FindObjectOfType<CardManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Card selectedCard = hit.collider.GetComponent<Card>();
                if (selectedCard != null && GameManager.Instance.gameStarted)
                {
                    cardManager.FlipCard(selectedCard);
                }
            }
        }
    }
}