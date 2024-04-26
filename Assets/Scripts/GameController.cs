using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private CardManager cardManager;
    private ScoreManager scoreManager;
    private SaveManager saveManager;

    GameObject raycastHitThis;
    [SerializeField] private Card selectedCard;

    void Start()
    {
        cardManager = GameManager.Instance.cardManager;
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
                raycastHitThis = hit.collider.gameObject;
                selectedCard = raycastHitThis.GetComponent<Card>();
                
                if (selectedCard != null && GameManager.Instance.gameStarted)
                {
                    cardManager.FlipCard(selectedCard);
                }
                
                else
                {
                    Debug.Log("Raycast hit something but no Card component found."+ hit.collider.gameObject.name);
                }
            }
            
            else
            {
                Debug.Log("Raycast didn't hit anything.");
            }
        }
    }
}