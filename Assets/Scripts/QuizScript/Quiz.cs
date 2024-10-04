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

    [CreateAssetMenu(fileName = "Quiz", menuName = "Data/Quiz")]
    public class Quiz : ScriptableObject
    {
        public string quiz; // the question will be stored in this variable 
        public string[] answer = new string[4];
        public int correctAnswer;
        public Difficulty difficulty;
    }
}