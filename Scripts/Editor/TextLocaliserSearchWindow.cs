using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public class TextLocaliserSearchWindow : EditorWindow
{
    public static string[] CsvDataIDs;

    public static SerializedProperty property;

    public static void Open(string[] csvList, SerializedProperty property)
    {
        TextLocaliserSearchWindow window = CreateInstance<TextLocaliserSearchWindow>();
        window.titleContent = new GUIContent("Localisation Search");
    
        CsvDataIDs = csvList;
        TextLocaliserSearchWindow.property = property;
    
        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
        window.ShowAsDropDown(r, new Vector2(500, 300));
    }

    public string key;
    public Vector2 scroll;

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
        key = EditorGUILayout.TextField(key);
        EditorGUILayout.EndHorizontal();

        GetSearchResults();
    }

    private void GetSearchResults()
    {
        if (key == null)
        {
            return;
        }
        
        TextAsset csvFile = property.FindPropertyRelative("csvFile").objectReferenceValue as TextAsset;
        SerializedProperty value = property.FindPropertyRelative("value");

        EditorGUILayout.BeginVertical();
        scroll = EditorGUILayout.BeginScrollView(scroll);

        for (int i = 0; i < CsvDataIDs.Length; i++)
        {
            if (CsvDataIDs[i].ToLower().Contains(key.ToLower()))
            {
                EditorGUILayout.BeginHorizontal("Box");
                
                EditorGUILayout.HelpBox(CsvDataIDs[i], MessageType.None);
        
                GUIContent content = new GUIContent("✔");
                
                EditorGUILayout.HelpBox(LocalizationSystem.GetString(csvFile,i), MessageType.None);
                
        
                if (GUILayout.Button(content, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                {
                    value.intValue = i;

                    
                    
                    this.Close();
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        
        value.serializedObject.ApplyModifiedProperties();
    }
}
