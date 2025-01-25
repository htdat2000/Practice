using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

namespace QuizLibrary
{
    public class LibraryWindow : EditorWindow
    {
        static QuizLibrary quizLibrary;

        static SerializedObject librarySO;

        [Header("Header Section")]
        Rect headerSection; // (0, 0, 600, 60)
        Rect libraryInputField = new(200, 0, 200, 20); //object field for assigning library

        [Header("Quiz List Section")]
        Rect quizListSection;   //(0, 60, 200, 340)
        Rect titleField = new(0, 0, 200, 20);
        Rect quizField = new(0, 20, 200, 320);
        ReorderableList quizList;
        int listHeight = 290;
        int addBtnHeight = 30;
        Vector2 scrollPos;
        Quiz selectedQuiz;
        int selectedAnswer = -1;

        [Header("Quiz Editor Section")]
        Rect quizEditorSection; //(200, 60, 400, 340)
        Rect quizDataField = new(25, 20, 350, 260);
        ReorderableList answerList;


        [Header("Texture")]
        Texture2D headerTexture;
        Texture2D quizListTexture;
        Texture2D editorTexture;

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
            //DrawTexture();

            DrawHeaderSection();
            if (quizList != null)
            {
                DrawQuizListSection();
            }
            if (selectedQuiz != null)
            {
                DrawQuizEditorSection();
            }

            librarySO.ApplyModifiedProperties();
        }
        void Init()
        {
            EditorApplication.update -= Init;
            DrawLayout();
            //InitTexture();
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
            //GUI.DrawTexture(quizDataField, quizListTexture);
        }
        #endregion

        #region Header Section
        void DrawHeaderSection()
        {
            GUILayout.BeginArea(headerSection);
            {
                GUILayout.Label("YOUR QUIZ LIBRARY");
                GUI.enabled = false;
                quizLibrary = (QuizLibrary)EditorGUI.ObjectField(libraryInputField, quizLibrary, typeof(QuizLibrary), false);
                GUI.enabled = true;
            }
            GUILayout.EndArea();
        }
        #endregion

        #region Quiz List Section
        void DrawQuizListSection()
        {
            GUILayout.BeginArea(quizListSection);
            {
                GUILayout.BeginArea(quizField);
                {
                    GUILayout.BeginVertical();
                    {
                        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Height(listHeight));
                        quizList.DoLayoutList();
                        EditorGUILayout.EndScrollView();
                        if (GUILayout.Button("Add", GUILayout.Height(addBtnHeight)))
                        {
                            Debug.Log("Add new quiz");
                        }
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
            GUILayout.EndArea();
        }
        void CreateQuizList()
        {
            if (quizLibrary != null && quizList == null)
            {
                librarySO = new SerializedObject(quizLibrary);
                quizList = new ReorderableList(librarySO, librarySO.FindProperty("quizList"), true, true, false, false);
                DrawListElement(quizList);
                OnListElementSelected(quizList);
                quizList.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Quiz List");
                };
            }
        }
        void DrawListElement(ReorderableList _reoderableList)
        {
            _reoderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {     
                var element = _reoderableList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty quizString = element.FindPropertyRelative("quiz");
                EditorGUI.LabelField(rect, quizString.stringValue, CustomStyles.ReListEleLabelStyle);
            };
        }
        void OnListElementSelected(ReorderableList _reoderableList)
        {
            _reoderableList.onSelectCallback = (ReorderableList l) =>
                {
                    selectedQuiz = quizLibrary.QuizList[l.index];
                    CreateAnswerList();
                };
        }
        #endregion

        #region Quiz Editor Section
        void DrawQuizEditorSection()
        {
            GUILayout.BeginArea(quizEditorSection);
            {
                GUILayout.BeginArea(quizDataField, CustomStyles.EditorPadding);
                {
                    EditorGUILayout.LabelField("Quiz:");
                    selectedQuiz.quiz = GUILayout.TextArea(selectedQuiz.quiz, 200, GUILayout.Width(300), GUILayout.Height(80));
                    GUILayout.Space(10);

                    answerList?.DoList(new Rect(25, 130, 150, 70));

                    if (selectedAnswer == -1)
                    {
                        answerList.index = 0;
                        selectedAnswer = 0;
                    }
                    selectedQuiz.answer[selectedAnswer] = EditorGUI.TextArea(new Rect(195, 130, 130, 70), selectedQuiz.answer[selectedAnswer]);
                    selectedQuiz.difficulty = (Difficulty)EditorGUI.EnumPopup(new Rect(195, 205, 130, 20), selectedQuiz.difficulty);
                }
                GUILayout.EndArea();
            }
            GUILayout.EndArea();
        }
        void CreateAnswerList()
        {
            if (quizList != null && selectedQuiz != null)
            {
                answerList = new ReorderableList(selectedQuiz.answer, typeof(string), false, true, false, false);
            }
            selectedAnswer = -1;
            DrawAnswerListElement();
            OnAnswerSelected();
            answerList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Answers");
            };
        }
        void DrawAnswerListElement()
        {
            answerList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = answerList.list[index];
                EditorGUI.LabelField(rect, "Answer " + index + ": " + element.ToString(), CustomStyles.ReListEleLabelStyle);
            };
        }
        void OnAnswerSelected()
        {
            answerList.onSelectCallback = (ReorderableList l) =>
                {
                    selectedAnswer = l.index;
                };
        }
        #endregion
    }
}