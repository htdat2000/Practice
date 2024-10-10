using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace QuizLibrary
{
    [CreateAssetMenu(fileName = "Lib", menuName = "Data/QuizLib")]
    public class QuizLibrary : ScriptableObject 
    {
        [SerializeField] List<Quiz> quizList = new();

    }
}