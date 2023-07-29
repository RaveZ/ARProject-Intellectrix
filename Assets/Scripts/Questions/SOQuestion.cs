using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Question
{
    [CreateAssetMenu(fileName = "QuestionData", menuName = "QuestionData")]
    public class SOQuestion : ScriptableObject
    {
        public String Question;

        public String[] Answer;
        public String CorrectAnswerText;
        [Range(0, 3)]
        public int CorrectAnswer;
        public int Score;
        public bool CardAlreadyUsed;
    }
}