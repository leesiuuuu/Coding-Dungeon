using UnityEngine;
using UnityEngine.EventSystems;

public class StoreCharacterAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private GameObject stats;
	public void OnPointerEnter(PointerEventData eventData)
	{
		stats.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		stats.SetActive(false);
	}
}
