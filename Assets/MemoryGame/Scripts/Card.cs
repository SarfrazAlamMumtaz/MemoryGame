using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MemoryGame
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject cardVisual;
        private bool toggle = false;
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"Clicked cell at" + gameObject);
            ToggleVisual();
        }


        private void ToggleVisual()
        {
            toggle = !toggle;
            cardVisual.SetActive(toggle);
        }
    }

}
