using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Status Effect/Entity Defense")]
public class EntityDefenseEffect : AbstractStatusFx
{
	public float defenseMultiplier = 1f;

	public EntityDefenseEffect(int turnToLive, float defenseMultiplier) : base(turnToLive)
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
}