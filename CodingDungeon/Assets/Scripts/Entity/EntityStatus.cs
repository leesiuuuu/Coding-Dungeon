using System.Collections.Generic;
using System.Linq;

public class EntityStatus
{
	private IEntity _entity;

	public List<AbstractStatusFx> CurrentEffects { get; private set; } = new();
	
	public EntityStatus(IEntity entity)
	{
		_entity = entity;
		TurnManager.Instance.OnTurnChange += OnTurnChanged;
	}

	public void AddStatusEffect(AbstractStatusFx effect)
	{
		if (effect.TurnToLive <= 0)
		{
			effect.OnStarted(_entity);
			effect.OnFinished(_entity);
			return;
		}
		
		CurrentEffects.Add(effect);
		effect.OnStarted(_entity);
	}

	private void OnTurnChanged()
	{
		foreach (AbstractStatusFx effect in CurrentEffects.ToList())
		{
			effect.ElapsedTurn += 1;
			if (effect.TurnToLive == effect.ElapsedTurn)
			{
				effect.OnFinished(_entity);
				CurrentEffects.Remove(effect);
			}
		}
	}
}