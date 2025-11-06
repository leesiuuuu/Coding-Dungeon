using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Status Effect/Character Damage")]
public class CharacterDamageEffect : AbstractStatusFx
{
	public int damage = 1;
	
	public override void OnStarted(Character character)
	{
		character.Attributes.CurrentHp =
			Mathf.Clamp(character.Attributes.CurrentHp - damage, 0, character.Attributes.MaxHp);
		
		Debug.Log(name);
		
		character.Die();
	}

	public override void OnFinished(Character character)
	{
	}
}