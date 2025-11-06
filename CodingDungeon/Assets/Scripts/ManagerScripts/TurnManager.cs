using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TurnStatus
{
	PlayerSelection,
	MonsterMoves,
	PlayerMoves,
	
}

public class TurnManager : SceneSingleMono<TurnManager>,IBootStrapper
{
	[SerializeField] private TurnStatus _currentTurnStatus;
	private int _turn;
	public event Action OnTurnChange;
	public event Action OnPlayerSelection;
	public event Action OnMonsterMoves;
	public event Action OnPlayerMoves;

	public event Action OnLose;
	public event Action OnWin;

	protected override void Awake()
	{
		base.Awake();
		BootManager.Register(this);
	}
	
	public IEnumerator BootStrap()
	{
		yield return new WaitForSeconds(0.1f);
		SetTurnStatus(TurnStatus.PlayerSelection);
	}

	public void TurnChange()
	{
		++_turn;
		OnTurnChange?.Invoke();
	}

	public TurnStatus GetTurnStatus()
	{
		return _currentTurnStatus;
	}

	public void SetTurnStatus(TurnStatus turnStatus)
	{
		_currentTurnStatus = turnStatus;
		CheckWinLose();
		switch (_currentTurnStatus)
		{
			case TurnStatus.PlayerSelection:
				PlayerSelection();
				break;
			case TurnStatus.MonsterMoves:
				MonsterMoves();
				break;
			case TurnStatus.PlayerMoves:
				PlayerMoves();
				break;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SetTurnStatus(TurnStatus.PlayerSelection);
		}
	}

	public void NextTurnStatus()
	{
		var len = Enum.GetValues(typeof(TurnStatus)).Length;
		_currentTurnStatus = (TurnStatus)((int)_currentTurnStatus+1%len);
		SetTurnStatus(_currentTurnStatus);
	}

	private void PlayerSelection()
	{
		OnPlayerSelection?.Invoke();
	}

	private void MonsterMoves()
	{
		StartCoroutine(MonsterMoveFlow());
	}

	private IEnumerator MonsterMoveFlow()
	{
		yield return new WaitForSeconds(1f);
		OnMonsterMoves?.Invoke();
	}

	private void PlayerMoves()
	{
		OnPlayerMoves?.Invoke();
	}

	public int GetTurn()
	{
		return _turn;
	}

	public void CheckWinLose()
	{
		if (PartyManager.Instance.alivePlayers <= 0)
		{
			OnLose?.Invoke();
		}
		else if (EnemyParty.Instance.Party.Count <= 0)
		{
			OnWin?.Invoke();
		}
	}
	
	
}
