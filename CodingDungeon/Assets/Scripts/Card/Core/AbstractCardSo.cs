using UnityEngine;

public abstract class AbstractCardSo : ScriptableObject
{
	public string Name;
	
	public Sprite Image;
	
	[TextArea(3, 3)]
	public string Description;
	
	public int Cost;

	public abstract void StartAction(CardActionContext ctx);

}