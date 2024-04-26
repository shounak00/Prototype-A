using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI matchesText;
    public GameObject gameEndPopUp;

    public void UpdateTurnsText()
    {
        turnsText.text ="Turns: " + GameManager.Instance.turns;
    }
    
    public void UpdateMatchesText()
    {
        matchesText.text ="Matches: " + GameManager.Instance.matches;
    }
}
