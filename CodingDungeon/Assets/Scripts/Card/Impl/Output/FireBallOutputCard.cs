using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/FireBall")]
public class FireBallOutputCard : OutputCardSo
{
	public override void StartAction(CardActionContext context)
	{
		BattleManager.Instance.BattleActions.Enqueue(new FireBallBattleAction(context.User, context.Target));
	}
}