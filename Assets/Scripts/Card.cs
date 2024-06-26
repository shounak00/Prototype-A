using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public Sprite image;
    public bool isFlipped = false;
    
    public GameObject backSide;
    private Quaternion targetRotation;
    private float flipSpeed = 0.1f;

    public string cardName;
    public string cardSlot;

    private void Start()
    {
        StartCoroutine(WaitandFlip());
    }
    
    IEnumerator WaitandFlip()
    {
        yield return new WaitForSeconds(1f);
        FlipBack();
    }

    public void Flip()
    {
        isFlipped = true;

        targetRotation = Quaternion.Euler(0, 90, 0);
        transform.DORotate(targetRotation.eulerAngles, flipSpeed).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            backSide.SetActive(false);

            targetRotation = Quaternion.Euler(0, 0, 0);
            transform.DORotate(targetRotation.eulerAngles, flipSpeed).SetEase(Ease.OutQuad);
            
            GameManager.Instance.CountFlippedCards();
        });

        //Debug.Log("Card flipped!");
    }

    public void FlipBack()
    {
        isFlipped = false;
        
        targetRotation = Quaternion.Euler(0, 90, 0);
        transform.DORotate(targetRotation.eulerAngles, flipSpeed).SetEase(Ease.OutQuad);
                
        backSide.SetActive(true);
                
        targetRotation = Quaternion.Euler(0, 180, 0);
        transform.DORotate(targetRotation.eulerAngles, flipSpeed).SetEase(Ease.OutQuad);
        
        //Debug.Log("Card unflipped!");
    }
}

