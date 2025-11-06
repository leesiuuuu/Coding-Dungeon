using UnityEngine;

public class BattleManager : SceneSingleMono<BattleManager>
{
	[SerializeField] private CardActionContextHolder _cardActionContextHolder;

	public Character User;

	public Enemy Target;
	
	public void SubmitActiveQueues()
	{
		var context = _cardActionContextHolder.GetContext(null, User, Target);
		foreach (var character in PartyManager.Instance.Characters)
		{
			character.CardHolder.ActiveCardsSequentially(context);
		}
	}
}