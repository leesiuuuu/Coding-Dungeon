using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Operation Card/Private Operation")]
public class PrivateOperationCard : OperationCardSo
{
	public override void StartAction(CardActionContext context)
	{
		Debug.Log("계산됨!");
	}
}