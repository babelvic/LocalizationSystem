using UnityEngine;

[System.Serializable]
public struct LocalisedString
{
    public int value;
    public TextAsset csvFile;
    
    public static implicit operator string(LocalisedString textField)
    {
        return textField.csvFile == null ? "CSV File not attached" : LocalizationSystem.GetString(textField.csvFile, textField.value);
    }
}
