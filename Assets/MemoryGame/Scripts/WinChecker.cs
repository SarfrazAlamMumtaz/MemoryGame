using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MemoryGame
{
    public class WinChecker : MonoBehaviour
    {
        [SerializeField] private ReturnToPool returnToPool;

        public List<Card> cards = new List<Card>();

        public static event Action OnWinCheckerReset;

        private void OnEnable()
        {
            Card.OnCardClicked += OnCardClicked;
        }
        private void OnDisable()
        {
            Card.OnCardClicked -= OnCardClicked;
        }

        private void OnCardClicked(Card card)
        {
            cards.Add(card);

            if (cards.Count > 1)
            {
               
                if(CheckForWin())
                {
                    Debug.Log("Win");
                }
                else
                {
                    Debug.Log("Lose");
                }

                cards.Clear();

                OnWinCheckerReset?.Invoke();

                returnToPool.ReturnToPoolGameobject();
            }
        }

        private bool CheckForWin()
        {
            bool matched = cards.All(value => value.id == cards[0].id);
            return matched;
        }
    }
}

