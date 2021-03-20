using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationSystem
{
    public enum Languages
    {
        ID = 0,
        English = 1,
        Spanish = 2,
        French = 3,
        German = 4 
    }

    public static Languages language = Languages.English;
    
    public static string GetString(TextAsset csvFile, int id)
    {
        return GetTable(csvFile)[id, (int) language];
    }

    private static string[,] GetTable(TextAsset csvFile)
    {
        string[] csvData = csvFile.text.Split('\n');
        string[,] csvTable = new string[csvData.Length-2, Enum.GetValues(typeof(Languages)).Length];

        for (int i = 1; i < csvData.Length - 1; i++)
        {
            string[] csvInfo = csvData[i].Split(';');

            for (int j = 0; j < csvInfo.Length; j++)
            {
                csvTable[i-1, j] = csvInfo[j];
            }
        }

        return csvTable;
    }
}
