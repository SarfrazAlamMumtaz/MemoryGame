using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MemoryGame
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private GameObject cardVisualHolder;
        [SerializeField] private Image cardVisual;
        [SerializeField] private List<Sprite> cardTypes = new List<Sprite>();
        public int id { get; private set; }
        public bool active { get; private set; }

        public static event Action<Card> OnCardClicked;
     
        private void OnEnable()
        {
            VisualState(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (active) return;

            active = true;

            OnCardClicked?.Invoke(this);
            VisualState(true);
       
        }
        public void ResetCard()
        {
            active = false;
            VisualState(false);
        }
        public void UpdateCard(int id,bool active)
        {
            this.id = id;
            this.active = active;
            
            cardVisual.sprite = cardTypes[id];

            if (active)
                HideCardGameobject();
        }
        private void VisualState(bool state)
        {
            cardVisualHolder.SetActive(state);
        }
        public void HideCardGameobject()
        {
            gameObject.SetActive(false);
        }
        public void CardFlip()
        {
            Sequence flipSeq = DOTween.Sequence();
            flipSeq.Append(rectTransform.DOLocalRotate(new Vector3(0, 360, 0), 4f,RotateMode.FastBeyond360).SetEase(Ease.Linear));
            flipSeq.InsertCallback(1f, FrontFace);
            flipSeq.InsertCallback(3f, BackFace);
        }
        void FrontFace()
        {
            VisualState(true);
        }

        void BackFace()
        {
            VisualState(false);
        }
    }

}
