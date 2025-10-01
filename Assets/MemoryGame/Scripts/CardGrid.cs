using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGrid : MonoBehaviour
{
    [SerializeField] private Pool poolCards;
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private RectTransform panelRectTransform;
    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 2;
    [SerializeField] private Vector2 cardSize = new Vector2(100, 100);
    [SerializeField] private Vector2 spacing = new Vector2(10f, 10f);

    private List<Card> cards = new List<Card>();
    public static event Action<List<Card>> OnCardPopulated;

    void Start()
    {
        UpdateLayout();
        SpawnCards();
    }

    public void UpdateLayout()
    {
        // Set fixed card size and spacing
        gridLayout.cellSize = cardSize;
        gridLayout.spacing = spacing;
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;

        // Calculate required panel size
        float width = (cardSize.x * columns) + (spacing.x * (columns - 1));
        float height = (cardSize.y * rows) + (spacing.y * (rows - 1));

        // Apply new size to the panel
        panelRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        panelRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    // Optional: Call this to change layout at runtime
    public void SetLayout(int newRows, int newColumns)
    {
        rows = newRows;
        columns = newColumns;
        UpdateLayout();
    }

    public void SpawnCards()
    {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++)
            {
                GameObject go = poolCards.PoolGameobject.Get();
                go.transform.SetParent(panelRectTransform,true);
                go.transform.localScale = Vector3.one;

                Card card = go.GetComponent<Card>();
                cards.Add(card);
            }
        }

        OnCardPopulated?.Invoke(cards);
    }
}
