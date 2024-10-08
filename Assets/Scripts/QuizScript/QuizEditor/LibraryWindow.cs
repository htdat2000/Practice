using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace QuizLibrary
{
    public class LibraryWindow : EditorWindow
    {
        QuizLibrary quizLibrary;

        Rect headerSection;
        Rect featureSection;

        Texture2D headerTexture;

        [MenuItem("Window/Library")]
        static void ShowWindow()
        {
            LibraryWindow window = (LibraryWindow)GetWindow(typeof(LibraryWindow));
            window.minSize = new Vector2(600, 400);
            window.maxSize = new Vector2(600, 400);
            window.Show();
        }
        void OnEnable()
        {
            Init();
        }
        void OnGUI()
        {
            //GUI.DrawTexture(new Rect(200, 0, 200, 60), headerTexture);
           
            //DrawSettings();
            DrawHeaderSection();

            
        }
        void Init()
        {
            DrawLayout();

            headerTexture = new Texture2D(1, 1);
            headerTexture.SetPixel(0, 0, Color.black);
            headerTexture.Apply();
        }
        void DrawLayout()
        {
            //Draw header layout
            headerSection.x = 0;
            headerSection.y = 0;
            headerSection.width = Screen.width;
            headerSection.height = 60;

            //Draw feature layout
        }
        void DrawHeaderSection()
        {
            GUILayout.BeginArea(headerSection);
            Rect rectDrawPos = new(200, 0, 200, 20f);
            quizLibrary = (QuizLibrary)EditorGUI.ObjectField(rectDrawPos, quizLibrary, typeof(QuizLibrary), true);
            GUILayout.EndArea();
        }
        /*
        void LoadQuiz()
        {
            foreach (Quiz quiz in Resources.LoadAll("Quizzes", typeof(Quiz)).Cast<Quiz>())
            {
                quizList.Add(quiz);
            }
        }
        */
        void DrawSettings()
        {
            if (GUILayout.Button("Create"))
            {
                QuizEditor.OpenWindow();
            }
        }
    }
}