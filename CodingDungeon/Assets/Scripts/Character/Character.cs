using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField]
	private CharacterSo _setting;

	public CharacterSo Setting => _setting;
	
	public CharacterStatus Status { get; private set; }

	public CharacterAttributes Attributes;

	public CharacterCardHolder CardHolder { get; private set; }

	public void Start()
	{
		Status = new CharacterStatus(this);
		Attributes = _setting.Attributes;
		CardHolder = new CharacterCardHolder(_setting.Deck);
	}

	public void Die()
	{
		Debug.Log($"[Character] {_setting.Name} 캐릭터 사망!!");
	}
}