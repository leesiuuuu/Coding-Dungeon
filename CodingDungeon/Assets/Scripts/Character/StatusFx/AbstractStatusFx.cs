using UnityEngine;

public abstract class AbstractStatusFx : ScriptableObject
{
	public int TurnToLive = 1;
	
	public int ElapsedTurn { get; set; }
	
	public abstract void OnStarted(Character character);

	public abstract void OnFinished(Character character);
}