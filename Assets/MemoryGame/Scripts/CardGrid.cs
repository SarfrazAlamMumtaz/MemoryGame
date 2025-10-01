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

        private void OnEnable()
        {
            GameController.OnGameStart += SetupBoard;
        }
        private void OnDisable()
        {
            GameController.OnGameStart -= SetupBoard;
            StopAllCoroutines();
        }
        private void SetupBoard(List<int> cards)
        {
            UpdateLayout(gameSetting.rows, gameSetting.columns,new Vector2(10,10));
            SpawnCards(cards);
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
        private void SpawnCards(List<int> cards)
        {
            StartCoroutine(SpawnCardRoutine(cards));
        }
        private IEnumerator SpawnCardRoutine(List<int> cards)
        {
            foreach (RectTransform card in panelRectTransform)
            {
                card.gameObject.GetComponent<IReturnToPool>().ReturnToPoolGameobject();
            }

            gridLayout.enabled = true;

            int count = cards.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject go = poolCards.PoolGameobject.Get();
                go.transform.SetParent(panelRectTransform, true);
                go.transform.localScale = Vector3.one;

                Card card = go.GetComponent<Card>();
                card.UpdateCard(cards[i]);
            }

            yield return new WaitForSeconds(1);

            gridLayout.enabled = false;
        }
    }
}

