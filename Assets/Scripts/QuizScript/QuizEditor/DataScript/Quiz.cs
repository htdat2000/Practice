using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuizLibrary
{
    public enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }

    //[CreateAssetMenu(fileName = "Quiz", menuName = "Data/Quiz")]
    [Serializable]
    public class Quiz
    {
        public string quiz = "Quiz description"; // the question will be stored in this variable 
        public List<string> answer = new(4);
        public int correctAnswer;
        public Difficulty difficulty;
    }
}