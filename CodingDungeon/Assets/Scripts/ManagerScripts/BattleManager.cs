using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SceneSingleMono<BattleManager>
{
	[SerializeField] private CardActionContextHolder _cardActionContextHolder;
	
	public void SubmitActiveQueues(Character user, IEntity target)
	{
		Debug.Log($"[BattleManager]: {user} - {target}");
	}

	public void StartBattle()
	{
	}
}