using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Status Effect/Character Damage")]
public class EntityDamageEffect : AbstractStatusFx
{
	public int damage = 1;
	
	public override void OnStarted(IEntity entity)
	{
		var attributes = entity.Attributes;
		attributes.CurrentHp =
			Mathf.Clamp(attributes.CurrentHp - damage, 0, attributes.MaxHp);
		entity.SetAttributes(attributes);

		if (attributes.CurrentHp == 0)
		{
			entity.Die();
		}
	}

	public override void OnFinished(IEntity entity)
	{
	}
}