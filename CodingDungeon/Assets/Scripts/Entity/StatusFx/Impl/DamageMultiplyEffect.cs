using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Status Effect/Damage Multiply")]
public class DamageMultiplyEffect : AbstractStatusFx
{
	public float damageMultiplier = 1f;

	public DamageMultiplyEffect(int turnToLive, float damageMultiplier) : base(turnToLive)
	{
		this.damageMultiplier = damageMultiplier;
	}

	public override void OnStarted(IEntity entity)
	{
		var attributes = entity.Attributes;
		attributes.DamageModifier *= damageMultiplier;
		entity.SetAttributes(attributes);
	}

	public override void OnFinished(IEntity entity)
	{
		var attributes = entity.Attributes;
		attributes.DamageModifier /= damageMultiplier;
		entity.SetAttributes(attributes);
	}

	public override string GetViewString()
	{
		return $"공격력 {damageMultiplier:f2}x";
	}
}