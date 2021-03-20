using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocalisedString))]
public class LocalisedStringDrawer : PropertyDrawer
{
    private bool dropdown;
    private float height;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        height = 20;
        if (dropdown)
        {
            return height + 25;
        }

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.width -= 34;
        position.height = 18;

        Rect foldButtonRect = new Rect(position);
        foldButtonRect.width = 5;
        foldButtonRect.height = 20;

        dropdown = EditorGUI.Foldout(foldButtonRect, dropdown, "");

        position.x += 15;
        position.width -= 15;
        
        var csvFile = property.FindPropertyRelative("csvFile");
        
        Rect valueRect = new Rect(position);
        

        //The Key
        SerializedProperty value = property.FindPropertyRelative("value");
        if (csvFile.objectReferenceValue == null)
        {
            string message = "To find any value you must select a CSV file";
            EditorGUI.HelpBox(valueRect, message, MessageType.Warning);
        }
        else
        {
            if(csvFile.objectReferenceValue.GetType() == typeof(TextAsset))
            {
                EditorGUI.HelpBox(valueRect, LocalizationSystem.GetString(csvFile.objectReferenceValue as TextAsset, value.intValue), MessageType.None);
            }
            else
            {
                string message = "Please, select a CSV file, the current is not";
                EditorGUI.HelpBox(valueRect, message, MessageType.Error);
            }
        }

        if (dropdown)
        {
            position.y += 25;
            EditorGUI.PropertyField(position, csvFile, label);
        }

        position.x += position.width + 2;
        position.width = 17;
        position.height = 17;

        if (GUI.Button(position, "S"))
        {
            if (csvFile.objectReferenceValue != null)
            {
                var Ids = GetIds(csvFile.objectReferenceValue as TextAsset);
                
                if (Ids.Length > 0)
                {
                    TextLocaliserSearchWindow.Open(Ids, property);
                }
            }
        }
        
        EditorGUI.EndProperty();
    }
    
    
    public static string[] GetIds(TextAsset csvFile)
    {
        List<string> IDs = new List<string>();
        
        string[] csvData = csvFile.text.Split(new char[]{'\n'});

        for (int i = 1; i < csvData.Length - 1; i++)
        {
            string[] csvInfo = csvData[i].Split(new char[] {';'});

            IDs.Add(csvInfo[0]);
        }

        return IDs.ToArray();
    }
}
