using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIAnswerManager : MonoBehaviour
    {
        public Color AnsweredColor;
        public Color NoAnswerColor;
        public Image[] AnswerOption;
        public int PickedOption;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateAnswer(int _pickedOption)
        {
            var gameManager = FindObjectOfType<GameManager>();
            PickedOption = _pickedOption;
            if (gameManager != null)
            {
                gameManager.PlayerCurrentOption[gameManager.CurrentTurn] = PickedOption;    
                ResetAnswer();
                AnswerOption[_pickedOption].color = AnsweredColor;
            }
        }

        public void ResetAnswer()
        {
            for (int i = 0; i < AnswerOption.Length; i++)
            {
                AnswerOption[i].color = NoAnswerColor;
            }
        }
    }

     
}