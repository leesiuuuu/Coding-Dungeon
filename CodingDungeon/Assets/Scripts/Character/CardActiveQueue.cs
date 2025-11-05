using System;
using UnityEngine;

public class CardActiveQueue
{
	public AbstractCardSo[] Cards = new AbstractCardSo[8];

	public event Action<int, AbstractCardSo> OnCardAdded;

	public event Action<int, AbstractCardSo> OnCardRemoved;

	public void AddCard(int index, AbstractCardSo card)
	{
		if (Cards[index] == null)
		{
			Cards[index] = card;
			OnCardAdded?.Invoke(index, card);
		}
		else
		{
			Debug.LogError("이미 점유된 슬롯에 카드 삽입이 시도됨.");
		}
	}

	public void RemoveCard(int index)
	{
		if (Cards[index] != null)
		{
			var card = Cards[index];
			Cards[index] = null;
			OnCardRemoved?.Invoke(index, card);
		}
	}
	
}