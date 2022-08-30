using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 

public class CSVReader : MonoBehaviour
{
    [SerializeField]
    private TextAsset textAssedData;

    [System.Serializable]
    public class Word
    {
        public string wordString;
        public float valence;
    }


    public List<Word> wordList = new List<Word>();

    private void ReadCSV()
    {
        string[] data = textAssedData.text.Split(new string[] {";","\n"}, StringSplitOptions.None);

        int tableSize = data.Length / 2 - 1;
    
        for(int i = 0; i < tableSize; i++)
        {

            Word newWord = new Word();
            newWord.wordString = data[2 *(i + 1)];
            newWord.valence = float.Parse(data[2 *(i + 1) + 1]);

            wordList.Add(newWord);
        }
        
    }


    private void Start() {
        ReadCSV();
    }

    public float GetDamageAmount(string playerInput)
    {

        string[] playerWords = playerInput.Split(' ');
        float damage = 0;

        foreach(string word in playerWords)
        {
            for(int i = 0; i < wordList.Count; i++)
            {
                if(wordList[i].wordString == word)
                {
                    if(wordList[i].valence < 5)
                        damage += wordList[i].valence + 10;
                    else
                        damage += wordList[i].valence;
                    
                    break;
                }   
            }
        }
        damage += 5;
        print(damage);
        return damage;
    }
}   
