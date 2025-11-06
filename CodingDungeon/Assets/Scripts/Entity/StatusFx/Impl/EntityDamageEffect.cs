using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Coding Dungeon/Status Effect/Damage")]
public class DamageEffect : AbstractStatusFx
{
	public int Damage = 1;

	public DamageEffect(int turnToLive, int damage) : base(turnToLive)
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

	public override string GetViewString()
	{
		return null;
	}
}