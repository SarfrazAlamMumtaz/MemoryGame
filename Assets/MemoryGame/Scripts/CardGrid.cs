using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MemoryGame
{
    public class CardGrid : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;

        [SerializeField] private Pool poolCards;
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private RectTransform panelRectTransform;

        public UnityEvent OnLoadingStarted;
        public UnityEvent OnLoadingFinished;

        private List<Card> spawnedCards = new List<Card>(); 

        private void OnEnable()
        {
            GameController.OnGameStart += SetupBoard;
        }
        private void OnDisable()
        {
            GameController.OnGameStart -= SetupBoard;
            StopAllCoroutines();
        }
        private void SetupBoard(List<CardData> cards, bool showCards)
        {
            UpdateLayout(gameSetting.rows, gameSetting.columns,new Vector2(10,10));
            SpawnCards(cards, showCards);
        }
        private void UpdateLayout(int rows , int cols , Vector2 spacing)
        {
            float availableWidth = panelRectTransform.rect.width;
            float availableHeight = panelRectTransform.rect.height;

            float cellWidth = (availableWidth - spacing.x * (cols - 1)) / cols;
            float cellHeight = (availableHeight - spacing.y * (rows - 1)) / rows;

            gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
            gridLayout.spacing = spacing;
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = cols;
        }
        private void SpawnCards(List<CardData> cards, bool showCards)
        {
            StartCoroutine(SpawnCardRoutine(cards, showCards));
        }
        private IEnumerator SpawnCardRoutine(List<CardData> cards,bool showCards)
        {
            OnLoadingStarted.Invoke();

            spawnedCards.Clear();

            foreach (RectTransform card in panelRectTransform)
            {
                card.gameObject.GetComponent<IReturnToPool>().ReturnToPoolGameobject();
            }

            gridLayout.enabled = true;

            for (int i = 0; i < cards.Count; i++)
            {
                GameObject go = poolCards.PoolGameobject.Get();
                go.transform.SetParent(panelRectTransform, true);
                go.transform.localScale = Vector3.one;
               
                Card card = go.GetComponent<Card>();
                spawnedCards.Add(card);
            }

            yield return new WaitForSeconds(1);

            gridLayout.enabled = false;

            yield return new WaitForSeconds(1);

            for (int i = 0; i < cards.Count; i++)
            {
                spawnedCards[i].UpdateCard(cards[i].cardID, cards[i].isMatched, showCards);
            }

            OnLoadingFinished.Invoke();
        }
        public List<Card> GetCards()
        {
            return spawnedCards;
        }
    }
}

