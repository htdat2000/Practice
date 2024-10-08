using UnityEngine;
using UnityEditor;

namespace QuizLibrary
{
    public class QuizEditor : EditorWindow
    {
        bool test = false;
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
            window.minSize = new(300, 275);
            window.Show();
        }
        void OnGUI()
        {
            DrawSettings();
        }
        void DrawSettings()
        {
            test = EditorGUILayout.Foldout(test, "haha");
            //File name for saving 
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("File name", GUILayout.MaxWidth(70));
            fileName = EditorGUILayout.TextField(fileName);
            EditorGUILayout.EndHorizontal();

            //Quiz describe
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Quiz", GUILayout.MaxWidth(70));
            quizInfo.quiz = EditorGUILayout.TextArea(quizInfo.quiz, GUILayout.MinHeight(50));
            EditorGUILayout.EndHorizontal();

            //Answer section
            AnswerSectionSettings();

            //Difficulty choosing
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Difficulty", GUILayout.MaxWidth(70));
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
            GUILayout.Label("Answer", GUILayout.MaxWidth(70));
            for (int i = 0; i < quizInfo.answer.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                answerSelected[i] = EditorGUILayout.Toggle(answerSelected[i], GUILayout.MaxWidth(40));
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
                quizInfo.answer[i] = EditorGUILayout.TextField(quizInfo.answer[i], GUILayout.MinHeight(25));
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