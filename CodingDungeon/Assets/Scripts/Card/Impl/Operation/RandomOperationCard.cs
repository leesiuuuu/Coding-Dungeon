using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Operation Card/Random Operation")]
public class RandomOperationCard : OperationCardSo
{
	public override void StartAction(CardActionContext context)
	{
		Debug.Log("계산됨!");
	}
}