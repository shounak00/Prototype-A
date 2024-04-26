using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite image;
    public bool isFlipped = false;
    public bool isMatched = false;

    public void Flip()
    {
        if (!isMatched)
        {
            isFlipped = !isFlipped;
            Debug.Log("Card flipped!");
        }
    }

    public void FlipBack()
    {
        isFlipped = false;
        Debug.Log("Card unflipped!");
    }
}

