using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnStatus
{
	PlayerSelection,
	MonsterMoves,
	PlayerMoves,
	OnTurnChange,
	
}

public class TurnManager : SceneSingleMono<TurnManager>,IBootStrapper
{
	[SerializeField] private TurnStatus _currentTurnStatus;
	private int _turn;
	public event Action OnTurnChange;
	public event Action OnPlayerSelection;
	public event Action OnMonsterMoves;
	public event Action OnPlayerMoves;

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

	private void TurnChange()
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
			case TurnStatus.OnTurnChange:
				TurnChange();
				break;
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
	
	
}
