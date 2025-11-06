using System.Collections;

public class GuardBattleAction : AbstractBattleAction
{
	public GuardBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		DefenseMultiplyEffect effect = new DefenseMultiplyEffect(1, 3.33f);
		
		Target.Status.AddStatusEffect(effect);
		yield break;
	}
}
