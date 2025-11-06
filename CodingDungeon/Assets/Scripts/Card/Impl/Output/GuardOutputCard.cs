using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/Guard")]
public class GuardOutputCard : OutputCardSo
{
	public override void StartAction(CardActionContext context)
	{
		EnqueueBattleAction(context, new GuardBattleAction(context.User, context.Target));
	}
}