using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "Deck", menuName = "Card System/Deck")]
    public class DeckSo : ScriptableObject
    {
        [SerializeField] private List<AbstractCardSo<object>> cards = new List<AbstractCardSo<object>>();

        public List<AbstractCardSo<object>> Cards => cards;

        public void AddCard(AbstractCardSo<object> card)
        {
            if (card != null && !cards.Contains(card))
            {
                cards.Add(card);
            }
        }

        public void RemoveCard(AbstractCardSo<object> card)
        {
            cards.Remove(card);
        }
    }
}

