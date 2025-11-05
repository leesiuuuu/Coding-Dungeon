using UnityEngine;

public abstract class AbstractCardSo<TParam> : ScriptableObject
{
	[SerializeField] public string Name;

	[SerializeField] private string Description;

	public void StartAction(object param)
	{
		StartActionInternal((TParam)param);
	}

	protected abstract void StartActionInternal(TParam param);
}
