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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Debug raycast origin
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 2f);

            if (Physics.Raycast(ray, out hit))
            {
                Card selectedCard = hit.collider.GetComponent<Card>();
                if (selectedCard != null && GameManager.Instance.gameStarted)
                {
                    cardManager.FlipCard(selectedCard);
                }
                
                else
                {
                    Debug.Log("Raycast hit something but no Card component found.");
                }
            }
            
            else
            {
                Debug.Log("Raycast didn't hit anything.");
            }
        }
    }
}