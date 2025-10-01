using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MemoryGame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private Pool winCheckerPool;

        public static event Action<List<int>> OnGameStart;
        public static event Action ActionWin;
        public static event Action ActionTurn;

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
        public void OnWin()
        {
            ActionTurn.Invoke();
            ActionWin.Invoke();
            ResetlastWinChecker();
        }
        public void OnLose()
        {
            ActionTurn.Invoke();
            ResetlastWinChecker();
        }
        private void OnCardClicked(Card card)
        {
            if (lastWinChecker != null && lastWinChecker.IsBusy() == false)
            {
                lastWinChecker.OnCardClicked(card,this);
            }
            else
            {
                GameObject go = winCheckerPool.PoolGameobject.Get();
                var pool_wc = go.GetComponent<WinChecker>();
                lastWinChecker = pool_wc;
                lastWinChecker.ResetWinChecker();
                lastWinChecker.OnCardClicked(card,this);
            }
        }
        private void ResetlastWinChecker()
        {
            lastWinChecker = null;
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

