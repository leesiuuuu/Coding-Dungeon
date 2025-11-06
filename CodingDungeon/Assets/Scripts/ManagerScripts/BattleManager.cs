using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : SceneSingleMono<BattleManager>
{
	[SerializeField] private CardActionContextHolder _cardActionContextHolder;

	private Dictionary<Character, IEntity> _submittedActions = new();

	public Queue<AbstractBattleAction> BattleActions = new();
	
	public void Start()
	{
		TurnManager.Instance.OnPlayerMoves += StartBattle;
	}
	
	public void SubmitAction(Character user, IEntity target)
	{
		_submittedActions.Add(user, target);
	}

	private void StartBattle()
	{
		StartCoroutine(StartBattleCoroutine());
	}

	private IEnumerator StartBattleCoroutine()
	{
		foreach (var i in _submittedActions.ToList())
		{
			_submittedActions.Remove(i.Key);

			var user = i.Key;
			var target = i.Value;
			CardActionContext context = _cardActionContextHolder.GetContext(null, user, target);
			user.CardHolder.ActiveCardsSequentially(context);
		}

		while (BattleActions.Count != 0)
		{
			AbstractBattleAction battleAction = BattleActions.Dequeue();
			yield return battleAction.StartAction();
		}
	}
}