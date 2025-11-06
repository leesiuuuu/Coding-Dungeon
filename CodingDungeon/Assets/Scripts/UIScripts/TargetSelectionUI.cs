using TMPro;
using UnityEngine;

public class TargetSelectionUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI messageText;
	[SerializeField] private GameObject panel;

	private void Awake()
	{
		if (panel != null) panel.SetActive(false);
	}

	public void ShowMessage(string cardName, string targetType)
	{
		if (panel != null)
		{
			panel.SetActive(true);
			messageText.text = $"{cardName} 카드의 대상({targetType})을 선택하세요!";
		}
	}

	public void Hide()
	{
		if (panel != null)
		{
			panel.SetActive(false);
		}
	}
}