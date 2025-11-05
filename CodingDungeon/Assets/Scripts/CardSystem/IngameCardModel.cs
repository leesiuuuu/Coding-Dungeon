using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class IngameCardModel : MonoBehaviour
    {
        [SerializeField] private DeckSo deck;
        [SerializeField] private List<AbstractCardSo<object>> hand = new List<AbstractCardSo<object>>();

        public DeckSo Deck => deck;
        public List<AbstractCardSo<object>> Hand => hand;
        public int HandCount => hand.Count;

        private void Start()
        {
            if (deck != null)
            {
                GenerateCard();
            }
        }

        public void UseCard(int index)
        {
            if (index >= 0 && index < hand.Count)
            {
                var card = hand[index];
                hand.RemoveAt(index);
                Debug.Log($"카드 사용: {card.Name}");
            }
        }

        public void UseCard(AbstractCardSo<object> card)
        {
            if (hand.Contains(card))
            {
                hand.Remove(card);
                Debug.Log($"카드 사용: {card.Name}");
            }
        }

        public void GenerateCard()
        {
            if (deck == null || deck.Cards.Count == 0)
            {
                Debug.LogWarning("덱이 없거나 비어있습니다.");
                return;
            }

            // 덱에서 랜덤하게 카드를 뽑아서 핸드에 추가
            int randomIndex = Random.Range(0, deck.Cards.Count);
            var card = deck.Cards[randomIndex];
            
            if (card != null)
            {
                hand.Add(card);
                Debug.Log($"카드 생성: {card.Name}");
            }
        }

        public void SetDeck(DeckSo newDeck)
        {
            deck = newDeck;
            hand.Clear();
        }
    }
}

