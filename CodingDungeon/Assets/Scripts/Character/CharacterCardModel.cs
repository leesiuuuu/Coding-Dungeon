using System;
using System.Linq;
using UnityEngine;

public class CharacterCardManager : MonoBehaviour
{
	[SerializeField]
	private DeckSo _deckSo;
	
	[SerializeField]
	private AbstractCardSo[] _hand;

	public AbstractCardSo[] Hand => _hand.ToArray();

	
	private void FillHand()
	{
		RandomCardProvider.Fill(_deckSo, _hand);
	}
	
	public void Start()
	{
		FillHand();
	}

	public void StartCardAction(CardActionContext ctx)
	{
		if (!_hand.Contains(ctx.Card))
		{
			Debug.LogError("보유하고 있지 않은 카드를 사용 시도함.");
			return;
		}
		
		ctx.Card.StartAction(ctx);
		FillHand();
	}
}