using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/Guard")]
public class GuardOutputCard : OutputCardSo
{
	public override void StartAction(CardActionContext context)
	{
		Debug.Log($"휩쓸기를 {context.User}이(가) {context.Target}에게 사용함");
	}
}