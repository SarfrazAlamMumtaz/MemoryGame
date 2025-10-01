using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    public class GameController : MonoBehaviour
    {
        public List<Card> spawnedCards = new List<Card>();

        void Start()
        {
           
        }

        private void OnEnable()
        {
            CardGrid.OnCardPopulated += SetupGame;
        }
        private void OnDisable()
        {
            CardGrid.OnCardPopulated -= SetupGame;
        }
       
        private void SetupGame(List<Card> cards)
        {
            spawnedCards.Clear();
            spawnedCards.AddRange(cards);

            int matchedCards = spawnedCards.Count / 2;


            //foreach (var item in spawnedCards)
            //{
            //    item.UpdateCard();
            //}

        }
        public void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = Random.Range(i, list.Count);
                // Swap
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}

