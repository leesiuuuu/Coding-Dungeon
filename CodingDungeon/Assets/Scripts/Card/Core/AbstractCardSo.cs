using UnityEngine;

public abstract class AbstractCardSo : ScriptableObject
{
	public string Name;
	
	public Sprite Image;
	
	[TextArea(3, 3)]
	public string Description;
	
	public int Cost;

	public EntityTarget Target;

	public abstract void StartAction(CardActionContext ctx);

}

public enum EntityTarget
{
	NONE,
	ENTITY,
	ENEMY,
	PLAYER
}