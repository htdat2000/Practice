using UnityEngine;
using UnityEditor;

namespace QuizLibrary
{
    public class QuizEditor : EditorWindow
    {
        Quiz quizInfo;
        string fileName = " hahaah hahah ";

        bool[] answerSelected = { true, false, false, false };

        string path = "Assets/Resources/Quizzes/";

        void OnEnable()
        {
            quizInfo = CreateInstance<Quiz>();
        }
        public static void OpenWindow()
        {
            QuizEditor window = GetWindow<QuizEditor>();
            window.minSize = new(600, 300);
            window.Show();
        }
        void OnGUI()
        {
            DrawSettings();
        }
        void DrawSettings()
        {
            //File name for saving 
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("File name");
            fileName = EditorGUILayout.TextField(fileName);
            EditorGUILayout.EndHorizontal();

            //Quiz describe
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Quiz");
            quizInfo.quiz = EditorGUILayout.TextArea(quizInfo.quiz, GUILayout.MinHeight(50));
            EditorGUILayout.EndHorizontal();

            //Answer section
            AnswerSectionSettings();

            //Difficulty choosing
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Difficulty");
            quizInfo.difficulty = (Difficulty)EditorGUILayout.EnumPopup(quizInfo.difficulty);
            EditorGUILayout.EndHorizontal();

            //Save data
            if (GUILayout.Button("Save"))
            {
                Debug.Log("Save");
                Save();
            }
        }
        void AnswerSectionSettings()
        {
            //Drawing answer section
            GUILayout.Label("Answer");
            for (int i = 0; i < quizInfo.answer.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                answerSelected[i] = EditorGUILayout.Toggle(answerSelected[i]);
                if (answerSelected[i] == true)
                {
                    quizInfo.correctAnswer = i;
                    for (int j = 0; j < 4; j++)
                    {
                        if (j != i)
                        {
                            answerSelected[j] = false;
                        }
                    }
                }
                quizInfo.answer[i] = EditorGUILayout.TextField(quizInfo.answer[i]);
                EditorGUILayout.EndHorizontal();
            }
        }
        void Save()
        {
            string saveName = fileName.Replace(" ", "");
            string savePath = path + saveName + ".asset";

            string assetGUID = AssetDatabase.AssetPathToGUID(savePath, AssetPathToGUIDOptions.OnlyExistingAssets);
            if ( assetGUID != null && assetGUID != "")
            {
                Debug.Log(AssetDatabase.AssetPathToGUID(savePath));
                Debug.Log("Exist");
            }
            else
            {
                AssetDatabase.CreateAsset(quizInfo, savePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                quizInfo = CreateInstance<Quiz>();
            }
        }
    }
}