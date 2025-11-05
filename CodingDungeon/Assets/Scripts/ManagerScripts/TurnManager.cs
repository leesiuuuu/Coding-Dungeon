public class TurnManager : SceneSingleMono<TurnManager>
{
	private int turn = 1;

	// 전투 상태인지를 확인하는 변수
	private bool isFight = false;

	// 다음 턴으로 넘기는 함수
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
