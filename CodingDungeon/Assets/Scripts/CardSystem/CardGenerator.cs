using UnityEngine;

namespace CardSystem
{
    public class CardGenerator : MonoBehaviour
    {
        [SerializeField] private DeckSo deck;
        [SerializeField] private int cardsToGenerate = 5;

        public void GenerateCards(IngameCardModel cardModel)
        {
            if (cardModel == null || deck == null)
            {
                Debug.LogWarning("CardModel 또는 Deck이 없습니다.");
                return;
            }

            for (int i = 0; i < cardsToGenerate; i++)
            {
                cardModel.GenerateCard();
            }
        }

        public void GenerateCards(IngameCardModel cardModel, int count)
        {
            if (cardModel == null || deck == null)
            {
                Debug.LogWarning("CardModel 또는 Deck이 없습니다.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                cardModel.GenerateCard();
            }
        }
    }
}

