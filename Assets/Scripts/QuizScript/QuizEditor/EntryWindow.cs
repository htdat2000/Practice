using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QuizLibrary
{
    public class EntryWindow : EditorWindow
    {
        QuizLibrary quizLibrary;

        static EntryWindow window;
        string filePath = "Assets/Resources/Quizzes/";

        [Header("Help Box Design")]
        GUIContent infoIcon;
        int iconWidth = 35;
        int iconHeight = 35;
        readonly string boxMessage = "Please assign your library or create a new one to continue.";

        [Header("Input Section")]
        Rect inputSection;
        Rect inputField = new Rect(100, 0, 100, 20); //The object field for assigning library 

        [Header("Message Section")]
        Rect messageSection;
        Rect helpBoxField = new Rect(25, 10, 250, 55);
        Rect buttonField = new Rect(195, 60, 80, 25);

        [Header("Texture")]
        Texture2D inputSecTexture;
        Texture2D messageSecTexture;

        [MenuItem("Window/Library")]
        static void ShowWindow()
        {
            window = GetWindow<EntryWindow>();
            window.minSize = new Vector2(300, 115);
            window.maxSize = new Vector2(300, 115);
            window.Show();
        }
        void OnEnable()
        {
            infoIcon = EditorGUIUtility.IconContent("console.infoicon");
            DrawLayout();
            InitTexture();
        }
        void OnGUI()
        {
            //DrawTexture();
            DrawInputSection();
            DrawMessageSection();
        }

        #region Init Layout, Content, Style, Texture Region
        void DrawLayout()
        {
            inputSection.x = 0;
            inputSection.y = 0;
            inputSection.width = 300;
            inputSection.height = 20;

            messageSection.x = 0;
            messageSection.y = 20;
            messageSection.width = 300;
            messageSection.height = Screen.height - inputSection.height;
        }
        void InitTexture()
        {
            inputSecTexture = new Texture2D(1, 1);
            inputSecTexture.SetPixel(0, 0, Color.black);
            inputSecTexture.Apply();

            messageSecTexture = new Texture2D(1, 1);
            messageSecTexture.SetPixel(0, 0, Color.white);
            messageSecTexture.Apply();
        }
        void DrawTexture()
        {
            GUI.DrawTexture(inputSection, inputSecTexture);
            //GUI.DrawTexture(messageSection, messageSecTexture);
        }
        #endregion

        #region Draw Function Section 
        void DrawInputSection()
        {
            GUILayout.BeginArea(inputSection);
            {
                quizLibrary = (QuizLibrary)EditorGUI.ObjectField(inputField, quizLibrary, typeof(QuizLibrary), false);
                if (quizLibrary != null)
                {
                    OpenLibrary(quizLibrary);
                }
            }
            GUILayout.EndArea();
        }
        void DrawMessageSection()
        {
            GUILayout.BeginArea(messageSection);
            {
                GUILayout.BeginArea(helpBoxField);
                {
                    GUILayout.BeginHorizontal(CustomStyles.HelpBoxStyle);
                    GUILayout.Label(infoIcon, GUILayout.Width(iconWidth), GUILayout.Height(iconHeight));
                    GUILayout.Label(boxMessage, CustomStyles.HelpBoxMessageStyle);
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndArea();
                GUILayout.BeginArea(buttonField);
                {
                    if (GUILayout.Button("Create", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)))
                    {
                        CreateLibrary();
                    }
                }
                GUILayout.EndArea();
            }
            GUILayout.EndArea();
        }
        #endregion

        void CreateLibrary()
        {
            string fileName = "library";
            string savePath = filePath + fileName + ".asset";

            //Check whether the file exist
            string assetGUID = AssetDatabase.AssetPathToGUID(savePath, AssetPathToGUIDOptions.OnlyExistingAssets);
            if (assetGUID != null && assetGUID != "")
            {
                QuizLibrary lib = (QuizLibrary)AssetDatabase.LoadAssetAtPath(savePath, typeof(QuizLibrary));
                OpenLibrary(lib);

            }
            else //if not create a new one
            {
                QuizLibrary newLibrary = CreateInstance<QuizLibrary>();
                AssetDatabase.CreateAsset(newLibrary, savePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                OpenLibrary(newLibrary);
            }
        }
        void OpenLibrary(QuizLibrary _lib)
        {
            LibraryWindow.ShowWindow(_lib);
            window.Close();
        }
    }
}