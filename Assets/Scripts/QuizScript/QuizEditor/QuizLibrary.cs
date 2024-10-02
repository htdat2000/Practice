using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace QuizLibrary
{
    public class QuizLibrary
    {

        List<Quiz> quizList = new();

        void LoadQuiz()
        {
            foreach (Quiz quiz in Resources.LoadAll("Quizzes", typeof(Quiz)).Cast<Quiz>())
            {
                quizList.Add(quiz);
            }
        }
    }
}