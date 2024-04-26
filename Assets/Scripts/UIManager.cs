using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI matchesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScore;
    public GameObject gameEndPopUp;
    public GameObject gameBoardMenu;

    
    
    public void UpdateText()
    {
        turnsText.text ="Turns: " + GameManager.Instance.turns;
        matchesText.text ="Matches: " + GameManager.Instance.matches;
        scoreText.text ="Score: " + GameManager.Instance.score;
    }

    public void FinalPopUp()
    {
        gameEndPopUp.SetActive(true);
        gameBoardMenu.SetActive(false);
        finalScore.text = "Your Score is " + GameManager.Instance.score;
    }

    public void startPopOff()
    {
        gameEndPopUp.SetActive(false);
        gameBoardMenu.SetActive(true);
        UpdateText();
    }

}
