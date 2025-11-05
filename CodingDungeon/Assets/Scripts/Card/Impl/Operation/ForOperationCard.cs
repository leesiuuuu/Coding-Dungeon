using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Operation Card/For Operation")]
public class ForOperationCard : OperationCardSo
{
	public override void StartAction(CardActionContext context)
	{
		Debug.Log("계산됨!");
	}
}