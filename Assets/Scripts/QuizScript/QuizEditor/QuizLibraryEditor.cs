using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4]; // Four possible answers
    public int correctAnswerIndex = 0;       // Index of the correct answer
}

public class QuizLibraryEditor : EditorWindow
{
    private List<Question> questions = new List<Question>(); // The quiz library
    private Vector2 scrollPos; // For scrollable view
    private int selectedQuestionIndex = -1; // To track which question is selected for editing

    [MenuItem("Window/Quiz Library Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuizLibraryEditor>("Quiz Library Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        
        // Left Side: Question List Panel
        DrawQuestionListPanel();
        
        // Right Side: Question Details Panel (Shown if a question is selected)
        if (selectedQuestionIndex >= 0)
        {
            DrawQuestionEditorPanel();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawQuestionListPanel()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(250));
        EditorGUILayout.LabelField("Quiz Library", EditorStyles.boldLabel);

        // Scrollable list of questions
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(400));
        
        for (int i = 0; i < questions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            // Display question text or placeholder if empty
            string questionTitle = string.IsNullOrEmpty(questions[i].questionText) ? "Untitled Question" : questions[i].questionText;
            
            // Button to select the question for editing
            if (GUILayout.Button(questionTitle, GUILayout.Width(150)))
            {
                selectedQuestionIndex = i; // Select the question to edit
            }

            // Button to delete the question
            if (GUILayout.Button("Delete", GUILayout.Width(50)))
            {
                if (EditorUtility.DisplayDialog("Delete Question", "Are you sure you want to delete this question?", "Yes", "No"))
                {
                    questions.RemoveAt(i);
                    selectedQuestionIndex = -1; // Unselect the question if deleted
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        // Add new question button
        if (GUILayout.Button("Add New Question"))
        {
            questions.Add(new Question());
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawQuestionEditorPanel()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Edit Question", EditorStyles.boldLabel);

        if (selectedQuestionIndex >= 0 && selectedQuestionIndex < questions.Count)
        {
            // Edit question text
            questions[selectedQuestionIndex].questionText = EditorGUILayout.TextField("Question", questions[selectedQuestionIndex].questionText);

            // Edit answer options
            for (int i = 0; i < questions[selectedQuestionIndex].answers.Length; i++)
            {
                questions[selectedQuestionIndex].answers[i] = EditorGUILayout.TextField($"Answer {i + 1}", questions[selectedQuestionIndex].answers[i]);

                // Radio button to select the correct answer
                bool isCorrect = (questions[selectedQuestionIndex].correctAnswerIndex == i);
                if (EditorGUILayout.Toggle("Correct Answer", isCorrect))
                {
                    questions[selectedQuestionIndex].correctAnswerIndex = i;
                }
            }
        }

        EditorGUILayout.EndVertical();
    }
}
