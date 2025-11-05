using UnityEngine;

public abstract class AbstractCardSo : ScriptableObject
{
	public string Name;

	[TextArea(3, 3)]
	public Sprite Image;
	
	public string Description;
	
	public int Cost;

	public abstract void StartAction(CardActionContext ctx);

}