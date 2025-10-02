using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MemoryGame
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private GameObject cardVisual;
        [SerializeField] private TextMeshProUGUI textVisual;

        public int id { get; private set; }

        private bool active = false;

        public static event Action<Card> OnCardClicked;

        private void Start()
        {
            VisualState(false);
            CardFlip();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (active) return;

            OnCardClicked?.Invoke(this);
            VisualState(true);
       
        }
        public void ResetCard()
        {
            active = false;
            VisualState(false);
        }
        public void UpdateCard(int id)
        {
            this.id = id;

            //remove before commit
            textVisual.SetText(id.ToString());
        }
        private void VisualState(bool state)
        {
            cardVisual.SetActive(state);
        }
        public void HideCardGameobject()
        {
            gameObject.SetActive(false);
        }
        public void CardFlip()
        {
            Sequence flipSeq = DOTween.Sequence();
            flipSeq.Append(rectTransform.DOLocalRotate(new Vector3(0, 360, 0), 4f,RotateMode.FastBeyond360).SetEase(Ease.Linear));
            flipSeq.InsertCallback(1f, Method1);
            flipSeq.InsertCallback(3f, Method2);
        }
        void Method1()
        {
            VisualState(true);
        }

        void Method2()
        {
            VisualState(false);
        }
    }

}
