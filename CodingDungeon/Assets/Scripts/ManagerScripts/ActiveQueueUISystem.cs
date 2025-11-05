using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveQueueUISystem : CardHandSystem
{
	[SerializeField] private PlayerPortraitUI _portraitUI;
	
	protected new void Start()
	{
		if (handCenter == null)
			handCenter = transform;

		_portraitUI.OnSelect += OnCharacterSelectedInternal;
	}

	private void OnCharacterSelectedInternal(Character selected)
	{
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
		targetPositions.Clear();
    
		int cardCount = cards.Count;
    
		for (int i = 0; i < cardCount; i++)
		{
			Vector3 pos = handCenter.position + new Vector3(i * pointDistance, 0, 0);
			targetPositions.Add(pos);
		}
	}
	
	protected new IEnumerator SlideInCard(Transform card, int index)
	{
		if (index >= targetPositions.Count) yield break;
    
		Vector3 targetPos = targetPositions[index];
		Vector3 startPos = targetPos + new Vector3(slideInDistance, 0, 0);
    
		card.position = startPos;
		card.gameObject.SetActive(true);
    
		float elapsed = 0f;
    
		List<Vector3> oldPositions = new List<Vector3>();
		for (int i = 0; i < cards.Count - 1; i++)
		{
			if (cards[i] != null)
				oldPositions.Add(cards[i].transform.position);
		}
    
		while (elapsed < slideInDuration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / slideInDuration;
			float curveValue = slideInCurve.Evaluate(t);
        
			if (index < targetPositions.Count)
			{
				targetPos = targetPositions[index];
				card.position = Vector3.Lerp(startPos, targetPos, curveValue);
			}
			
			for (int i = 0; i < cards.Count - 1; i++)
			{
				if (cards[i] != null && i < targetPositions.Count && i < oldPositions.Count)
				{
					cards[i].transform.position = Vector3.Lerp(oldPositions[i], targetPositions[i], curveValue);
				}
			}
        
			yield return null;
		}
    
		for (int i = 0; i < cards.Count; i++)
		{
			if (cards[i] != null && i < targetPositions.Count)
			{
				cards[i].transform.position = targetPositions[i];
			}
		}
    
		isAddingCard = false;
	}

	protected new IEnumerator RepositionExistingCards()
	{
		float duration = 0.3f;
		float elapsed = 0f;
    
		List<Vector3> startPositions = new List<Vector3>();
		foreach (var card in cards)
		{
			if (card != null)
				startPositions.Add(card.transform.position);
		}
    
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = Mathf.Clamp01(elapsed / duration);
			float curveValue = slideInCurve.Evaluate(t);
        
			for (int i = 0; i < cards.Count; i++)
			{
				if (cards[i] != null && i < targetPositions.Count && i < startPositions.Count)
				{

					cards[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], curveValue);
				}
			}
        
			yield return null;
		}

		for (int i = 0; i < cards.Count; i++)
		{
			if (cards[i] != null && i < targetPositions.Count)
			{
				cards[i].transform.position = targetPositions[i];
			}
		}
	}
}