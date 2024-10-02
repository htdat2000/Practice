using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace QuizLibrary
{
    public class LibraryWindow : EditorWindow
    {
        //public List<Quiz> quizList = new();

        [MenuItem("Window/Library")]
        static void ShowWindow()
        {
            LibraryWindow window = (LibraryWindow)GetWindow(typeof(LibraryWindow));
            window.Show();
        }
        void OnGUI()
        {
            DrawSettings();
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