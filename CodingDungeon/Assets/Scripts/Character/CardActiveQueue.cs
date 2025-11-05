using System;
using System.Collections.Generic;
using UnityEngine;

public class CardActiveQueue
{
	public List<AbstractCardSo> Cards = new();

	public void AddCard(AbstractCardSo card)
	{
		Cards.Add(card);
	}

	public void RemoveCard(int index)
	{
		Cards.RemoveAt(index);
	}

	public void Clear()
	{
		Cards.Clear();
	}
	
}