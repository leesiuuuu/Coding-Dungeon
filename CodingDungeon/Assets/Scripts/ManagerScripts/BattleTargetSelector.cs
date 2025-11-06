using System;
using System.Linq;
using UnityEngine;

public class BattleTargetSelector : SceneSingleMono<BattleTargetSelector>
{
	public EntitySelector EntitySelector;
	[SerializeField] private TargetSelectionUI targetSelectionUI;
	
	public event Action<IEntity> OnSelected;
	
	private bool _isSelecting;
	
	private OutputCardSo _currentCard;
	
	public void Start()
	{
		EntitySelector.gameObject.SetActive(false);
	}

	public void Update()
	{
		if (_isSelecting)
		{
			if (Input.GetMouseButtonDown(0) && EntitySelector.GetEntity(out var entity, _currentCard.Target))
			{
				OnSelectedInternal(entity);
			}
		}
	}

	public void StartTargetSelection()
	{
		_currentCard = GetCurrentOutputCard();
		if (_currentCard != null)
		{
			string targetType = GetTargetTypeText(_currentCard.Target);
			targetSelectionUI.ShowMessage(_currentCard.Name, targetType);
		}

		EntitySelector.gameObject.SetActive(true);
		_isSelecting = true;
	}

	private void OnSelectedInternal(IEntity entity)
	{
		Debug.Log("OnSelectedInternal(); " + entity);
		_isSelecting = false;
		EntitySelector.gameObject.SetActive(false);
		targetSelectionUI.Hide();
		OnSelected?.Invoke(entity);
	}

	private OutputCardSo GetCurrentOutputCard()
	{
		var character = PartyManager.Instance.SelectedCharacter;
		return character?.CardHolder.ActiveQueueCards
			.FirstOrDefault(c => c is OutputCardSo) as OutputCardSo;
	}

	private string GetTargetTypeText(EntityTarget target)
	{
		return target switch
		{
			EntityTarget.PLAYER => "플레이어",
			EntityTarget.ENEMY => "적",
			EntityTarget.ENTITY => "캐릭터",
			_ => string.Empty
		};
	}
}