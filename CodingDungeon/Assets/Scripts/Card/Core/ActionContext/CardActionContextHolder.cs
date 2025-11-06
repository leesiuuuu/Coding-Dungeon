using UnityEngine;

public class CardActionContextHolder : MonoBehaviour
{
	public ParameterProviderGroup ProviderGroup;

	public CardActionContext GetContext(AbstractCardSo card, Character user, Enemy target)
	{
		return new CardActionContext(card, user, target, ProviderGroup);
	}

}