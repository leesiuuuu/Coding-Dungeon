using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : SceneSingleMono<TurnManager>
{
	private int turn = 1;


	// 다음 턴으로 넘기는 함수
	public void AddTurn(UnityAction onTurnAdded)
	{
		++turn;
		onTurnAdded?.Invoke();
	}

	// 현재 플레이어 턴인지 확인
	public bool IsPlayerTurn()
	{
		return (turn % 2 != 0);
	}
}
