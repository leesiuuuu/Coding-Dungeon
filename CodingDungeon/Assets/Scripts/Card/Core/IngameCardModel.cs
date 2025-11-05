using System.Collections.Generic;
using UnityEngine;

public class IngameCardModel : MonoBehaviour
{
	[SerializeField]
	private List<AbstractCardSo> hand = new List<AbstractCardSo>();

	[SerializeField]
	private DeckSo deck;

	public List<AbstractCardSo> Hand => hand;

	public void UseCard(AbstractCardSo card)
	{
		if (hand.Contains(card))
		{
			hand.Remove(card);
			// 카드 사용 로직은 필요시 구현
			card.StartAction(null);
		}
	}

	public void AddCardToHand(AbstractCardSo card)
	{
		if (card != null && !hand.Contains(card))
		{
			hand.Add(card);
		}
	}

	public void InitializeFromDeck(DeckSo deckSo)
	{
		deck = deckSo;
		if (deck != null && deck.cards != null)
		{
			hand.Clear();
			foreach (var card in deck.cards)
			{
				if (card != null)
				{
					hand.Add(card);
				}
			}
		}
	}
}

