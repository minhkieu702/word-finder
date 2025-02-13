using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDataLoaded : MonoBehaviour
{
    [System.Serializable]
    public class SearchingWord
    {
        public bool Found = false;
        public string Word;
    }
    public float timeInSeconds;
    public int Columns = 0;
    public int Rows = 0;
    public BoardRow[] Board;
    public List<SearchingWord> SearchWords = new List<SearchingWord>();

    public void ClearData()
    {
        foreach (var word in SearchWords)
        {
            word.Found = false;
        }
    }

    [System.Serializable]
    public class BoardRow
    {
        public int Size;
        public string[] Row;

        public BoardRow()
        {

        }
        public BoardRow(int size)
        {
            CreateRow(size);
        }

        public void CreateRow(int size)
        {
            Size = size;
            Row = new string[Size];
            ClearRow();
        }
        public void ClearRow()
        {
            for (int i = 0; i < Size; i++)
            {
                Row[i] = " ";
            }
        }
    }

    public void ClearWithEmptyString()
    {
        for (int i = 0; i < Columns; i++)
        {
            Board[i].ClearRow();
        }
    }

    public void CreateNewBoard()
    {
        Board = new BoardRow[Columns];
        for (int i = 0; i < Columns; i++)
        {
            Board[i] = new BoardRow(Rows);
        }
    }
}
