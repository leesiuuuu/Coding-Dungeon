using UnityEngine;

public class CardActionContextHolder : MonoBehaviour
{
	[SerializeField]
	private ParameterProvider[] _providers;

	public CardActionContext GetContext(AbstractCardSo card, Character user, Enemy target)
	{
		return new CardActionContext(card, user, target, _providers);
	}

}