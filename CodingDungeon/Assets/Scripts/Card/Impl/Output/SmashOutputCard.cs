using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/Smash")]
public class SmashOutputCard : OutputCardSo
{
	public override void StartAction(CardActionContext context)
	{
		EnqueueBattleAction(context, new SmashBattleAction(context.User, context.Target));
	}
}