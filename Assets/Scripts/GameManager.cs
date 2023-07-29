using Game.Question;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int[] PlayerScore = new int[4];
    public int[] PlayerCurrentOption = new int[4];
    public int CurrentTurn;
    public int SelectedCard;

    public SOQuestion[] QuestionCards;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentTurn = 0;
        ResetCardUseState();
    }
    public void ResetCardUseState()
    {
        for(int i = 0; i < QuestionCards.Length; i++)
        {
            QuestionCards[i].CardAlreadyUsed = false;
        }
    }
    public void DrawCard()
    {
        var UsedCount = 0;
        for (int i = 0; i < QuestionCards.Length; i++)
        {
            if (QuestionCards[i].CardAlreadyUsed)
            {
                UsedCount++;
            }
        }
        if(UsedCount == QuestionCards.Length)
        {
            print("Run out of card");
            return;
        }else
        {
            SelectedCard = Random.Range(0, QuestionCards.Length);
            QuestionCards[SelectedCard].CardAlreadyUsed = true;
        }

    } 

    public void CalculateScore()
    {
        for(int i = 0; i < PlayerScore.Length; i++)
        {
            if (PlayerCurrentOption[i] == QuestionCards[SelectedCard].CorrectAnswer)
            {
                PlayerScore[i] += QuestionCards[SelectedCard].Score;
            }

        }
    }
}
