using System.Collections;

public abstract class AbstractBattleAction
{
	public Character User { get; private set; }

	public IEntity Target { get; private set; }

	public AbstractBattleAction(Character user, IEntity target)
	{
		User = user;
		Target = target;
	}

	public abstract IEnumerator StartAction();
}