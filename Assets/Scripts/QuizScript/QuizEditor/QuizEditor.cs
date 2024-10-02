using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QuizLibrary
{
    public class QuizEditor : EditorWindow
    {
        static Quiz quizInfo;

        void OnEnable()
        {
            quizInfo = (Quiz)CreateInstance("Quiz");
        }
        public static void OpenWindow()
        {
            QuizEditor window = GetWindow<QuizEditor>();
            window.minSize = new (600, 300);
            window.Show();
        }
        void OnGUI()
        {
            DrawSettings();
        }
        void DrawSettings()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Title");
            quizInfo.tile = EditorGUILayout.TextField(quizInfo.tile);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Quiz");
            quizInfo.quiz = EditorGUILayout.TextArea(quizInfo.quiz,GUILayout.MinHeight(50));
            EditorGUILayout.EndHorizontal();

            DrawTextField("A", quizInfo.answerA);
            DrawTextField("B", quizInfo.answerB);
            DrawTextField("C", quizInfo.answerC);
            DrawTextField("D", quizInfo.answerD);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Difficulty");
            quizInfo.difficulty = (Difficulty)EditorGUILayout.EnumPopup(quizInfo.difficulty);
            EditorGUILayout.EndHorizontal();
        }
        void DrawTextField(string _title, string _)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(_title);
            _ = EditorGUILayout.TextField(_);
            EditorGUILayout.EndHorizontal();
        }
    }
}