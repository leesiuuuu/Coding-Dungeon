using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/FireBall")]
public class FireBallOutputCard : OutputCardSo
{
	public override void StartAction(CardActionContext context)
	{
		EnqueueBattleAction(context, new FireBallBattleAction(context.User, context.Target));
	}
}