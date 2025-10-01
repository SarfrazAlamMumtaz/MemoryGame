using System;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private Pool winCheckerPool;

        public static event Action<List<int>> OnGameStart;

        private void Start()
        {
            SetupGame();
            SpawnWinChecker();
        }
        private void OnEnable()
        {
            WinChecker.OnWinCheckerReset += SpawnWinChecker;
        }
        private void OnDisable()
        {
            WinChecker.OnWinCheckerReset -= SpawnWinChecker;
        }
        private void SpawnWinChecker()
        {
            Debug.Log("Spawn new win checker");

            GameObject go = winCheckerPool.PoolGameobject.Get();
        }
        private void SetupGame()
        {
            int totalCards = (gameSetting.rows * gameSetting.columns);
            int matchedCards = totalCards / 2;

            List<int> cardsId = new List<int>(new int[totalCards]);

            for (int i = 0; i < matchedCards; i++)
            {
                cardsId[i] = i;
                cardsId[matchedCards + i] = i;
            }

            //shuffle
            cardsId.Shuffle();

            OnGameStart?.Invoke(cardsId);
        }
    }
}

