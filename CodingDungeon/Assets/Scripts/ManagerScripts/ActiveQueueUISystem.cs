using System.Collections;
using System.Linq;
using UnityEngine;

public class ActiveQueueUISystem : CardHandSystem
{
	[SerializeField] private PlayerPortraitUI _portraitUI;

	protected new void Start()
	{
	}
	
	protected void Awake()
	{
		if (handCenter == null)
			handCenter = transform;

		Debug.Log("[ActiveQueueUISystem] Subscribe Succeed: " + _portraitUI.name);
		_portraitUI.OnSelect += OnCharacterSelectedInternal;
	}

	private void OnCharacterSelectedInternal(Character selected)
	{
		Debug.Log("OnCharacterSelectedInternal(): " + selected.Setting.name);
		if (character != null)
		{
			character.CardHolder.OnActiveQueueAdded -= AddCard;
			character.CardHolder.OnActiveQueueRemoved -= RemoveCardAt;
		}
        
		character = selected;
		UpdateCards(character.CardHolder.ActiveQueueCards);

		character.CardHolder.OnActiveQueueAdded += AddCard;
		character.CardHolder.OnActiveQueueRemoved += RemoveCardAt;
	}

	protected new void RecalculatePositions()
	{
		base.RecalculatePositions();
	}
	
	protected new IEnumerator SlideInCard(Transform card, int index)
	{
		return base.SlideInCard(card, index);
	}

	protected new IEnumerator RepositionExistingCards()
	{
		return base.RepositionExistingCards();
	}
}