using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/Smash")]
public class SmashOutputCard : OutputCardSo
{
	public override void StartAction(CardActionContext context)
	{
		BattleManager.Instance.BattleActions.Enqueue(new SmashBattleAction(context.User, context.Target));
	}
}