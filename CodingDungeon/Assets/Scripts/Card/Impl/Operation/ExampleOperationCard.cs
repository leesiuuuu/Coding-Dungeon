using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Operation Card/Example")]
public class ExampleOperationCard : OperationCardSo
{
	public override void StartAction(CardActionContext context)
	{
		Debug.Log("계산됨!");
	}
}