using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

namespace QuizLibrary
{
    public class LibraryWindow : EditorWindow
    {
        static QuizLibrary quizLibrary;

        static SerializedObject librarySO;

        ReorderableList quizList;
        Quiz selectedQuiz;

        [Header("Layout")]
        Rect headerSection;
        Rect quizListSection;
        Rect quizEditorSection;

        [Header("Texture")]
        Texture2D headerTexture;
        Texture2D quizListTexture;
        Texture2D editorTexture;

        [Header("Quiz Editor Section")]
        string currentQuiz = "Enter Quiz Here";
        string[] currentAnswer = new string[4];
        int currentCorrectAnswer;
        Difficulty currentDifficulty;


        //[MenuItem("Window/LibraryMainMenu")]
        public static void ShowWindow(QuizLibrary _lib)
        { 
            quizLibrary = _lib;
            librarySO = new SerializedObject(_lib);
            
            LibraryWindow window = GetWindow<LibraryWindow>();
            window.minSize = new Vector2(600, 400);
            window.maxSize = new Vector2(600, 400);
            window.Show();
        }
        void OnEnable()
        {
            EditorApplication.update += Init;
        }
        void OnGUI()
        {
            librarySO.Update();
            DrawTexture();

            DrawHeaderSection();
            if (quizList != null)
            {
                DrawQuizListSection();          
            }
            //if(selectedQuiz != null)
            {
                DrawQuizEditorSection();
            }

            librarySO.ApplyModifiedProperties(); 
        }
        void Init()
        {
            EditorApplication.update -= Init;
            DrawLayout();
            InitTexture();
            CreateQuizList();

        }
        #region layout and texture 
        void DrawLayout()
        {
            //Draw header layout
            headerSection.x = 0;
            headerSection.y = 0;
            headerSection.width = Screen.width;
            headerSection.height = 60;

            //Draw Quiz List layout
            quizListSection.x = 0;
            quizListSection.y = 60;
            quizListSection.width = 200;
            quizListSection.height = Screen.height - 60;

            //Draw Quiz Editor layout
            quizEditorSection.x = 200;
            quizEditorSection.y = 60;
            quizEditorSection.width = Screen.width - 200;
            quizEditorSection.height = Screen.height - 60;
        }
        void InitTexture()
        {
            headerTexture = new Texture2D(1, 1);
            headerTexture.SetPixel(0, 0, Color.black);
            headerTexture.Apply();

            quizListTexture = new Texture2D(1, 1);
            quizListTexture.SetPixel(0, 0, Color.white);
            quizListTexture.Apply();

            editorTexture = new Texture2D(1, 1);
            editorTexture.SetPixel(0, 0, Color.gray);
            editorTexture.Apply();
        }
        void DrawTexture()
        {
            GUI.DrawTexture(new Rect(200, 0, 200, 60), headerTexture);
            GUI.DrawTexture(quizEditorSection, editorTexture);
            GUI.DrawTexture(quizListSection, quizListTexture);
        }
        #endregion
        
        #region Header Section
        void DrawHeaderSection()
        {
            GUILayout.BeginArea(headerSection);
            Rect rectDrawPos = new(200, 0, 200, 20f);
            GUI.enabled = false;
            quizLibrary = (QuizLibrary)EditorGUI.ObjectField(rectDrawPos, quizLibrary, typeof(QuizLibrary), false);
            //CreateQuizList();
            GUI.enabled = true;
            GUILayout.EndArea();
        }
        void CreateQuizList()
        {
            if (quizLibrary != null && quizList == null)
            {
                librarySO = new SerializedObject(quizLibrary);
                quizList = new ReorderableList(librarySO, librarySO.FindProperty("quizList"));
            }
        }
        #endregion

        #region Quiz List Section
        void DrawQuizListSection()
        {
            GUILayout.BeginArea(quizListSection);
            quizList.DoLayoutList();
            GUILayout.EndArea();
        }
        #endregion

        #region Quiz Editor Section
        void DrawQuizEditorSection()
        {
            GUILayout.BeginArea(quizEditorSection);
            GUILayout.BeginArea(new Rect(50, 0, quizEditorSection.width, quizEditorSection.height));
            EditorGUILayout.LabelField("Quiz:");
            currentQuiz = GUILayout.TextArea(currentQuiz, 400, GUILayout.Width(300), GUILayout.Height(150));
            GUILayout.EndArea();
            GUILayout.EndArea();
        }
        #endregion
        
    }
}