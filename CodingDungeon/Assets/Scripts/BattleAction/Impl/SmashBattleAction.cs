using System.Collections;

public class SmashBattleAction : AbstractBattleAction
{
	public SmashBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		EntityDamageEffect damageEffect = new EntityDamageEffect(0, 11);
		damageEffect.Damage = (int)(damageEffect.Damage * User.Attributes.DamageModifier);
		
		Target.Status.AddStatusEffect(damageEffect);
		yield break;;
	}
}
