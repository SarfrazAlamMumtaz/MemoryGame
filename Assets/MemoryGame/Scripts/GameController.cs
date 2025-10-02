using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MemoryGame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private Pool winCheckerPool;
        [SerializeField] private SaveSystem saveSystem;

        public UnityEvent EventStartGame;
        public UnityEvent EventCorrectPair;
        public UnityEvent EventLose;
        public UnityEvent EventFlipCard;
        public UnityEvent EventGameover;

        public static event Action<List<CardData>> OnGameStart;

        private WinChecker lastWinChecker;

        public void Play()
        {
            EventStartGame.Invoke();

            saveSystem.OnGameLoad(gameSaveData =>
            {
                gameSetting.rows = gameSaveData.rows;
                gameSetting.columns = gameSaveData.columns;
                var scoreSystem = FindAnyObjectByType<ScoreSystem>();
                scoreSystem.UpdateScore(gameSaveData.score);
                scoreSystem.UpdateTurn(gameSaveData.turn);

                List<CardData> cardList = new List<CardData>();
                
                foreach (var card in gameSaveData.cards)
                {
                    cardList.Add(new CardData(card.cardID,card.isMatched));
                }
              
                OnGameStart?.Invoke(cardList);
            },
            () =>
            {  
                //Load new game
                SetupNewGame();

            });
        }
        private void OnEnable()
        {
            Card.OnCardClicked += OnCardClicked;
        }
        private void OnDisable()
        {
            Card.OnCardClicked -= OnCardClicked;
        }
        public void SaveProgress()
        {
            GameSaveData saveData = new GameSaveData();
            var scoreSystem = FindAnyObjectByType<ScoreSystem>();
            saveData.score = scoreSystem.GetScore();
            saveData.turn = scoreSystem.GetTurn();
            saveData.rows = gameSetting.rows;
            saveData.columns = gameSetting.columns;

            var cardGrid = FindAnyObjectByType<CardGrid>();
            saveSystem.OnGameSave(cardGrid.GetCards(), saveData);
        }
        public void OnCorrectPair()
        {
            EventCorrectPair.Invoke();
            ResetlastWinChecker();

            SaveProgress();

            if (CheckForGameOver())
            {
                saveSystem.DeleteSave();

                DOVirtual.Float(0f, 1f, 1f,(float value) =>
                {
                }).OnComplete(() =>{
                    EventGameover.Invoke();
                });
            }
        }
        public void OnLose()
        {
            EventLose.Invoke();
         
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

            EventFlipCard.Invoke();
        }
        public bool CheckForGameOver()
        {
            var cardGrid = FindObjectsOfType<Card>().ToList();
            foreach (var item in cardGrid)
            {
                if (item.active == false)
                    return false;
            }

            return true;

        }
        private void ResetlastWinChecker()
        {
            lastWinChecker = null;
        }
        private void SetupNewGame()
        {
            int totalCards = (gameSetting.rows * gameSetting.columns);
            int matchedCards = totalCards / 2;

            List<CardData> cardList = new List<CardData>();

            for (int i = 0; i < matchedCards; i++)
            {
                // Add 2 cards for each matched pair
                cardList.Add(new CardData(i, false));
                cardList.Add(new CardData(i, false));
            }

            // Shuffle the list
            cardList.Shuffle();

            OnGameStart?.Invoke(cardList);
        }
        public void RestartGame()
        {
            StartCoroutine(LoadYourScene());
        }
        IEnumerator LoadYourScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

            while (!asyncLoad.isDone)
            {
                // You can use asyncLoad.progress for a loading bar
                yield return null;
            }
        }
    }
}

