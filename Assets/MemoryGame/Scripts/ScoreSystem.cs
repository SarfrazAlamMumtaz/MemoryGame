using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    public class ScoreSystem : MonoBehaviour
    {
        private int Score;
        private int Turn;

        public static event Action<int> OnScoreChanged;
        public static event Action<int> OnTurnChanged;
       
        public void AddScore()
        {
            Score += 1;
            OnScoreChanged?.Invoke(Score);
        }

        public void AddTurn()
        {
            Turn += 1;
            OnTurnChanged?.Invoke(Turn);
        }

        public void ResetStats()
        {
            Score = 0;
            Turn = 0;
            OnScoreChanged?.Invoke(Score);
            OnTurnChanged?.Invoke(Turn);
        }
    }
}

