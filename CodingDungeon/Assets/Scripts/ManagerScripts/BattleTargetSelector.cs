using System;
using UnityEngine;

public class BattleTargetSelector : SceneSingleMono<BattleTargetSelector>
{
	public EntitySelector EntitySelector;
	
	public event Action<IEntity> OnSelected;
	
	private bool _isSelecting;
	
	public void Start()
	{
		EntitySelector.gameObject.SetActive(false);
	}

	public void Update()
	{
		if (_isSelecting)
		{
			if (Input.GetMouseButtonDown(0) &&
				EntitySelector.GetEntity(out var entity)
				)
			{
				OnSelectedInternal(entity);
			}
		}
	}

	public void StartTargetSelection()
	{
		EntitySelector.gameObject.SetActive(true);
		_isSelecting = true;
	}

	private void OnSelectedInternal(IEntity entity)
	{
		Debug.Log("OnSelectedInternal(); " + entity);
		_isSelecting = false;
		OnSelected?.Invoke(entity);
	}
}