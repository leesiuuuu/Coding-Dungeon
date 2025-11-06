using System.Collections.Generic;
using System.Linq;

public class CharacterStatus
{
	private Character _character;

	public List<AbstractStatusFx> CurrentEffects { get; private set; } = new();
	
	public CharacterStatus(Character character)
	{
		_character = character;
		TurnManager.Instance.OnTurnChange += OnTurnChanged;
	}

	public void AddStatusEffect(AbstractStatusFx effect)
	{
		if (effect.TurnToLive <= 0)
		{
			effect.OnStarted(_character);
			effect.OnFinished(_character);
			return;
		}
		
		CurrentEffects.Add(effect);
		effect.OnStarted(_character);
	}

	private void OnTurnChanged()
	{
		foreach (AbstractStatusFx effect in CurrentEffects.ToList())
		{
			effect.ElapsedTurn += 1;
			if (effect.TurnToLive == effect.ElapsedTurn)
			{
				effect.OnFinished(_character);
				CurrentEffects.Remove(effect);
			}
		}
	}
}