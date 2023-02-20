using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public Transform environmentRoot;
    

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                switch (letter)
                {
                    case 'x':
                        Instantiate(rockPrefab, new Vector3(column + 0.5f, row + 0.5f, 0f), Quaternion.identity, environmentRoot);
                        break;
                    case 'b':
                        Instantiate(brickPrefab, new Vector3(column + 0.5f, row + 0.5f, 0f), Quaternion.identity, environmentRoot);
                        break;
                    case '?':
                        Instantiate(questionBoxPrefab, new Vector3(column + 0.5f, row + 0.5f, 0f), Quaternion.identity, environmentRoot);
                        break;
                    case 's':
                        Instantiate(stonePrefab, new Vector3(column + 0.5f, row + 0.5f, 0f), Quaternion.identity, environmentRoot);
                        break;
                }
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
