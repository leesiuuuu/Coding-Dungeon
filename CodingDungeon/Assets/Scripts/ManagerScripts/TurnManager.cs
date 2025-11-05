using System;

public enum TurnStatus
{
	PlayerSelection,
	MonsterMoves,
	PlayerMoves,
	
}

public class TurnManager : SceneSingleMono<TurnManager>
{
	private int turn;
	public event Action OnTurnChange;
	private bool isFight;
	
	
	public void AddTurn()
	{
		++turn;
	}

	// 전투 상태인지 확인할 수 있는 함수
	public bool IsFight()
	{
		return isFight;
	}

	// 현재 상태를 바꿔줌
	// ex) 전투 -> 선택
	public void ChangeAction()
	{
		isFight = !isFight;
	}
}
