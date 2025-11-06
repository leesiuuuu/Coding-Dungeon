using UnityEngine;
using UnityEngine.UI;

public class MemoryBar : MonoBehaviour
{
	[SerializeField]
	private Image image;
	
	public void Update()
	{
		var activeQueue = PartyManager.Instance.SelectedCharacter.CardHolder.ActiveQueueCards;
		int count = 0;
		foreach (var i in activeQueue)
		{
			count += i.Cost;
		}

		image.fillAmount = count / 10f;
		if (count >= 10)
		{
			image.color = Color.red;
		}
		else
		{
			image.color = Color.yellow;
		}
	}
}