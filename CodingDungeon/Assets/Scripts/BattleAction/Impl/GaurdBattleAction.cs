using System.Collections;

public class GaurdBattleAction : AbstractBattleAction
{
	public GaurdBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		//Target.Status.AddStatusEffect(damageEffect);
		yield break;
	}
}
