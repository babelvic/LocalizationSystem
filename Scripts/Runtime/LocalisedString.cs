using UnityEngine;

[System.Serializable]
public struct LocalisedString
{
    public int value;
    public TextAsset csvFile;
    
    public static implicit operator string(LocalisedString textField)
    {
        return LocalizationSystem.GetString(textField.csvFile, textField.value);
    }
}
