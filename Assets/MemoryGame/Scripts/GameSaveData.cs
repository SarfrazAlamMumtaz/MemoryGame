using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    [System.Serializable]
    public class GameSaveData
    {
        public int score;
        public int turn;
        public int rows;
        public int columns;
        public List<CardData> cards = new List<CardData>();
    }

    [System.Serializable]
    public class CardData
    {
        public int cardID;
        public bool isMatched;

        public CardData(int cardID, bool isMatched)
        {
            this.cardID = cardID;
            this.isMatched = isMatched;
        }
    }
}

