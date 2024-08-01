using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "Data/Quiz")]
public class Quiz : ScriptableObject
{
    public string quiz; // the question will be stored in this variable 
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;
    public int correctAnswer;
}
