using System.Linq;

public class CardActionContext
{
	public AbstractCardSo Card;
	
	public Character User;

	public Character Target;
	
	public ParameterProvider[] Providers;

	public CardActionContext(AbstractCardSo card, Character user, Character target, ParameterProvider[] providers)
	{
		Card = card;
		User = user;
		Target = target;
		Providers = providers;
	}

	public ParameterProvider GetContext<T>() where T : CardActionContext
	{
		var provider = Providers.FirstOrDefault(i => i.GetType() == typeof(T));
		return provider;
	}
}