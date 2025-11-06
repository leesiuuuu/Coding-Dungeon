using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Operation Card/Repeat")]
public class RepeatOperationCard : OperationCardSo
{
	public int times = 2;
	
	public override void StartAction(CardActionContext ctx)
	{ 
		ctx.ProviderGroup.RepeatTimesProvider.RepeatTimes *= times;
	}
}