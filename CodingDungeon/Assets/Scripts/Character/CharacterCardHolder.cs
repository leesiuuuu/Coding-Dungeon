using System;
using System.Linq;
using UnityEngine;

public class CharacterCardHolder
{
	private DeckSo _deckSo;
	
	private AbstractCardSo[] _hand;

	public AbstractCardSo[] Hand => _hand.ToArray();

	private CardActiveQueue _activeQueue = new CardActiveQueue();

	public void ActiveCardsSequentially(CardActionContext context)
	{
		var activeQueue = _activeQueue.Cards;

		for (int i = 0; i < activeQueue.Length; i++)
		{
			if (activeQueue[i] != null)
			{
				activeQueue[i].StartAction(context);
			}
		}
	}
	
	public void AddCardAtActiveQueue(int index, AbstractCardSo card)
	{
		_activeQueue.AddCard(index, card);
	}
	
	public void RemoveCardAtActiveQueue(int index)
	{
		_activeQueue.RemoveCard(index);
	}

}