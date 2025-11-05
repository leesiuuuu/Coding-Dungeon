using System.Collections;
using UnityEngine;

public class ActiveQueueUISystem : CardHandSystem
{
	protected new IEnumerator SlideInCard(Transform card, int index)
	{
		return base.SlideInCard(card, index);
	}

	protected new IEnumerator RepositionExistingCards()
	{
		return base.RepositionExistingCards();
	}
}