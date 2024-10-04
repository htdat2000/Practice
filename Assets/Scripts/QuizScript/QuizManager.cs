using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuizLibrary;

public class QuizManager : MonoBehaviour
{
    [SerializeField] Quiz[] quizs;

    int currentIndex = 0;
    Quiz currentQuiz;

    [SerializeField] TMP_Text question;
    [SerializeField] TMP_Text[] answerText = new TMP_Text[4];
    [SerializeField] TMP_Text a;
    [SerializeField] TMP_Text b;
    [SerializeField] TMP_Text c;
    [SerializeField] TMP_Text d;

    protected void Start()
    {
        LoadQuiz(currentIndex);
    }
    protected void LoadQuiz(int index)
    {
        if(currentIndex == quizs.Length)
        {
            Debug.Log("No More Quiz To Load");
            return;
        }
        currentQuiz = quizs[index];

        question.text = currentQuiz.quiz;
        
        for (int i = 0; i < 4; i++)
        {
            answerText[i].text = currentQuiz.answer[i];
        }
        currentIndex++;
    }
    public void AnswerButton(int answer)
    {
        if (currentQuiz.correctAnswer == answer)
        {
            Debug.Log("True");
            LoadQuiz(currentIndex);
        }
        else
        {
            Debug.Log("false");
        }
    }
}
