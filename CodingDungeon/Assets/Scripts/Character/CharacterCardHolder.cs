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

		TurnManager.Instance.OnTurnChange += FillHand;
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
	
	public EntityTarget GetRequiredTarget()
	{
		var outputCard = _activeQueue.Cards.FirstOrDefault(c => c is OutputCardSo) as OutputCardSo;
		return outputCard?.Target ?? EntityTarget.NONE;
	}
	
	public void AddCardAtActiveQueue(int index, AbstractCardSo card)
	{
		// Output 카드가 이미 ActiveQueue에 있으면 추가를 취소
		if (IsOutputCard(card) && _activeQueue.Cards.Any(c => c != null && IsOutputCard(c)))
		{
			Debug.Log("Output 카드가 이미 ActiveQueue에 존재합니다. 추가를 취소합니다.");
			return;
		}

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
	
	private bool IsOutputCard(AbstractCardSo card)
	{
		if (card == null) return false;
		// 현재는 Name으로 판별. 필요하면 다른 프로퍼티로 교체하세요.
		return card is OutputCardSo;
	}

}