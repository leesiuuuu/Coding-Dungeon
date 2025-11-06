public class CardActionContext
{
	public AbstractCardSo Card;
	
	public Character User;

	public Enemy Target;
	
	public ParameterProviderGroup ProviderGroup;

	public CardActionContext(AbstractCardSo card, Character user, Enemy target, ParameterProviderGroup providerGroup)
	{
		Card = card;
		User = user;
		Target = target;
		ProviderGroup = providerGroup;
	}
	
}