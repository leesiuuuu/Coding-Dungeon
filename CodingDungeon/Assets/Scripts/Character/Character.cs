using UnityEngine;

public class Character : MonoBehaviour, IEntity
{
	[SerializeField]
	private CharacterSo _setting;

	public CharacterSo Setting => _setting;
	
	public EntityAttributes Attributes { get; private set; }
	
	public EntityStatus Status { get; private set; }


	public void SetAttributes(EntityAttributes value)
	{
		Attributes = value;
	}

	public CharacterCardHolder CardHolder { get; private set; }

	public void Start()
	{
		Attributes = _setting.Attributes;
		Status = new EntityStatus(this);
		CardHolder = new CharacterCardHolder(_setting.Deck);
	}

	public void Die()
	{
		Debug.Log($"[Character] {_setting.Name} 캐릭터 사망!!");
	}
}