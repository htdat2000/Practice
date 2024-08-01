using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [SerializeField] Quiz[] quizs;

    int currentIndex = 0;
    Quiz currentQuiz;

    [SerializeField] TMP_Text question;
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
        a.text = currentQuiz.answerA;
        b.text = currentQuiz.answerB;
        c.text = currentQuiz.answerC;
        d.text = currentQuiz.answerD;
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
