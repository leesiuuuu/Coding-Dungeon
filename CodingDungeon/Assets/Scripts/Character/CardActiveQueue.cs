using System;
using UnityEngine;

public class CardActiveQueue
{
	public AbstractCardSo[] Cards = new AbstractCardSo[8];

	public void AddCard(int index, AbstractCardSo card)
	{
		if (Cards[index] == null)
		{
			Cards[index] = card;
		}
		else
		{
			Debug.LogError("이미 점유된 슬롯에 카드 삽입이 시도됨.");
		}
	}

	public AbstractCardSo RemoveCard(int index)
	{
		if (Cards[index] != null)
		{
			var card = Cards[index];
			Cards[index] = null;
		}
		return Cards[index];
	}

	public void Clear()
	{
		Cards = new AbstractCardSo[8];
	}
	
}