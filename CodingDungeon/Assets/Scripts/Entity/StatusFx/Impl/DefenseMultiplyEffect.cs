using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Status Effect/Defense Multiply")]
public class DefenseMultiplyEffect : AbstractStatusFx
{
	public float defenseMultiplier = 1f;

	public DefenseMultiplyEffect(int turnToLive, float defenseMultiplier) : base(turnToLive)
	{
		this.defenseMultiplier = defenseMultiplier;
	}

	public override void OnStarted(IEntity entity)
	{
		var attributes = entity.Attributes;
		attributes.DefenseModifier *= defenseMultiplier;
		entity.SetAttributes(attributes);
	}

	public override void OnFinished(IEntity entity)
	{
		var attributes = entity.Attributes;
		attributes.DefenseModifier /= defenseMultiplier;
		entity.SetAttributes(attributes);
	}

	public override string GetViewString()
	{
		return $"방어력 {defenseMultiplier:f2}x";
	}
}