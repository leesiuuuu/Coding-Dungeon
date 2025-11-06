public class CardActionContext
{
	public AbstractCardSo Card;
	
	public Character User;

	public IEntity Target;
	
	public ParameterProviderGroup ProviderGroup;

	public CardActionContext(AbstractCardSo card, Character user, IEntity target, ParameterProviderGroup providerGroup)
	{
		Card = card;
		User = user;
		Target = target;
		ProviderGroup = providerGroup;
	}
	
}