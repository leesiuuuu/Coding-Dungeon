using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/Sweep")]
public class SweepOutputCard : OutputCardSo
{
	public override void StartAction(CardActionContext context)
	{
		BattleManager.Instance.BattleActions.Enqueue(new SweepBattleAction(context.User, context.Target));
	}
}