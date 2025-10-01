using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame
{
    public class CardGrid : MonoBehaviour
    {
        [SerializeField] private Pool poolCards;
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private RectTransform panelRectTransform;
        [SerializeField] private int rows = 2;
        [SerializeField] private int columns = 2;
        [SerializeField] private Vector2 spacing = new Vector2(10f, 10f);

        private List<Card> cards = new List<Card>();
        public static event Action<List<Card>> OnCardPopulated;

        void Start()
        {
            UpdateLayout();
            SpawnCards();
        }

        // Optional: Change layout at runtime
        public void SetLayout(int newRows, int newCols)
        {
            rows = newRows;
            columns = newCols;
            UpdateLayout();
        }


        private void UpdateLayout()
        {
            float availableWidth = panelRectTransform.rect.width;
            float availableHeight = panelRectTransform.rect.height;

            float cellWidth = (availableWidth - spacing.x * (columns - 1)) / columns;
            float cellHeight = (availableHeight - spacing.y * (rows - 1)) / rows;

            gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
            gridLayout.spacing = spacing;
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = columns;
        }
        private void SpawnCards()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GameObject go = poolCards.PoolGameobject.Get();
                    go.transform.SetParent(panelRectTransform, true);
                    go.transform.localScale = Vector3.one;

                    Card card = go.GetComponent<Card>();
                    cards.Add(card);
                }
            }

            OnCardPopulated?.Invoke(cards);
        }
    }
}

