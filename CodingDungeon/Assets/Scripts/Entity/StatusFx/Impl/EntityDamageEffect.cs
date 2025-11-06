using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Coding Dungeon/Status Effect/Character Damage")]
public class EntityDamageEffect : AbstractStatusFx
{
	public int Damage = 1;

	public EntityDamageEffect(int turnToLive, int damage) : base(turnToLive)
	{
		Damage = damage;
	}

	public override void OnStarted(IEntity entity)
	{
		var attributes = entity.Attributes;
		attributes.CurrentHp =
			Mathf.Clamp(attributes.CurrentHp - Damage, 0, attributes.MaxHp);
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