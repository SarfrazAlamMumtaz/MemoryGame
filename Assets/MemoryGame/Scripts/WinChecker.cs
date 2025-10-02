using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MemoryGame
{
    public class WinChecker : MonoBehaviour
    {
        private IReturnToPool returnToPool;
        private List<Card> cards = new List<Card>();
        private bool busy = false;

        private void Awake()
        {
            returnToPool = GetComponent<IReturnToPool>();
            ResetWinChecker();
        }
        public void ResetWinChecker()
        {
            busy = false;
            gameObject.SetActive(true);
        }
        public void OnCardClicked(Card card, GameController gameController)
        {
            if (busy) return;

            StartCoroutine(UpdateWinChecker(card, gameController));
        }
        private IEnumerator UpdateWinChecker(Card card, GameController gameController)
        {
            cards.Add(card);

            if (cards.Count > 1)
            {
                busy = true;

                if (Won())
                {
                    foreach (var item in cards)
                    {
                        item.ShakeCardScale();
                    }
                    gameController.OnCorrectPair();

                    yield return new WaitForSeconds(1);

                    foreach (var item in cards)
                    {
                        item.HideCardGameobject();
                    }
                }
                else
                {
                    gameController.OnLose();
                    yield return new WaitForSeconds(1);

                    foreach (var item in cards)
                    {
                        item.ResetCard();
                    }
                }

                returnToPool.ReturnToPoolGameobject();

                cards.Clear();

                busy = false;
            }
        }

        private bool Won()
        {
            bool matched = cards.All(value => value.id == cards[0].id);
            return matched;
        }
        public bool IsBusy()
        {
            return busy;
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}

