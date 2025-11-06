using TMPro;
using UnityEngine;

public class CoinDataLoad : MonoBehaviour
{
	[SerializeField] private TMP_Text coinText;
	private void Update()
	{
		coinText.text = PlayerDataManager.Instance.Coin.ToString();
	}
}
