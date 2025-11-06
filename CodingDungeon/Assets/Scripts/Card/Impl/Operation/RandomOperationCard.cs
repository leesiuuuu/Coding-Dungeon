using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Operation Card/Random")]
public class RandomOperationCard : OperationCardSo
{
	public int EffectLifetime = 1;
	public float RandomMin = 1;
	public float RandomMax = 2;
	
	public override void StartAction(CardActionContext context)
	{
		float value = Random.Range(RandomMin, RandomMax);
		var damageMultiplier = new DamageMultiplyEffect(EffectLifetime, value);
		var defenseMultiplier = new DefenseMultiplyEffect(EffectLifetime, value);
		
		context.User.Status.AddStatusEffect(damageMultiplier);
		context.User.Status.AddStatusEffect(defenseMultiplier);
	}
}