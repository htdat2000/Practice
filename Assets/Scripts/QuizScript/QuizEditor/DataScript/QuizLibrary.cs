using System.Collections.Generic;
using UnityEngine;

namespace QuizLibrary
{
    [CreateAssetMenu(fileName = "Lib", menuName = "Data/QuizLib")]
    public class QuizLibrary : ScriptableObject 
    {
        [SerializeField] List<Quiz> quizList = new();

        public List<Quiz> QuizList
        {
            get { return quizList; }
        }
    }
}