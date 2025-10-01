using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MemoryGame
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject cardVisual;
        [SerializeField] private TextMeshProUGUI textVisual;

        public int id { get; private set; }

        private bool active = false;

        public static event Action<Card> OnCardClicked;

        private void Start()
        {
            VisualState(false);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (active) return;

            OnCardClicked?.Invoke(this);
            ToggleVisual();
        }

        public void UpdateCard(int id)
        {
            this.id = id;

            //remove before commit
            textVisual.SetText(id.ToString());
        }

        private void ToggleVisual()
        {
            active = !active;
            VisualState(active);
        }

        private void VisualState(bool state)
        {
            cardVisual.SetActive(state);
        }
    }

}
