using System.Collections;

public class SweepBattleAction : AbstractBattleAction
{
	public SweepBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		EntityDamageEffect damageEffect = new EntityDamageEffect(0, 16);
		damageEffect.Damage = (int)(damageEffect.Damage * User.Attributes.DamageModifier);
		
		Target.Status.AddStatusEffect(damageEffect);
		yield break;
	}
}
