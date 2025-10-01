using System;
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

        public static event Action OnWinCheckerReset;

        private void Awake()
        {
            returnToPool = GetComponent<IReturnToPool>();
        }
        public void OnCardClicked(Card card)
        {
            if (busy) return;

            StopAllCoroutines();
            StartCoroutine(UpdateWinChecker(card));
        }
        private IEnumerator UpdateWinChecker(Card card)
        {
            cards.Add(card);

            if (cards.Count > 1)
            {
                busy = true;

                yield return new WaitForSeconds(1);
                if (CheckForWin())
                {
                    foreach (var item in cards)
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Debug.Log("Lose");
                }

                cards.Clear();

                OnWinCheckerReset?.Invoke();

                returnToPool.ReturnToPoolGameobject();

                busy = false;
            }
        }

        private bool CheckForWin()
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

