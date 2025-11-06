using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Operation Card/For 3")]
public class For3OperationCard : OperationCardSo
{
	public override void StartAction(CardActionContext context)
	{
		context.ProviderGroup.RepeatTimesProvider.RepeatTimes *= 3;
	}
}