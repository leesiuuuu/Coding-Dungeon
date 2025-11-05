using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCardHolder
{
	private DeckSo _deckSo;
	
	private List<AbstractCardSo> _hand = new();

	public List<AbstractCardSo> Hand => _hand.ToList();

	public List<AbstractCardSo> ActiveQueueCards => _activeQueue.Cards;

	private CardActiveQueue _activeQueue = new();
	
	public event Action<AbstractCardSo> OnCardAdded;

	public event Action<int> OnCardRemoved;

	public event Action<AbstractCardSo> OnActiveQueueAdded;

	public event Action<int> OnActiveQueueRemoved;

	public CharacterCardHolder(DeckSo deckSo)
	{
		_deckSo = deckSo;
		FillHand();
	}

	private void FillHand()
	{
		List<AbstractCardSo> newCards = RandomCardProvider.Fill(_deckSo, _hand, 7);
		foreach (var card in newCards)
		{
			AddHandCard(card);
		}
	}

	private void AddHandCard(AbstractCardSo card)
	{
		_hand.Add(card);
		OnCardAdded?.Invoke(card);
	}

	private void RemoveHandCard(int index)
	{
		_hand.RemoveAt(index);
		OnCardRemoved?.Invoke(index);
	}

	public void ActiveCardsSequentially(CardActionContext context)
	{
		var activeCardArray = _activeQueue.Cards;

		for (int i = 0; i < activeCardArray.Count; i++)
		{
			if (activeCardArray[i] != null)
			{
				context.Card = activeCardArray[i];
				activeCardArray[i].StartAction(context);
			}
		}
		_activeQueue.Clear();
	}
	
	public void AddCardAtActiveQueue(int index, AbstractCardSo card)
	{
		RemoveHandCard(index);
		_activeQueue.AddCard(card);
		OnActiveQueueAdded?.Invoke(card);
	}
	
	public void RemoveCardAtActiveQueue(int index)
	{
		Debug.Log(index);
		AddHandCard(_activeQueue.Cards[index]);
		_activeQueue.RemoveCard(index);
		OnActiveQueueRemoved?.Invoke(index);
	}

}