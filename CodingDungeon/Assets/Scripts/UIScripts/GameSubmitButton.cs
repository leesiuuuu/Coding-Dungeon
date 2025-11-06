using UnityEngine;

public class GameSubmitButton : MonoBehaviour
{
	public void Submit()
	{
		BattleManager.Instance.SubmitActiveQueues();
	}
}