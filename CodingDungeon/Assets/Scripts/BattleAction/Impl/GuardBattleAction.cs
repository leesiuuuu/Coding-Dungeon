using System.Collections;

public class GuardBattleAction : AbstractBattleAction
{
	public GuardBattleAction(Character user, IEntity target) : base(user, target)
	{
	}

	public override IEnumerator StartAction()
	{
		EntityDefenseEffect defenseEffect = new EntityDefenseEffect(1, 3.33f);
		
		Target.Status.AddStatusEffect(defenseEffect);
		yield break;
	}
}
