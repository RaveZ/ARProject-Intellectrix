using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlayerManager : MonoBehaviour
{
    public UnityEvent OnAllPlayerTurnEnd = null;
 
    public Color HighlightColor;
    public Color NoHighlightColor;
    public Image[] PlayerHighlight;
    public TMP_Text[] PlayerScoreText;
    void Start()
    {
        ResetHighlight();
    }

    public void ResetHighlight()
    {
        for (int i = 0; i < PlayerHighlight.Length; i++)
        {
            PlayerHighlight[i].color = NoHighlightColor;
        }
    }

    public void PlayTurn()
    {
        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            ResetHighlight();
            PlayerHighlight[gameManager.CurrentTurn].color = HighlightColor;
        }
    }
    public void NextTurn()
    {
        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            if(gameManager.CurrentTurn == gameManager.PlayerScore.Length-1)
            {
                gameManager.CurrentTurn = 0;
                OnAllPlayerTurnEnd?.Invoke();
                return;
            }
            else
            {
                gameManager.CurrentTurn++;
                PlayTurn();
            }
        }
    }

    public void UpdateScore()
    {
        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.CalculateScore();
            for(int i = 0; i < PlayerScoreText.Length; i++)
            {
                PlayerScoreText[i].text = gameManager.PlayerScore[i].ToString();
            }
        }
    }
}
