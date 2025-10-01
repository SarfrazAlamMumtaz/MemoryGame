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

        private WinChecker lastWinChecker;

        private void Start()
        {
            SetupGame();
        }
        private void OnEnable()
        {
            Card.OnCardClicked += OnCardClicked;
        }
        private void OnDisable()
        {
            Card.OnCardClicked -= OnCardClicked;
        }

        private void OnCardClicked(Card card)
        {
            if(lastWinChecker != null && lastWinChecker.IsBusy() == false)
            {
                lastWinChecker.OnCardClicked(card);
            }
            else
            {
                GameObject go = winCheckerPool.PoolGameobject.Get();
                lastWinChecker = go.GetComponent<WinChecker>();

                lastWinChecker.OnCardClicked(card);
            }
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

