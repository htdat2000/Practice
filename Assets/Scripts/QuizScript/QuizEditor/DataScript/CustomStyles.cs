using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class CustomStyles
{
    static GUIStyle helpBoxStyle;
    static GUIStyle helpBoxMessageStyle;

    public static GUIStyle HelpBoxStyle
    {
        get
        {
            helpBoxStyle ??= new GUIStyle(EditorStyles.helpBox);
            return helpBoxStyle;
        }
    }

    public static GUIStyle HelpBoxMessageStyle
    {
        get
        {
            helpBoxMessageStyle ??= new GUIStyle()
            {
                fontSize = 13, // Set your desired font size
                wordWrap = true // Ensure text wraps properly
            };
            return helpBoxMessageStyle;
        }
    }
}
