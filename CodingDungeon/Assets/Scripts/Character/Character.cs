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

	public CharacterCardHolder CardHolder => _cardHolder;

	public void Start()
	{
		_cardHolder = new CharacterCardHolder(_setting.Deck);
	}
	
}