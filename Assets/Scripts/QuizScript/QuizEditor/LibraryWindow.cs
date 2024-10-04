using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace QuizLibrary
{
    public class LibraryWindow : EditorWindow
    {
        List<Quiz> quizList = new();

        [MenuItem("Window/Library")]
        static void ShowWindow()
        {
            LibraryWindow window = (LibraryWindow)GetWindow(typeof(LibraryWindow));
            window.Show();
        }
        void OnEnable()
        {
            Init();
            Debug.Log(quizList.Count);
        }
        void OnGUI()
        {
            DrawSettings();
        }
        void Init()
        {
            LoadQuiz();
        }
        void LoadQuiz()
        {
            foreach (Quiz quiz in Resources.LoadAll("Quizzes", typeof(Quiz)).Cast<Quiz>())
            {
                quizList.Add(quiz);
            }
        }
        void DrawSettings()
        {
            if (GUILayout.Button("Create"))
            {
                QuizEditor.OpenWindow();
            }
        }
    }
}