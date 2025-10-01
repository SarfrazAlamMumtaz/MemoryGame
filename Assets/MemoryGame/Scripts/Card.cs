using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MemoryGame
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject cardVisual;
        [SerializeField] private TextMeshProUGUI textVisual;

        private bool active = false;
        private int id;

        private void Start()
        {
            VisualState(false);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"ID : " + id);
            ToggleVisual();
        }

        public void UpdateCard(int id)
        {
            this.id = id;

            //remove
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
