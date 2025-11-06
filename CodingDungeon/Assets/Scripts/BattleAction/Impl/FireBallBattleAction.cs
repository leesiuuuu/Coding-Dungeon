using System.Collections;

public class FireBallBattleAction : AbstractBattleAction
{
	public FireBallBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		EntityDamageEffect damageEffect = new EntityDamageEffect(0, 20);
		damageEffect.Damage = (int)(damageEffect.Damage * User.Attributes.DamageModifier);
		
		Target.Status.AddStatusEffect(damageEffect);
		yield break;
	}
}
