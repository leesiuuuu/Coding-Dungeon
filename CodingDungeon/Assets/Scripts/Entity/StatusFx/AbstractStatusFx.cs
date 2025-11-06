using UnityEngine;

public abstract class AbstractStatusFx : ScriptableObject
{
	public int TurnToLive = 1;
	
	public int ElapsedTurn { get; set; }

	protected AbstractStatusFx(int turnToLive)
	{
		TurnToLive = turnToLive;
	}

	public abstract void OnStarted(IEntity entity);

	public abstract void OnFinished(IEntity entity);
}