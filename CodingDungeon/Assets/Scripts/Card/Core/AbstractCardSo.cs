using UnityEngine;

public abstract class AbstractCardSo : ScriptableObject
{
	[SerializeField]
	public string Name;

	[SerializeField]
	[TextArea(3, 3)]
	public Sprite Image;
	
	[SerializeField]
	private string Description;
	
	[SerializeField]
	public int Cost;

	public abstract void StartAction(object param);

}