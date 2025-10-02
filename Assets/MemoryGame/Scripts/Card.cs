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

            CardFlip();
        }
        public void ResetCard()
        {
            active = false;
            VisualState(false);
            Sequence flipSeq = DOTween.Sequence();
            flipSeq.Append(rectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
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
            flipSeq.Append(rectTransform.DOLocalRotate(new Vector3(0, 180, 0), 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
            flipSeq.InsertCallback(0.1f, FrontFace);
        }
        void FrontFace()
        {
            VisualState(true);
        }

        void BackFace()
        {
            VisualState(false);
        }
        public void ShakeCardScale()
        {
            rectTransform.DOShakeScale(0.3f, 0.2f, 10, 90, false, ShakeRandomnessMode.Full);
        }

    }

}
