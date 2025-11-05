using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField]
	private CharacterSo _setting;

	public CharacterSo Setting => _setting;
	
	[SerializeField]
	private CharacterAttributes _attributes;

	public CharacterAttributes CharacterAttributes => _attributes;
	
	private CharacterCardHolder _cardHolder;
	
	public void ActiveCardsSequentially(CardActionContext context)
	{
		_cardHolder.ActiveCardsSequentially(context);
	}

	public void AddCardAtActiveQueue(int index, AbstractCardSo card)
	{
		_cardHolder.AddCardAtActiveQueue(index, card);
	}
	
	public void RemoveCardAtActiveQueue(int index)
	{
		_cardHolder.RemoveCardAtActiveQueue(index);
	}
	
}